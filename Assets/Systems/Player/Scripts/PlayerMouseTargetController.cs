using System;
using UnityEngine;
using VisualStudioEX3.Artemis.Assets.TurretSystem.Controllers;
using VisualStudioEX3.Artemis.Assets.TurretSystem.Controllers.UI;
using VisualStudioEX3.Artemis.Framework.Core.Contracts.Attributes;
using VisualStudioEX3.Artemis.Framework.InputManager.Components;
using VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Models;
using VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Models.Assets;

namespace VisualStudioEX3.Artemis.Assets.Player.Controllers
{
    /// <summary>
    /// Player Mouse Target Controller.
    /// </summary>
    /// <remarks>This components allow to player can click on <see cref="TurretPlacementController"/> objects in scene.</remarks>
    [DisallowMultipleComponent]
    public class PlayerMouseTargetController : MonoBehaviour
    {
        #region Constants
        private const string INPUT_MAP_ASSET_NAME = "TurretPlacement";
        private const string MOUSE_POSITION_INPUT_AXIS_NAME = "Mouse Position";
        private const string MOUSE_CLICK_INPUT_ACTION_NAME = "Mouse Click";
        #endregion

        #region Internal vars
        private PlayerController _playerController;
        private InputAxis _mousePositionAxis;
        private InputAction _mouseClickAction;
        #endregion

        #region Inspector fields
        [SerializeField, Layer]
        private int _targetLayer;
        [SerializeField]
        private float _maxDistance;
        #endregion

        #region Initializers & Terminators
        private void Awake()
        {
            this._playerController = this.GetComponent<PlayerController>();

            this.SubscribeInputEvents(inputMapAssetName: INPUT_MAP_ASSET_NAME,
                mousePositionAxisName: MOUSE_POSITION_INPUT_AXIS_NAME,
                mouseClickActionName: MOUSE_CLICK_INPUT_ACTION_NAME,
                eventListener: this.OnPlayerClick);
        }

        private void OnDestroy() => this.UnsubscribeInputEvents(eventListener: this.OnPlayerClick);
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

        private Ray CreateRayFromCameraToMousePosition() => this._playerController.PlayerCamera.ScreenPointToRay(this._mousePositionAxis);

        private int GetLayerMask() => 1 << this._targetLayer;

        private bool Raycast(out RaycastHit hit) => Physics.Raycast(ray: this.CreateRayFromCameraToMousePosition(), out hit, this._maxDistance, this.GetLayerMask());

        private bool CanOpenTheTurretSelectorDialog() => !TurretSelectorDialogController.Instance.IsOpen;

        private void CreateTurret(Transform turretPlacementTransform)
        {
            if (this.CanOpenTheTurretSelectorDialog())
            {
                TurretPlacementController turretPlacement = turretPlacementTransform.GetComponentInParent<TurretPlacementController>();
                turretPlacement.OpenTurretSelectorDialog();
            }
        }
        #endregion

        #region Event listeners
        private void OnPlayerClick()
        {
            if (this.Raycast(out RaycastHit hit))
                this.CreateTurret(hit.transform);
        }
        #endregion
    }
}
