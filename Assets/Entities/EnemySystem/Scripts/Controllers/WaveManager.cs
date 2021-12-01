using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VisualStudioEX3.Artemis.Assets.EnemySystem.Constants;
using VisualStudioEX3.Artemis.Assets.EnemySystem.Models;
using VisualStudioEX3.Artemis.Assets.EnemySystem.Services;
using VisualStudioEX3.Artemis.Framework.Core.Components;

namespace VisualStudioEX3.Artemis.Assets.EnemySystem.Controllers
{
    /// <summary>
    /// Wave Manager.
    /// </summary>
    /// <remarks>This component manages each enemy wave. It's accessible as singleton instance from others scripts.</remarks>
    public class WaveManager : MonoBehaviourSingleton<WaveManager>
    {
        #region Inspector fields
        [SerializeField]
        private WaveAsset[] _waves;

        [SerializeField]
        private EnemySpawnerController[] _spawnLocations;

        [SerializeField, Tooltip(TooltipMessageConstants.TIME_IN_SECONDS_TOOLTIP_MESSAGE)]
        private float _timeBeforeStartFirstWave;
        #endregion

        #region Properties
        public IReadOnlyList<EnemySpawnerController> SpawnLocations => this._spawnLocations;
        #endregion

        #region Events
        /// <summary>
        /// Notifies the time to wait to start the next wave.
        /// </summary>
        public event Action<int> OnPrepareForNextWave;

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
            base.Awake();
            this.CreateEnemyInstances();
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(this._timeBeforeStartFirstWave);

            int waveNumber = 0;
            foreach (WaveAsset wave in this._waves)
                yield return WaveController.Instance.StartNextWave(++waveNumber, wave);

            this.OnAllWavesCompleted?.Invoke();
        }

        public override void OnDestroy() => base.OnDestroy();
        #endregion

        #region Methods & Functions
        public void RaiseOnPrepareForNextWave(int waveNumber) => this.OnPrepareForNextWave?.Invoke(waveNumber);

        public void RaiseOnWaveFinished(int waveNumber) => this.OnWaveFinished?.Invoke(waveNumber);
        
        public void RaiseOnAllWavesCompleted() => this.OnAllWavesCompleted?.Invoke();
        
        private void CreateEnemyInstances() => EnemyControllerFactory.Instance.GenerateInstances(this._waves);
        
        private int GetRandomIndex(int lenght) => UnityEngine.Random.Range(0, lenght);
        
        /// <summary>
        /// Gets randomly any of the <see cref="EnemySpawnerController"/> instances.
        /// </summary>
        /// <returns>Returns a <see cref="EnemySpawnerController"/> instance.</returns>
        public EnemySpawnerController GetRandomSpawner() => this._spawnLocations[this.GetRandomIndex(this._spawnLocations.Length)];
        #endregion
    }
}
