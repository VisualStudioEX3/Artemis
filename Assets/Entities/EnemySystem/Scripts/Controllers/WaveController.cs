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
        private int _waveNumber;
        private int _totalWaveEnemies;
        private int _deaths;

        private WaveAsset _wave;

        private WaveManager _waveManager;
        #endregion

        private void Awake() => this._waveManager = this.GetComponent<WaveManager>();

        #region Event listernes
        public void OnEnemyDead()
        {
            if (++this._deaths == this._totalWaveEnemies)
                this._waveManager.RaiseOnWaveFinished(this._waveNumber);
        }
        #endregion

        #region Coroutines
        public IEnumerator StartNextWaveCoroutine(int waveNumber, WaveAsset wave)
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
                this._waveManager.GetRandomSpawner().SpawnEnemy(data.enemy.GetType());
        }
        #endregion
    }
}
