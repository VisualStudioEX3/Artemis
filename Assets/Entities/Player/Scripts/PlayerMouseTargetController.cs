using System;
using UnityEngine;
using VisualStudioEX3.Artemis.Framework.Core.Contracts;
using VisualStudioEX3.Artemis.Framework.InputManager.Components;
using VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Models;
using VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Models.Assets;

namespace VisualStudioEX3.Artemis.Player.Controllers
{
    [DisallowMultipleComponent]
    public class PlayerMouseTargetController : MonoBehaviour
    {
        #region Constants
        private const string INPUT_MAP_ASSET_NAME = "TurretPlacement";
        private const string MOUSE_POSITION_INPUT_AXIS_NAME = "Mouse Position";
        private const string MOUSE_CLICK_INPUT_ACTION_NAME = "Mouse Click";
        #endregion

        #region Internal vars
        private InputAxis _mousePositionAxis;
        private InputAction _mouseClickAction;
        #endregion

        #region Inspector fields
        [SerializeField]
        private Camera _playerCamera;
        [SerializeField, Layer]
        private int _layerMask;
        [SerializeField]
        private float _maxDistance;
        #endregion

        #region Initializers & Terminators
        private void Awake()
        {
            this.SubscribeInputEvents(inputMapAssetName: INPUT_MAP_ASSET_NAME,
                mousePositionAxisName: MOUSE_POSITION_INPUT_AXIS_NAME,
                mouseClickActionName: MOUSE_CLICK_INPUT_ACTION_NAME,
                eventListener: this.OnPlayerClick);
        }

        private void OnDestroy()
        {
            this.UnsubscribeInputEvents(eventListener: this.OnPlayerClick);
        }
        #endregion

        #region Methods & Functions
        private void SubscribeInputEvents(string inputMapAssetName, string mousePositionAxisName, string mouseClickActionName, Action eventListener)
        {
            InputMapAsset inputMap = InputManager.Instance.GetInputMap(inputMapAssetName);

            this._mousePositionAxis = inputMap.GetAxis(mousePositionAxisName);
            this._mouseClickAction = inputMap.GetAction(mouseClickActionName);
            this._mouseClickAction.OnAction += eventListener;
        }

        private void UnsubscribeInputEvents(Action eventListener) => this._mouseClickAction.OnAction -= eventListener;
        #endregion

        #region Event listeners
        private void OnDrawGizmos()
        {
            
        }

        private void OnPlayerClick()
        {
            Ray ray = this._playerCamera.ScreenPointToRay(this._mousePositionAxis);

            if (Physics.Raycast(ray, out RaycastHit hit, this._maxDistance, 1 << this._layerMask))
            {
                Transform objectHit = hit.transform;
                print(objectHit.name);
            }
        }
        #endregion
    }
}
