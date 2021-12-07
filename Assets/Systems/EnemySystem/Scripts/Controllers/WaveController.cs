using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using VisualStudioEX3.Artemis.Assets.EnemySystem.Models;

namespace VisualStudioEX3.Artemis.Assets.EnemySystem.Controllers
{
    /// <summary>
    /// Wave Controller.
    /// </summary>
    /// <remarks>This component manages the wave logic and spawn <see cref="EnemyController"/> instances.</remarks>
    public class WaveController : MonoBehaviour
    {
        #region Internal vars
        private int _totalWaveEnemies;
        private int _deaths;

        private WaveAsset _wave;

        private WaveManager _waveManager;
        #endregion

        #region Initializers
        private void Awake() => this.ResolveWaveManagerComponent();
        #endregion

        #region Methods & Functions
        private void ResolveWaveManagerComponent() => this._waveManager = this.GetComponent<WaveManager>();

        private EnemySpawnerController GetRandomEnemySpawnLocation() => this._waveManager.GetRandomEnemySpawnLocation();

        private EnemyController SpawnEnemy(Type enemyType) => this.GetRandomEnemySpawnLocation().SpawnEnemy(enemyType);
        #endregion

        #region Event listeners
        private void OnEnemyDead(int reward) => this._deaths++;
        #endregion

        #region Coroutines
        public IEnumerator StartNextWaveCoroutine(WaveAsset wave)
        {
            this._totalWaveEnemies = wave.enemyTypes.Sum(e => e.count);
            this._deaths = 0;
            this._wave = wave;

            yield return this.WaveCoroutine();
        }

        private IEnumerator WaveCoroutine()
        { 
            foreach (EnemyWaveData data in this._wave.enemyTypes)
                this.StartCoroutine(this.EnemySpawnCoroutine(data));

            yield return new WaitUntil(() => this._deaths == this._totalWaveEnemies);
        }

        private IEnumerator EnemySpawnCoroutine(EnemyWaveData data)
        {
            yield return new WaitForSeconds(data.startToSpawnDelay);

            for (int i = 0; i < data.count; i++)
            {
                EnemyController enemy = this.SpawnEnemy(data.enemy.GetType());
                enemy.OnDead += this.OnEnemyDead;
            }
        }
        #endregion
    }
}
