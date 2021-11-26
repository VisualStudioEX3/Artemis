using System;
using System.Collections.Generic;
using UnityEngine;
using VisualStudioEX3.Artemis.Framework.InputManager.Contracts;

namespace VisualStudioEX3.Artemis.Framework.InputManager.Services.Processors
{
    class UnityLegacyInputKeyboardMouseProcessor : IKeyboardMouseProcessor
    {
        #region Constants
        private static readonly Dictionary<KeyStates, Func<KeyCode, bool>> UNITY_LEGACY_INPUT_GET_KEY_FUNCTIONS = new()
        {
            { KeyStates.Pressed, Input.GetKey },
            { KeyStates.Down, Input.GetKeyDown },
            { KeyStates.Up, Input.GetKeyUp },
        };
        #endregion

        #region Enums
        private enum KeyStates
        {
            Pressed,
            Down,
            Up
        }
        #endregion

        #region Properties
        public bool IsAnyKeyPressed => Input.anyKey;
        public bool IsAnyKeyDown => Input.anyKeyDown;
        #endregion

        #region Methods & Functions
        private bool IsMouseWheel(KeyboardMouseCodes code)
        {
            return code == KeyboardMouseCodes.MouseWheelUp ||
                   code == KeyboardMouseCodes.MouseWheelDown;
        }

        private bool GetMouseWheelStatus(KeyboardMouseCodes code)
        {
            return code switch
            {
                KeyboardMouseCodes.MouseWheelDown => Input.mousePosition.z > 0,
                KeyboardMouseCodes.MouseWheelUp => Input.mousePosition.z < 0,
                _ => false,
            };
        }

        private bool GetKeyStatus(KeyboardMouseCodes code, KeyStates state)
        {
            KeyCode unityKeyCode = KeyboardMouseAdapter.ToUnityLegacyInputKeyCode(code);
            Func<KeyCode, bool> getKeyFunction = 
                UnityLegacyInputKeyboardMouseProcessor.UNITY_LEGACY_INPUT_GET_KEY_FUNCTIONS[state];

            return (this.IsMouseWheel(code) && state != KeyStates.Up)
                ? this.GetMouseWheelStatus(code)
                : getKeyFunction(unityKeyCode);
        }

        public bool IsPressed(KeyboardMouseCodes code) => this.GetKeyStatus(code, KeyStates.Pressed);

        public bool IsDown(KeyboardMouseCodes code) => this.GetKeyStatus(code, KeyStates.Down);

        public bool IsUp(KeyboardMouseCodes code) => this.GetKeyStatus(code, KeyStates.Up);
        #endregion
    }
}
