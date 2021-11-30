using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VisualStudioEX3.Artemis.Assets.EnemySystem.Models;
using VisualStudioEX3.Artemis.Framework.Core.Components;

namespace VisualStudioEX3.Artemis.Assets.EnemySystem.Controllers
{
    /// <summary>
    /// Wave Controller.
    /// </summary>
    /// <remarks>Singleton watcher component that only manages the <see cref="EnemyController"/> death count.</remarks>
    public class WaveController : MonoBehaviourSingleton<WaveController>
    {
        #region Internal vars
        private int _waveNumber;
        private int _totalWaveEnemies;
        private int _deaths;

        private WaveAsset _wave;
        #endregion

        #region Initializer & Terminator
        public override void Awake()
        {
            base.Awake();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }
        #endregion

        public void OnEnemyDead()
        {
            if (++this._deaths == this._totalWaveEnemies)
                WaveManager.Instance.RaiseOnWaveFinished(this._waveNumber);
        }

        #region Coroutines
        public IEnumerator StartNextWave(int waveNumber, WaveAsset wave)
        {
            this._waveNumber = waveNumber;
            this._totalWaveEnemies = wave.enemyTypes.Sum(e => e.count);
            this._deaths = 0;
            this._wave = wave;

            yield return this.WaveCoroutine();
        }

        private IEnumerator WaveCoroutine()
        {
            WaveManager.Instance.RaiseOnPrepareForNextWave(this._waveNumber);

            foreach (EnemyWaveData data in this._wave.enemyTypes)
                this.StartCoroutine(this.EnemySpawnCoroutine(data));

            yield return new WaitUntil(() => this._deaths == this._totalWaveEnemies);
        } 

        private IEnumerator EnemySpawnCoroutine(EnemyWaveData data)
        {
            yield return new WaitForSeconds(data.startToSpawnDelay);

            for (int i = 0; i < data.count; i++)
            {
                WaveManager.Instance.GetRandomSpawner().SpawnEnemy(data.enemy.GetType());
            }
        }
        #endregion
    }
}
