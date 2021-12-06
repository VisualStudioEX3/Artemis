using UnityEngine;

namespace VisualStudioEX3.Artemis.Assets.Player.Controllers
{
    /// <summary>
    /// Player controller.
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        #region Internal vars
        private PlayerZoomController _playerZoomController;
        #endregion

        #region Inspector fields
        [SerializeField]
        private Camera _playerCamera; 
        #endregion

        #region Properties
        public Camera PlayerCamera => this._playerCamera;
        #endregion

        #region Methods & Functions
        private void Awake()
        {
            this._playerZoomController = this.GetComponent<PlayerZoomController>();
            this._playerZoomController.ResetZoomToDefault();
        } 
        #endregion
    }
}
