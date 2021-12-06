using UnityEngine;
using VisualStudioEX3.Artemis.Assets.Common.Controllers.UI;
using VisualStudioEX3.Artemis.Assets.LevelManagement;
using VisualStudioEX3.Artemis.Assets.Scenes.Controllers;

namespace VisualStudioEX3.Artemis.Assets.EnemySystem.Controllers.UI
{
    public class WaveUIController : MonoBehaviour
    {
        #region Inspector fields
        [SerializeField]
        private UILabelFieldController _level;
        [SerializeField]
        private UILabelFieldController _wave;
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
        #endregion

        #region Event listeners
        private void OnGameManagerIsInitialized() => this.SubscribeSceneManagerEvents();

        private void OnSceneLoaded()
        {
            this.SubscribeWaveEvents();
            this.UpdateLevelValue();
            this.UpdateWaveValue(1);
        }

        private void OnSceneUnload() => this.UnsubscribeWaveEvents();

        private void OnWaveStarts(int waveNumber) => this._wave.Value = this.GetWaveNumberString(waveNumber);
        #endregion
    }
}
