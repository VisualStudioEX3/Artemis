using UnityEngine;
using VisualStudioEX3.Artemis.Framework.Core.Components;
using VisualStudioEX3.Artemis.Assets.Player.Controllers;

namespace VisualStudioEX3.Artemis.SceneController
{
    public class EnvironmentManagerController : MonoBehaviourSingleton<EnvironmentManagerController>
    {
        #region Internal vars
        private PlayerController _player; 
        private PlayerZoomController _playerZoomController;
        #endregion

        #region Initializers
        public override void Awake()
        {
            base.Awake();
            this.GetPlayerComponents();
        }

        public override void OnDestroy() => base.OnDestroy();
        #endregion

        #region Methods & Functions
        public override bool IsPersistentBetweenScenes() => true;

        private void GetPlayerComponents()
        {
            this._player = this.GetComponentInChildren<PlayerController>();
            this._playerZoomController = this._player.GetComponent<PlayerZoomController>();
        }

        public void ResetPlayer()
        {
            this._playerZoomController.transform.position = Vector3.zero;
            this._playerZoomController.ResetZoomToDefault();
        }
        #endregion
    }
}
