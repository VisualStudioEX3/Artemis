using System;
using System.Collections.Generic;
using UnityEngine;
using VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Enums;
using VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Interfaces;
using VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Models;

namespace VisualStudioEX3.Artemis.Framework.InputManager.Services.Processors
{
    public class UnityLegacyInputKeyboardMouseProcessor : IKeyboardMouseProcessor
    {
        #region Constants
        private static readonly Dictionary<KeyStates, Func<KeyCode, bool>> UNITY_LEGACY_INPUT_GET_KEY_FUNCTIONS = new()
        {
            { KeyStates.Pressed, Input.GetKey },
            { KeyStates.Down, Input.GetKeyDown },
            { KeyStates.Up, Input.GetKeyUp },
        };
        #endregion

        #region Properties
        public bool IsAnyKeyPressed => Input.anyKey;
        public bool IsAnyKeyDown => Input.anyKeyDown;
        public Vector2 MousePosition => Input.mousePosition;
        #endregion

        #region Methods & Functions
        private bool IsMouseWheelKeyCode(KeyboardMouseCodes code)
        {
            return code switch
            {
                KeyboardMouseCodes.MouseWheelLeft or 
                KeyboardMouseCodes.MouseWheelUp or 
                KeyboardMouseCodes.MouseWheelRight or 
                KeyboardMouseCodes.MouseWheelDown => true,
                _ => false,
            };
        }

        private bool GetMouseWheelStatus(KeyboardMouseCodes code, KeyStates state)
        {
            InputAction mouseWheelAxis = MouseWheelProcessor.mouseWheelAxes[code];

            return state switch
            {
                KeyStates.Down => mouseWheelAxis.IsDown,
                KeyStates.Up => mouseWheelAxis.IsUp,
                _ => mouseWheelAxis.IsPressed,
            };
        }

        private bool GetKeyStatus(KeyboardMouseCodes code, KeyStates state)
        {
            KeyCode unityKeyCode = KeyboardMouseAdapter.ToUnityLegacyInputKeyCode(code);
            Func<KeyCode, bool> getKeyFunction =
                UnityLegacyInputKeyboardMouseProcessor.UNITY_LEGACY_INPUT_GET_KEY_FUNCTIONS[state];

            return getKeyFunction(unityKeyCode);
        }

        private bool GetStatus(KeyboardMouseCodes code, KeyStates state)
        {
            return this.IsMouseWheelKeyCode(code) 
                ? this.GetMouseWheelStatus(code, state) 
                : this.GetKeyStatus(code, state);
        }

        public bool IsPressed(KeyboardMouseCodes code) => this.GetStatus(code, KeyStates.Pressed);

        public bool IsDown(KeyboardMouseCodes code) => this.GetStatus(code, KeyStates.Down);

        public bool IsUp(KeyboardMouseCodes code) => this.GetStatus(code, KeyStates.Up);
        #endregion
    }
}
