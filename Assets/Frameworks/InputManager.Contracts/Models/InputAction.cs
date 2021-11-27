using System;
using UnityEngine;
using VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Enums;

namespace VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Models
{
    /// <summary>
    /// Defines an input action setup.
    /// </summary>
    [Serializable]
    public class InputAction
    {
        #region Internal vars
        private bool _isPressed;
        private bool _isDown;
        private bool _isUp; 
        #endregion

        #region Public vars
        /// <summary>
        /// Name of the action.
        /// </summary>
        public string name;

        /// <summary>
        /// Spected key action state.
        /// </summary>
        public KeyStates state;

        /// <summary>
        /// Primary binding key.
        /// </summary>
        public KeyboardMouseCodes primaryKey;

        /// <summary>
        /// Secondary binding key.
        /// </summary>
        public KeyboardMouseCodes secondaryKey;
        #endregion

        #region Properties
        /// <summary>
        /// Is any of the bindings are pressed?
        /// </summary>
        [HideInInspector]
        public bool IsPressed
        {
            get => this._isPressed;
            set
            {
                this._isDown = this._isUp = this._isPressed;
                this._isPressed = value;
            }
        }

        /// <summary>
        /// Is any of the bindings are down?
        /// </summary>
        [HideInInspector]
        public bool IsDown => !this._isDown && this._isPressed;

        /// <summary>
        /// Is any of the bindings are up or released?
        /// </summary>
        [HideInInspector]
        public bool IsUp => this._isUp && !this._isPressed;
        #endregion
    }
}
