using UnityEngine;
using VisualStudioEX3.Artemis.Framework.InputManager.Components;
using VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Models;
using VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Models.Assets;

namespace VisualStudioEX3.Artemis.Assets.Player.Controllers
{
    /// <summary>
    /// Player Zoom controller.
    /// </summary>
    /// <remarks>This components controls the player camera zoom.</remarks>
    [DisallowMultipleComponent]
    public class PlayerZoomController : MonoBehaviour
    {
        #region Constants
        private const string INPUT_MAP_ASSET_NAME = "Navigation";
        private const string ZOOM_IN_INPUT_ACTION_NAME = "Zoom In";
        private const string ZOOM_OUT_INPUT_ACTION_NAME = "Zoom Out";

        private const float MIN_ZOOM_DISTANCE = 5f;
        private const float MAX_ZOOM_DISTANCE = 20f;
        private const float DEFAULT_ZOOM_DISTANCE = 10f;

        private const float MIN_ZOOM_SPEED = 1f;
        private const float MAX_ZOOM_SPEED = 10f;
        private const float DEFAULT_ZOOM_SPEED = 5f;
        #endregion

        #region Internal vars
        private PlayerController _playerController;

        private Vector3 _minZoomPosition;
        private Vector3 _maxZoomPosition;

        private InputAction _zoomInAction;
        private InputAction _zoomOutAction;
        #endregion

        #region Inspector fields
        [SerializeField, Range(MIN_ZOOM_SPEED, MAX_ZOOM_SPEED)]
        private float _zoomSpeed = DEFAULT_ZOOM_SPEED;
        #endregion

        #region Properties
        private Transform PlayerCameraTransform => this._playerController.PlayerCamera.transform; 
        #endregion

        #region Initializers
        private void Awake()
        {
            this._playerController = this.GetComponent<PlayerController>();

            this.SubscribeInputEvents(inputMapAssetName: INPUT_MAP_ASSET_NAME,
                zoomInActionName: ZOOM_IN_INPUT_ACTION_NAME,
                zoomOutActionName: ZOOM_OUT_INPUT_ACTION_NAME);

            this.ResetZoomToDefault();
        }

        private void OnDestroy() => this.UnsubscribeInputEvents();
        #endregion

        #region Methods & Functions
        private void SubscribeInputEvents(string inputMapAssetName, string zoomInActionName, string zoomOutActionName)
        {
            InputMapAsset inputMap = InputManager.Instance.GetInputMap(inputMapAssetName);

            this._zoomInAction = inputMap.GetAction(zoomInActionName);
            this._zoomInAction.OnAction += this.OnZoomIn;

            this._zoomOutAction = inputMap.GetAction(zoomOutActionName);
            this._zoomOutAction.OnAction += this.OnZoomOut;
        }

        private void UnsubscribeInputEvents()
        {
            this._zoomInAction.OnAction -= this.OnZoomIn;
            this._zoomOutAction.OnAction -= this.OnZoomOut;
        }

        private Vector3 CalculateStep() => this.PlayerCameraTransform.forward * this._zoomSpeed * Time.deltaTime;

        private void ZoomInStep()
        {
            this.PlayerCameraTransform.position += this.CalculateStep();
            this.ClampZoom();
        }

        private void ZoomOutStep()
        {
            this.PlayerCameraTransform.position -= this.CalculateStep();
            this.ClampZoom();
        }

        private void CalculateClampedVectors()
        {
            this._minZoomPosition = this.CalculatePositionFromFloorByDistance(MIN_ZOOM_DISTANCE);
            this._maxZoomPosition = this.CalculatePositionFromFloorByDistance(MAX_ZOOM_DISTANCE);
        }

        private Vector3 CalculatePositionFromFloorByDistance(float distance) => this.transform.position + (-this.PlayerCameraTransform.forward * distance);

        private float GetDistanceToFloor() => Vector3.Distance(this.PlayerCameraTransform.position, this.transform.position);

        private void ClampZoom()
        {
            Vector3 position = this.PlayerCameraTransform.position;
            float distance = this.GetDistanceToFloor();

            this.CalculateClampedVectors();

            if (distance < MIN_ZOOM_DISTANCE)
                position = this._minZoomPosition;
            else if (distance > MAX_ZOOM_DISTANCE)
                position = this._maxZoomPosition;

            this.PlayerCameraTransform.position = position;
        }

        /// <summary>
        /// Resets the camera zoom factor to the default factor.
        /// </summary>
        public void ResetZoomToDefault() => this.PlayerCameraTransform.position = this.CalculatePositionFromFloorByDistance(DEFAULT_ZOOM_DISTANCE);
        #endregion

        #region Event listeners
        private void OnZoomIn() => this.ZoomInStep();

        private void OnZoomOut() => this.ZoomOutStep();
        #endregion
    }
}
