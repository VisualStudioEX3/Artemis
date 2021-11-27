using System.Collections.Generic;
using VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Interfaces;
using VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Models;

namespace VisualStudioEX3.Artemis.Framework.InputManager.Services.Processors
{
    public class UnityLegacyInputActionsProcessor : IInputActionProcessor
    {
        #region Internal vars
        private readonly IKeyboardMouseProcessor _keyboardMouseProcessor;
        #endregion

        #region Constructor
        public UnityLegacyInputActionsProcessor(IKeyboardMouseProcessor keyboardMouseProcessor)
        {
            this._keyboardMouseProcessor = keyboardMouseProcessor;
        }
        #endregion

        #region Methods & Functions
        public void Update(IEnumerable<InputAction> actions)
        {
            foreach (InputAction action in actions)
                this.ProcessAction(action);

            MouseWheelProcessor.Update();
        }

        private void ProcessAction(InputAction action)
        {
            action.IsPressed = this._keyboardMouseProcessor.IsPressed(action.primaryKey) ||
                               this._keyboardMouseProcessor.IsPressed(action.secondaryKey);

            bool raiseAction = action.state switch
            {
                Contracts.Enums.KeyStates.Down => action.IsDown,
                Contracts.Enums.KeyStates.Up => action.IsUp,
                _ => action.IsPressed,
            };

            if (raiseAction)
                action.RaiseOnActionEvent();
        } 
        #endregion
    }
}
