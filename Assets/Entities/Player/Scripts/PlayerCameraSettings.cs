using UnityEngine;

namespace VisualStudioEX3.Artemis.Player.Controllers
{
    /// <summary>
    /// Player Camera Settings component.
    /// </summary>
    /// <remarks>This components allow to setup the camera position and angle edit mode.
    /// The camera faces always to the root Player gameobject position. 
    /// Moves the camera in height (local Y axis) and distance (local Z axis) to setup the camera angle.</remarks>
    [DisallowMultipleComponent, ExecuteInEditMode]
    public class PlayerCameraSettings : MonoBehaviour
    {
        #region Constants
        private const float MIN_CAMERA_HEIGHT = 5f;
        private const float MAX_CAMERA_HEIGHT = 20f;
        private const float DEFAULT_CAMERA_HEIGHT = 7.5f;

        private const float MIN_CAMERA_DISTANCE = -5f;
        private const float MAX_CAMERA_DISTANCE = -10f;
        private const float DEFAULT_CAMERA_DISTANCE = -7.5f;
        #endregion

        #region Inspector fields
        [SerializeField]
        private Camera _playerCamera;

        [SerializeField, Header("Camera position and rotation"), Tooltip("Y axis."), Range(MIN_CAMERA_HEIGHT, MAX_CAMERA_HEIGHT)]
        private float _cameraHeight = DEFAULT_CAMERA_HEIGHT;

        [SerializeField, Tooltip("Z axis."), Range(MIN_CAMERA_DISTANCE, MAX_CAMERA_DISTANCE)]
        private float _cameraDistance = DEFAULT_CAMERA_DISTANCE;
        #endregion

        #region Methods & Functions
        private void UpdateCameraPosition()
        {
            Quaternion localRotation = this._playerCamera.transform.localRotation;
            var position = new Vector3(0f, this._cameraHeight, this._cameraDistance);

            this._playerCamera.transform.localRotation = Quaternion.identity;
            this._playerCamera.transform.localPosition = position;
            this._playerCamera.transform.localRotation = localRotation;
        }

        private void UpdateCameraForward()
        {
            this._playerCamera.transform.forward = this.transform.position - this._playerCamera.transform.position;
        }
        #endregion

        #region Event listeners
        private void OnValidate()
        {
            if (!Application.isPlaying)
            {
                this.UpdateCameraPosition();
                this.UpdateCameraForward();
            }
        }

        private void OnDrawGizmos()
        {
            // Draws, in editor mode, the direction from the camera to the gameObject root.
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(this._playerCamera.transform.position, this.transform.position);
        }
        #endregion
    }
}
