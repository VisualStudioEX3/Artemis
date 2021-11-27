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
        private IInputActionProcessor _inputActionProcessor;
        #endregion

        #region Serializable fields
        [SerializeField]
        private InputAction[] _actions;
        #endregion

        #region Initializers
        private void Awake()
        {
            this._inputActionProcessor = InputManagerServiceFactory.Factory.GetService<IInputActionProcessor>();
        }
        #endregion

        #region Update logic
        private void Update()
        {
            this._inputActionProcessor.Update(this._actions);
        }
        #endregion
    }
}
