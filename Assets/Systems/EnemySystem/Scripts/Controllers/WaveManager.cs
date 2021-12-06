using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VisualStudioEX3.Artemis.Assets.EnemySystem.Constants;
using VisualStudioEX3.Artemis.Assets.EnemySystem.Models;
using VisualStudioEX3.Artemis.Assets.EnemySystem.Services;
using VisualStudioEX3.Artemis.Assets.LevelManagement;
using VisualStudioEX3.Artemis.Framework.Core.Components;

namespace VisualStudioEX3.Artemis.Assets.EnemySystem.Controllers
{
    /// <summary>
    /// Wave Manager.
    /// </summary>
    /// <remarks>This component manages each enemy wave. It's accessible as singleton instance from others scripts.</remarks>
    public class WaveManager : MonoBehaviourSingleton<WaveManager>
    {
        #region Internal vars
        private WaveController _waveController; 
        #endregion

        #region Inspector fields
        [SerializeField]
        private WaveAsset[] _waves;

        [SerializeField, Tooltip(TooltipMessageConstants.TIME_IN_SECONDS_TOOLTIP_MESSAGE)]
        private float _timeBeforeStartFirstWave;
        #endregion

        #region Events
        /// <summary>
        /// Notifies the time to wait to start the first wave.
        /// </summary>
        public event Action<float> OnPrepareForFirstWave;

        /// <summary>
        /// Notifies when a enemy wave starts.
        /// </summary>
        public event Action<int> OnWaveStart;

        /// <summary>
        /// Notifies when a enemy wave is finished.
        /// </summary>
        public event Action<int> OnWaveFinished;

        /// <summary>
        /// Notifies when all waves are completed.
        /// </summary>
        public event Action OnAllWavesCompleted;
        #endregion

        #region Initializers & Terminators
        public override void Awake()
        {
            this._waveController = this.GetComponent<WaveController>();

            base.Awake();
            this.CreateEnemyInstances();
        }

        private IEnumerator Start()
        {
            this.OnPrepareForFirstWave?.Invoke(this._timeBeforeStartFirstWave);
            yield return new WaitForSeconds(this._timeBeforeStartFirstWave);

            int waveNumber = 0;
            foreach (WaveAsset wave in this._waves)
            {
                this.OnWaveStart(++waveNumber);
                yield return this._waveController.StartNextWaveCoroutine(wave);
                this.OnWaveFinished?.Invoke(waveNumber);
            }

            this.OnAllWavesCompleted?.Invoke();
        }

        public override void OnDestroy() => base.OnDestroy();
        #endregion

        #region Methods & Functions
        private void CreateEnemyInstances() => EnemyControllerFactory.Instance.GenerateInstances(this._waves);
        
        private int GetRandomIndex(int lenght) => UnityEngine.Random.Range(0, lenght);

        /// <summary>
        /// Gets randomly any of the <see cref="EnemySpawnerController"/> instances.
        /// </summary>
        /// <returns>Returns an <see cref="EnemySpawnerController"/> instance.</returns>
        public EnemySpawnerController GetRandomEnemySpawnLocation()
        {
            IReadOnlyList<EnemySpawnerController> spawnLocations = LevelManagerController.Instance.EnemySpawnLocations;

            return spawnLocations[this.GetRandomIndex(spawnLocations.Count)];
        }

        /// <summary>
        /// Gets all active <see cref="EnemyController"/> instances in scene.
        /// </summary>
        /// <returns>Returns an enumeration with all active <see cref="EnemyController"/> instances in scene.</returns>
        public IEnumerable<EnemyController> GetAllActiveEnemies() => EnemyControllerFactory.Instance.GetAllActiveEnemies();
        #endregion
    }
}
