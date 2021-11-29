using System.Collections.Generic;
using UnityEngine;
using VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Enums;
using VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Interfaces;
using VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Models;

namespace VisualStudioEX3.Artemis.Framework.InputManager.Services.Processors
{
    public class UnityLegacyInputAxisProcessor : IInputAxisProcessor
    {
        #region Internal vars
        private IMousePositionAxesProcessor _mousePositionAxesProcessor;
        private IInputActionProcessor _inputActionProcessor;
        #endregion

        #region Properties
        public bool IsMouseMoving => this._mousePositionAxesProcessor.IsMouseMoving; 
        #endregion

        #region Constructor
        public UnityLegacyInputAxisProcessor(IMousePositionAxesProcessor mousePositionAxesProcessor,
                                             IInputActionProcessor inputActionProcessor)
        {
            this._mousePositionAxesProcessor = mousePositionAxesProcessor;
            this._inputActionProcessor = inputActionProcessor;
        }
        #endregion

        #region Methods & Functions
        public void Update(IEnumerable<InputAxis> axes)
        {
            this._mousePositionAxesProcessor.Update();

            foreach (InputAxis axis in axes)
                this.ProcessAxis(axis);
        }

        private void ProcessAxis(InputAxis axis)
        {
            Vector2 result = this.ReadFirstDevice(axis);

            if (result == Vector2.zero)
                result = this.ReadSecondDevice(axis);

            if (result != Vector2.zero)
            {
                if (axis.invertYAxis)
                    result.y *= -1;

                if (axis.normalize && result.sqrMagnitude > 1f)
                    result.Normalize();
            }

            axis.x = result.x;
            axis.y = result.y;

            if (result != Vector2.zero)
                axis.RaiseOnAxisMoveEvent();
        }

        private Vector2 ReadFirstDevice(InputAxis axis)
        {
            return axis.inputSourcePriority == InputAxisSources.Mouse 
                ? this.ProcessMouseAxis(axis) 
                : this.ProcessKeyboardAxis(axis);
        }

        private Vector2 ReadSecondDevice(InputAxis axis)
        {
            return axis.inputSourcePriority == InputAxisSources.Mouse 
                ? this.ProcessKeyboardAxis(axis) 
                : this.ProcessMouseAxis(axis);
        }

        private Vector2 ProcessMouseAxis(InputAxis axis)
        {
            Vector2 result = axis.mouseBehaviour switch
            {
                MouseInputModes.MousePosition => this._mousePositionAxesProcessor.MousePosition,
                MouseInputModes.MouseAxis => this._mousePositionAxesProcessor.MouseAxis,
                MouseInputModes.MouseDelta => this._mousePositionAxesProcessor.MouseDelta,
                _ => Vector2.zero,
            };

            return result * axis.sensitivity;
        }

        private Vector2 ProcessKeyboardAxis(InputAxis axis)
        {
            Vector2 result = Vector2.zero;

            this._inputActionProcessor.Update(new[] { axis.LeftKey, axis.UpKey, axis.RightKey, axis.DownKey });

            if (axis.LeftKey.IsPressed)
                result.x = -axis.sensitivity;
            else if (axis.RightKey.IsPressed)
                result.x = axis.sensitivity;

            if (axis.UpKey.IsPressed)
                result.y = axis.sensitivity;
            else if (axis.DownKey.IsPressed)
                result.y = -axis.sensitivity;

            return result;
        }
        #endregion
    }
}
