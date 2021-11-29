using System;
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
        private const string INPUT_MAP_ASSET_NAME = "Navigation";
        private const string MOUSE_NAVIGATION_INPUT_AXIS_NAME = "Mouse Navigation";
        private const string KEYBOARD_NAVIGATION_INPUT_AXIS_NAME = "Keyboard Navigation";
        private const string MOUSE_NAVIGATION_INPUT_ACTION_NAME = "Enable Mouse Navigation";
        private const string KEYBOARD_NAVIGATION_INPUT_ACTION_NAME = "Enable Keyboard Navigation";

        private const float MIN_MOVEMENT_SPEED = 0.5f;
        private const float MAX_MOVEMENT_SPEED = 10f;
        private const float DEFAULT_MOVEMENT_SPEED = 1f;
        #endregion

        #region Internal vars
        InputAxis _mouseNavigationAxis;
        InputAxis _keyboardNavigationAxis;
        InputAction _mouseNavigationAction;
        InputAction _keyboardNavigationAction;
        #endregion

        #region Inspector fields
        [SerializeField, Range(MIN_MOVEMENT_SPEED, MAX_MOVEMENT_SPEED)]
        private float _movementSpeed = DEFAULT_MOVEMENT_SPEED;
        #endregion

        #region Initializers & Terminators
        private void Awake()
        {
            this.SubscribeOnNavigationEvent(eventListener: this.OnMouseNavigation,
                inputAxis: out this._mouseNavigationAxis,
                inputAction: out this._mouseNavigationAction,
                inputMapAssetName: INPUT_MAP_ASSET_NAME,
                inputAxisName: MOUSE_NAVIGATION_INPUT_AXIS_NAME,
                inputActionName: MOUSE_NAVIGATION_INPUT_ACTION_NAME);

            this.SubscribeOnNavigationEvent(eventListener: this.OnKeyboardNavigation,
                inputAxis: out this._keyboardNavigationAxis,
                inputAction: out this._keyboardNavigationAction,
                inputMapAssetName: INPUT_MAP_ASSET_NAME,
                inputAxisName: KEYBOARD_NAVIGATION_INPUT_AXIS_NAME,
                inputActionName: KEYBOARD_NAVIGATION_INPUT_ACTION_NAME);
        }

        private void OnDestroy()
        {
            this.UnsubscribeOnNavigationEvent(eventListener: this.OnMouseNavigation, inputAxis: this._mouseNavigationAxis);
            this.UnsubscribeOnNavigationEvent(eventListener: this.OnKeyboardNavigation, inputAxis: this._keyboardNavigationAxis);
        }
        #endregion

        #region Methods & Functions
        private void SubscribeOnNavigationEvent(Action<Vector2> eventListener, 
            out InputAxis inputAxis,
            out InputAction inputAction, 
            string inputMapAssetName, 
            string inputAxisName, 
            string inputActionName)
        {
            InputMapAsset inputMap = InputManager.Instance.GetInputMap(inputMapAssetName);

            inputAxis = inputMap.GetAxis(inputAxisName);
            inputAction = inputMap.GetAction(inputActionName);

            inputAxis.OnAxisMove += eventListener;
        }

        private void UnsubscribeOnNavigationEvent(Action<Vector2> eventListener, InputAxis inputAxis)
        {
            inputAxis.OnAxisMove -= eventListener;
        }

        private void MovePlayer(Vector2 delta)
        {
            // TODO: Control the bounds of the scenary to avoid the player moves outside.
            this.transform.position += this._movementSpeed * Time.deltaTime * new Vector3(delta.x, 0f, delta.y);
        }
        #endregion

        #region Event listeners
        private void OnMouseNavigation(Vector2 delta)
        {  
            if (this._mouseNavigationAction.IsPressed)
                this.MovePlayer(delta);
        }

        private void OnKeyboardNavigation(Vector2 delta)
        {
            if (this._keyboardNavigationAction.IsPressed)
                this.MovePlayer(delta);
        }
        #endregion
    }
}
