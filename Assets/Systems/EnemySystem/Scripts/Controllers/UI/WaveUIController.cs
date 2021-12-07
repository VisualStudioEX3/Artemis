using System;
using System.Collections;
using TMPro;
using UnityEngine;
using VisualStudioEX3.Artemis.Assets.Common.Controllers.UI;
using VisualStudioEX3.Artemis.Assets.LevelManagement;
using VisualStudioEX3.Artemis.Assets.Scenes.Controllers;

namespace VisualStudioEX3.Artemis.Assets.EnemySystem.Controllers.UI
{
    public class WaveUIController : MonoBehaviour
    {
        #region Inspector fields
        [Header("Topbar label fields"), SerializeField]
        private UILabelFieldController _level;
        [SerializeField]
        private UILabelFieldController _wave;
        [SerializeField]
        private UILabelFieldController _timeBeforeFirstWave;

        [Header("Messages on screen"), SerializeField]
        private WaveStartsWarningController _waveStartsWarning;
        #endregion

        private void Awake() => GameManagerController.OnGameManagerIsIntialized += this.OnGameManagerIsInitialized;

        private void OnDestroy()
        {
            this.UnsubscribeManagerEvents();
            this.UnsubscribeWaveEvents();
        }

        #region Methods & Functions
        private void SubscribeSceneManagerEvents()
        {
            SceneManager.Instance.OnLoadSceneFinished += this.OnSceneLoaded;
            SceneManager.Instance.OnUnloadSceneStarts += this.OnSceneUnload;
        }

        private void UnsubscribeManagerEvents()
        {
            SceneManager.Instance.OnLoadSceneFinished -= this.OnSceneLoaded;
            SceneManager.Instance.OnUnloadSceneStarts -= this.OnSceneUnload;
        }

        private bool HasWaveManager() => WaveManager.Instance is not null;

        private void SubscribeWaveEvents()
        {
            if (this.HasWaveManager())
                WaveManager.Instance.OnWaveStart += this.OnWaveStarts;
        }

        private void UnsubscribeWaveEvents()
        {
            if (this.HasWaveManager())
                WaveManager.Instance.OnWaveStart -= this.OnWaveStarts;
        }

        private string GetLevelNumberString() => (SceneManager.Instance.CurrentLevelIndex + 1).ToString();

        private string GetWaveNumberString(int waveNumber) => waveNumber.ToString();

        private void UpdateLevelValue() => this._level.Value = this.GetLevelNumberString();

        private void UpdateWaveValue(int waveNumber) => this._wave.Value = this.GetWaveNumberString(waveNumber);

        private string FormatTime(int seconds) => TimeSpan.FromSeconds(seconds).ToString(@"mm\:ss");

        private void UpdateTimeBeforeFirstWaveValue(int seconds) => this._timeBeforeFirstWave.Value = this.FormatTime(seconds);

        private void ShowTimeBeforeFirstWaveField() => this._timeBeforeFirstWave.gameObject.SetActive(true);

        private void HideTimeBeforeFirstWaveField() => this._timeBeforeFirstWave.gameObject.SetActive(false);

        private void DisplayTimeCountDownToStartFirstWave() => this.StartCoroutine(this.DisplayTimeUntilStartTheFirstWaveCoroutine(WaveManager.Instance.TimeBeforeStartFirstWave));

        private void ShowWaveStartsWarning(int waveNumber) => this._waveStartsWarning.Play(waveNumber);
        #endregion

        #region Event listeners
        private void OnGameManagerIsInitialized() => this.SubscribeSceneManagerEvents();

        private void OnSceneLoaded()
        {
            this.SubscribeWaveEvents();
            
            if (this.HasWaveManager())
            {
                this.UpdateLevelValue();
                this.UpdateWaveValue(1);
                this.DisplayTimeCountDownToStartFirstWave();
            }
        }

        private void OnSceneUnload() => this.UnsubscribeWaveEvents();

        private void OnWaveStarts(int waveNumber)
        {
            this.ShowWaveStartsWarning(waveNumber);
            this.UpdateWaveValue(waveNumber);
        }
        #endregion

        #region Coroutines
        private IEnumerator DisplayTimeUntilStartTheFirstWaveCoroutine(int time)
        {
            var waitSecondYield = new WaitForSeconds(1f);

            this.ShowTimeBeforeFirstWaveField();

            for (int secondsLeft = time; secondsLeft >= 0; secondsLeft--)
            {
                this.UpdateTimeBeforeFirstWaveValue(secondsLeft);
                yield return waitSecondYield;
            }

            this.HideTimeBeforeFirstWaveField();
        } 
        #endregion
    }
}
