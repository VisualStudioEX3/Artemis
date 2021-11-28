using UnityEngine;
using VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Interfaces;
using VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Models;
using VisualStudioEX3.Artemis.Framework.InputManager.ServiceProvider;

namespace VisualStudioEX3.Artemis.Framework.InputManager.Components
{
    /// <summary>
    /// Input Manager component.
    /// </summary>
    /// <remarks>This component manages all game input states.</remarks>
    public class InputManager : MonoBehaviour
    {
        #region Internal vars
        private IInputAxisProcessor _inputAxisProcessor;
        private IInputActionProcessor _inputActionProcessor;
        #endregion

        #region Serializable fields
        [SerializeField]
        private InputAxis[] _axes;
        [SerializeField]
        private InputAction[] _actions;
        #endregion

        #region Initializers
        private void Awake()
        {
            InputManagerServiceFactory serviceFactory = InputManagerServiceFactory.Factory;

            this._inputAxisProcessor = serviceFactory.GetService<IInputAxisProcessor>();
            this._inputActionProcessor = serviceFactory.GetService<IInputActionProcessor>();
        }
        #endregion

        #region Update logic
        private void Update()
        {
            this._inputAxisProcessor.Update(this._axes);
            this._inputActionProcessor.Update(this._actions);
        }
        #endregion
    }
}
