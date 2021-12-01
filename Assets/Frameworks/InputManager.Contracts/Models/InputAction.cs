using System;
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
        public bool IsDown => !this._isDown && this._isPressed;

        /// <summary>
        /// Is any of the bindings are up or released?
        /// </summary>
        public bool IsUp => this._isUp && !this._isPressed;

        /// <summary>
        /// Is the action raised?
        /// </summary>
        public bool IsActionRaised => (this._isPressed && state == KeyStates.Pressed) || 
                                      (this._isDown && state == KeyStates.Down) || 
                                      (this._isUp && state == KeyStates.Up);
        #endregion

        #region Events
        /// <summary>
        /// Event to notify when the action is raised.
        /// </summary>
        public event Action OnAction;
        #endregion

        #region Methods & Functions
        public void RaiseOnActionEvent() => this.OnAction?.Invoke(); 
        #endregion
    }
}
