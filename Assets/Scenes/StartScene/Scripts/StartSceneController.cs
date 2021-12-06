using UnityEngine;
using VisualStudioEX3.Artemis.Assets.EconomySystem.Controllers;
using VisualStudioEX3.Artemis.Assets.LevelManagement;

namespace VisualStudioEX3.Artemis.Assets.Scenes.Controllers
{
    [DisallowMultipleComponent]
    public class StartSceneController : MonoBehaviour
    {
        #region Initializers & Terminators
        private void Awake()
        {
            this.ResetAndLockPlayer();
            this.EnableFloorScrollEffect();
        }

        private void OnDestroy()
        {
            this.UnlockPlayer();
            this.DisableFloorScrollEffect();
        } 
        #endregion

        #region Methods & Functions
        private void ResetAndLockPlayer()
        {
            EnvironmentManagerController.Instance.ResetPlayer();
            EnvironmentManagerController.Instance.LockPlayer();
        }

        private void UnlockPlayer() => EnvironmentManagerController.Instance.UnlockPlayer();

        private void EnableFloorScrollEffect() => EnvironmentManagerController.Instance.EnableFloorScrollMaterialEffect();
        
        private void DisableFloorScrollEffect() => EnvironmentManagerController.Instance.DisableFloorScrollMaterialEffect();

        /// <summary>
        /// Method to start the game from the UI.
        /// </summary>
        public void StartNewGame()
        {
            EconomyManager.Instance.ResetUnits();
            SceneManager.Instance.LoadNextLevel();
        }
        #endregion
    }
}
