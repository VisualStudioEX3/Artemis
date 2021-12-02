using UnityEngine;
using VisualStudioEX3.Artemis.Framework.Core.Components;
using VisualStudioEX3.Artemis.Assets.Player.Controllers;
using VisualStudioEX3.Artemis.Framework.Core.Components.Materials;

namespace VisualStudioEX3.Artemis.Assets.Scenes.Controllers
{
    /// <summary>
    /// Manager to control components related with the environment level.
    /// </summary>
    /// <remarks>The environment level contains the background box scene, with the floor scroll effect controlle, and the Player entity.</remarks>
    [DisallowMultipleComponent]
    public class EnvironmentManagerController : MonoBehaviourSingleton<EnvironmentManagerController>
    {
        #region Internal vars
        private PlayerController _player;
        private PlayerNavigationController _playerNavigationController;
        private PlayerZoomController _playerZoomController;
        
        private ScrollMaterial _floorScrollMaterialController;
        #endregion

        #region Initializers & Terminators
        public override void Awake()
        {
            base.Awake();

            this.GetPlayerComponents();
            this.GetFloorScrollMaterialController();
        }

        public override void OnDestroy() => base.OnDestroy();
        #endregion

        #region Methods & Functions
        public override bool IsPersistentBetweenScenes() => true;

        private void GetPlayerComponents()
        {
            this._player = this.GetComponentInChildren<PlayerController>();

            this._playerNavigationController = this._player.GetComponent<PlayerNavigationController>();
            this._playerZoomController = this._player.GetComponent<PlayerZoomController>();
        }

        private void GetFloorScrollMaterialController() => this._floorScrollMaterialController = this.GetComponentInChildren<ScrollMaterial>();

        /// <summary>
        /// Resets Player's position and zoom level.
        /// </summary>
        public void ResetPlayer()
        {
            this._playerZoomController.transform.position = Vector3.zero;
            this._playerZoomController.ResetZoomToDefault();
        }

        private void EnablePlayer(bool enable) => this._playerNavigationController.enabled = this._playerZoomController.enabled = enable;

        /// <summary>
        /// Locks Player's movement and zoom.
        /// </summary>
        public void LockPlayer() => this.EnablePlayer(false);

        /// <summary>
        /// Unlocks Player's movement and zoom.
        /// </summary>
        public void UnlockPlayer() => this.EnablePlayer(true);

        /// <summary>
        /// Enables the floor scroll material effect.
        /// </summary>
        public void EnableFloorScrollMaterialEffect() => this._floorScrollMaterialController.enabled = true;

        /// <summary>
        /// Disables the floor scroll material effect.
        /// </summary>
        public void DisableFloorScrollMaterialEffect()
        {
            this._floorScrollMaterialController.ResetScroll();
            this._floorScrollMaterialController.enabled = false;
        }
        #endregion
    }
}
