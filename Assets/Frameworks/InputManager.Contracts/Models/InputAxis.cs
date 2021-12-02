using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Enums;

namespace VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Models
{
    /// <summary>
    /// Input Axis.
    /// </summary>
    /// <remarks>A virtual input axis object that read values from mouse and keyboard bindings.</remarks>
    [Serializable]
    public class InputAxis
    {
        #region Constants
        public const float MIN_SENSITIVITY = 0.05f;
        public const float MAX_SENSITIVITY = 3f;
        public const float DEFAULT_SENSITIVITY = 1f;
        #endregion

        #region Private vars
        private IEnumerable<InputAction> _keyboardBindings;
        #endregion

        #region Public vars
        public string name;
        [Space]

        /// <summary>
        /// Horizontal axis.
        /// </summary>
        [HideInInspector]
        public float x;

        /// <summary>
        /// Vertical axis.
        /// </summary>
        [HideInInspector]
        public float y;

        /// <summary>
        /// Sets the first device to read values when processed this <see cref="InputAxis"/>.
        /// </summary>
        public InputAxisSources inputSourcePriority = InputAxisSources.Mouse;

        /// <summary>
        /// Indicate if this axis uses the mouse input (axis or position) when the gamepad is not is the active input.
        /// </summary>
        public MouseInputModes mouseBehaviour = MouseInputModes.MousePosition;

        [Header("Keyboard navigation")]
        /// <summary>
        /// Keyboard binding for left direction.
        /// </summary>
        public InputAction LeftKey;

        /// <summary>
        /// Keyboard binding for up direction.
        /// </summary>
        public InputAction UpKey;

        /// <summary>
        /// Keyboard binding for right direction.
        /// </summary>
        public InputAction RightKey;

        /// <summary>
        /// Keyboard binding for down direction.
        /// </summary>
        public InputAction DownKey;
        [Space]

        /// <summary>
        /// Invert Y axis.
        /// </summary>
        public bool invertYAxis = false;

        /// <summary>
        /// Axis sensitivity factor.
        /// </summary>
        [Range(InputAxis.MIN_SENSITIVITY, InputAxis.MAX_SENSITIVITY)]
        public float sensitivity = InputAxis.DEFAULT_SENSITIVITY;

        /// <summary>
        /// Normalize axis.
        /// </summary>
        /// <remarks>Use this property to fix the diagonal ranges on 360º/free movements.</remarks>
        [Tooltip("Normalize the diagonal ranges."), Space]
        public bool normalize = true;
        #endregion

        #region Property
        /// <summary>
        /// Gets an enumeration with all <see cref="InputAction"/> bindings in the following order: Left, Up, Right, Down.
        /// </summary>
        public IEnumerable<InputAction> KeyboardBindings
        {
            get
            {
                this._keyboardBindings ??= new InputAction[] { this.LeftKey, this.UpKey, this.RightKey, this.DownKey };
                return this._keyboardBindings;
            }
        }

        /// <summary>
        /// Is the axis activated from keyboard?
        /// </summary>
        public bool IsAxisActivatedFromKeyboard => this.KeyboardBindings.Any(e => e.IsActionRaised);
        #endregion

        #region Operators
        /// <summary>
        /// Cast to <see cref="Vector2"/>.
        /// </summary>
        public static implicit operator Vector2(InputAxis value)
        {
            return new(value.x, value.y);
        }

        /// <summary>
        /// Cast to <see cref="Vector3"/>.
        /// </summary>
        public static implicit operator Vector3(InputAxis value)
        {
            return new(value.x, value.y);
        }
        #endregion

        #region Events
        /// <summary>
        /// Event to notify when the axis has moved.
        /// </summary>
        /// <remarks>The event returns the current axis value.</remarks>
        public event Action<Vector2> OnAxisMove;
        #endregion

        #region Methods & Functions
        public void RaiseOnAxisMoveEvent() => this.OnAxisMove?.Invoke(this);
        #endregion
    }
}
