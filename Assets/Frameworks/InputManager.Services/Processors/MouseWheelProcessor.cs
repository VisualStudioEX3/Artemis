using System.Collections.Generic;
using UnityEngine;
using VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Enums;
using VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Models;

namespace VisualStudioEX3.Artemis.Framework.InputManager.Services.Processors
{
    // BUG: Check why the down and up events not working properly (now working like the pressed event).
    internal static class MouseWheelProcessor
    {
        #region Internal vars
        public static readonly Dictionary<KeyboardMouseCodes, InputAction> mouseWheelAxes = new()
        {
            { KeyboardMouseCodes.MouseWheelLeft, new InputAction() },
            { KeyboardMouseCodes.MouseWheelUp, new InputAction() },
            { KeyboardMouseCodes.MouseWheelRight, new InputAction() },
            { KeyboardMouseCodes.MouseWheelDown, new InputAction() },
        };
        #endregion

        #region Methods & Functions
        public static void Update()
        {
            foreach (KeyValuePair<KeyboardMouseCodes, InputAction> item in MouseWheelProcessor.mouseWheelAxes)
                ProcessAxis(code: item.Key, action: item.Value);
        }

        private static void ProcessAxis(KeyboardMouseCodes code, InputAction action)
        {
            Vector2 mouseScrollDelta = Input.mouseScrollDelta;

            switch (code)
            {
                case KeyboardMouseCodes.MouseWheelLeft:
                    action.IsPressed = mouseScrollDelta.x < 0;
                    break;
                case KeyboardMouseCodes.MouseWheelUp:
                    action.IsPressed = mouseScrollDelta.y > 0;
                    break;
                case KeyboardMouseCodes.MouseWheelRight:
                    action.IsPressed = mouseScrollDelta.x > 0;
                    break;
                case KeyboardMouseCodes.MouseWheelDown:
                    action.IsPressed = mouseScrollDelta.y < 0;
                    break;
            }
        } 
        #endregion
    }
}
