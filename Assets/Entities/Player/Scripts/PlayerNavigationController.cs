using UnityEngine;
using VisualStudioEX3.Artemis.Framework.InputManager.Components;
using VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Models;
using VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Models.Assets;

namespace VisualStudioEX3.Artemis
{
    /// <summary>
    /// Player Navigation controller.
    /// </summary>
    /// <remarks>This components controls the player movement through the scene.</remarks>
    [DisallowMultipleComponent]
    public class PlayerNavigationController : MonoBehaviour
    {
        #region Constants
        private const string INPUT_MAP_ASSET_NAME = "Default";
        private const string NAVIGATION_INPUT_AXIS_NAME = "Navigation";

        private const float MIN_MOVEMENT_SPEED = 0.5f;
        private const float MAX_MOVEMENT_SPEED = 10f;
        private const float DEFAULT_MOVEMENT_SPEED = 1f;
        #endregion

        #region Internal vars
        InputAxis _navigationAxis;
        #endregion

        #region Inspector fields
        [SerializeField, Range(MIN_MOVEMENT_SPEED, MAX_MOVEMENT_SPEED)]
        private float _movementSpeed = DEFAULT_MOVEMENT_SPEED;
        #endregion

        #region Initializers & Terminators
        private void Awake()
        {
            this.SubscribeOnPlayerMovesEvent();
        }

        private void OnDestroy()
        {
            this.UnsubscribeOnPlayerMovesEvent();
        }
        #endregion

        #region Methods & Functions
        private void SubscribeOnPlayerMovesEvent()
        {
            InputMapAsset inputMap = InputManager.Instance.GetInputMap(INPUT_MAP_ASSET_NAME);
            this._navigationAxis = inputMap.GetAxis(NAVIGATION_INPUT_AXIS_NAME);
            this._navigationAxis.OnAxisMove += this.OnPlayerMoves;
        }

        private void UnsubscribeOnPlayerMovesEvent() => this._navigationAxis.OnAxisMove -= this.OnPlayerMoves;

        private void MovePlayer(Vector2 delta)
        {
            // TODO: Control the bounds of the scenary to avoid the player moves outside.
            this.transform.position += this._movementSpeed * Time.deltaTime * (Vector3)delta;
        }
        #endregion

        #region Event listeners
        private void OnPlayerMoves(Vector2 delta)
        {
            this.MovePlayer(delta);
        }
        #endregion
    }
}
