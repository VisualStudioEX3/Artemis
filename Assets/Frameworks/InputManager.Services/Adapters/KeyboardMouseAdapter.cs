using System;
using System.Collections.Generic;
using UnityEngine;
using VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Enums;

namespace VisualStudioEX3.Artemis.Framework.InputManager.Services
{
    internal static class KeyboardMouseAdapter
    {
        #region Constants
        private static readonly Dictionary<KeyboardMouseCodes, KeyCode> LEGACY_UNITY_INPUT_KEYCODES = new()
        {
            { KeyboardMouseCodes.None, KeyCode.None },
            { KeyboardMouseCodes.Backspace, KeyCode.Backspace },
            { KeyboardMouseCodes.Tab, KeyCode.Tab },
            { KeyboardMouseCodes.Clear, KeyCode.Clear },
            { KeyboardMouseCodes.Return, KeyCode.Return },
            { KeyboardMouseCodes.Escape, KeyCode.Escape },
            { KeyboardMouseCodes.Space, KeyCode.Space },
            { KeyboardMouseCodes.Exclaim, KeyCode.Exclaim },
            { KeyboardMouseCodes.DoubleQuote, KeyCode.DoubleQuote },
            { KeyboardMouseCodes.Hash, KeyCode.Hash },
            { KeyboardMouseCodes.Dollar, KeyCode.Dollar },
            { KeyboardMouseCodes.Ampersand, KeyCode.Ampersand },
            { KeyboardMouseCodes.Quote, KeyCode.Quote },
            { KeyboardMouseCodes.LeftParen, KeyCode.LeftParen },
            { KeyboardMouseCodes.RightParen, KeyCode.RightParen },
            { KeyboardMouseCodes.Asterisk, KeyCode.Asterisk },
            { KeyboardMouseCodes.Plus, KeyCode.Plus },
            { KeyboardMouseCodes.Comma, KeyCode.Comma },
            { KeyboardMouseCodes.Minus, KeyCode.Minus },
            { KeyboardMouseCodes.Period, KeyCode.Period },
            { KeyboardMouseCodes.Slash, KeyCode.Slash },
            { KeyboardMouseCodes.Alpha0, KeyCode.Alpha0 },
            { KeyboardMouseCodes.Alpha1, KeyCode.Alpha1 },
            { KeyboardMouseCodes.Alpha2, KeyCode.Alpha2 },
            { KeyboardMouseCodes.Alpha3, KeyCode.Alpha3 },
            { KeyboardMouseCodes.Alpha4, KeyCode.Alpha4 },
            { KeyboardMouseCodes.Alpha5, KeyCode.Alpha5 },
            { KeyboardMouseCodes.Alpha6, KeyCode.Alpha6 },
            { KeyboardMouseCodes.Alpha7, KeyCode.Alpha7 },
            { KeyboardMouseCodes.Alpha8, KeyCode.Alpha8 },
            { KeyboardMouseCodes.Alpha9, KeyCode.Alpha9 },
            { KeyboardMouseCodes.Colon, KeyCode.Colon },
            { KeyboardMouseCodes.Semicolon, KeyCode.Semicolon },
            { KeyboardMouseCodes.Less, KeyCode.Less },
            { KeyboardMouseCodes.Equals, KeyCode.Equals },
            { KeyboardMouseCodes.Greater, KeyCode.Greater },
            { KeyboardMouseCodes.Question, KeyCode.Question },
            { KeyboardMouseCodes.At, KeyCode.At },
            { KeyboardMouseCodes.LeftBracket, KeyCode.LeftBracket },
            { KeyboardMouseCodes.Backslash, KeyCode.Backslash },
            { KeyboardMouseCodes.RightBracket, KeyCode.RightBracket },
            { KeyboardMouseCodes.Caret, KeyCode.Caret },
            { KeyboardMouseCodes.Underscore, KeyCode.Underscore },
            { KeyboardMouseCodes.BackQuote, KeyCode.BackQuote },
            { KeyboardMouseCodes.A, KeyCode.A },
            { KeyboardMouseCodes.B, KeyCode.B },
            { KeyboardMouseCodes.C, KeyCode.C },
            { KeyboardMouseCodes.D, KeyCode.D },
            { KeyboardMouseCodes.E, KeyCode.E },
            { KeyboardMouseCodes.F, KeyCode.F },
            { KeyboardMouseCodes.G, KeyCode.G },
            { KeyboardMouseCodes.H, KeyCode.H },
            { KeyboardMouseCodes.I, KeyCode.I },
            { KeyboardMouseCodes.J, KeyCode.J },
            { KeyboardMouseCodes.K, KeyCode.K },
            { KeyboardMouseCodes.L, KeyCode.L },
            { KeyboardMouseCodes.M, KeyCode.M },
            { KeyboardMouseCodes.N, KeyCode.N },
            { KeyboardMouseCodes.O, KeyCode.O },
            { KeyboardMouseCodes.P, KeyCode.P },
            { KeyboardMouseCodes.Q, KeyCode.Q },
            { KeyboardMouseCodes.R, KeyCode.R },
            { KeyboardMouseCodes.S, KeyCode.S },
            { KeyboardMouseCodes.T, KeyCode.T },
            { KeyboardMouseCodes.U, KeyCode.U },
            { KeyboardMouseCodes.V, KeyCode.V },
            { KeyboardMouseCodes.W, KeyCode.W },
            { KeyboardMouseCodes.X, KeyCode.X },
            { KeyboardMouseCodes.Y, KeyCode.Y },
            { KeyboardMouseCodes.Z, KeyCode.Z },
            { KeyboardMouseCodes.Delete, KeyCode.Delete },
            { KeyboardMouseCodes.Keypad0, KeyCode.Keypad0 },
            { KeyboardMouseCodes.Keypad1, KeyCode.Keypad1 },
            { KeyboardMouseCodes.Keypad2, KeyCode.Keypad2 },
            { KeyboardMouseCodes.Keypad3, KeyCode.Keypad3 },
            { KeyboardMouseCodes.Keypad4, KeyCode.Keypad4 },
            { KeyboardMouseCodes.Keypad5, KeyCode.Keypad5 },
            { KeyboardMouseCodes.Keypad6, KeyCode.Keypad6 },
            { KeyboardMouseCodes.Keypad7, KeyCode.Keypad7 },
            { KeyboardMouseCodes.Keypad8, KeyCode.Keypad8 },
            { KeyboardMouseCodes.Keypad9, KeyCode.Keypad9 },
            { KeyboardMouseCodes.KeypadPeriod, KeyCode.KeypadPeriod },
            { KeyboardMouseCodes.KeypadDivide, KeyCode.KeypadDivide },
            { KeyboardMouseCodes.KeypadMultiply, KeyCode.KeypadMultiply },
            { KeyboardMouseCodes.KeypadMinus, KeyCode.KeypadMinus },
            { KeyboardMouseCodes.KeypadPlus, KeyCode.KeypadPlus },
            { KeyboardMouseCodes.KeypadEnter, KeyCode.KeypadEnter },
            { KeyboardMouseCodes.KeypadEquals, KeyCode.KeypadEquals },
            { KeyboardMouseCodes.UpArrow, KeyCode.UpArrow },
            { KeyboardMouseCodes.DownArrow, KeyCode.DownArrow },
            { KeyboardMouseCodes.RightArrow, KeyCode.RightArrow },
            { KeyboardMouseCodes.LeftArrow, KeyCode.LeftArrow },
            { KeyboardMouseCodes.Insert, KeyCode.Insert },
            { KeyboardMouseCodes.Home, KeyCode.Home },
            { KeyboardMouseCodes.End, KeyCode.End },
            { KeyboardMouseCodes.PageUp, KeyCode.PageUp },
            { KeyboardMouseCodes.PageDown, KeyCode.PageDown },
            { KeyboardMouseCodes.F1, KeyCode.F1 },
            { KeyboardMouseCodes.F2, KeyCode.F2 },
            { KeyboardMouseCodes.F3, KeyCode.F3 },
            { KeyboardMouseCodes.F4, KeyCode.F4 },
            { KeyboardMouseCodes.F5, KeyCode.F5 },
            { KeyboardMouseCodes.F6, KeyCode.F6 },
            { KeyboardMouseCodes.F7, KeyCode.F7 },
            { KeyboardMouseCodes.F8, KeyCode.F8 },
            { KeyboardMouseCodes.F9, KeyCode.F9 },
            { KeyboardMouseCodes.F10, KeyCode.F10 },
            { KeyboardMouseCodes.F11, KeyCode.F11 },
            { KeyboardMouseCodes.F12, KeyCode.F12 },
            { KeyboardMouseCodes.F13, KeyCode.F13 },
            { KeyboardMouseCodes.F14, KeyCode.F14 },
            { KeyboardMouseCodes.F15, KeyCode.F15 },
            { KeyboardMouseCodes.RightShift, KeyCode.RightShift },
            { KeyboardMouseCodes.LeftShift, KeyCode.LeftShift },
            { KeyboardMouseCodes.RightControl, KeyCode.RightControl },
            { KeyboardMouseCodes.LeftControl, KeyCode.LeftControl },
            { KeyboardMouseCodes.RightAlt, KeyCode.RightAlt },
            { KeyboardMouseCodes.LeftAlt, KeyCode.LeftAlt },
            { KeyboardMouseCodes.AltGr, KeyCode.AltGr },
            { KeyboardMouseCodes.LeftMouseButton, KeyCode.Mouse0 },
            { KeyboardMouseCodes.RightMouseButton, KeyCode.Mouse1 },
            { KeyboardMouseCodes.MiddleMouseButton, KeyCode.Mouse2 },
            { KeyboardMouseCodes.AdditionalMouseButton4, KeyCode.Mouse3 },
            { KeyboardMouseCodes.AdditionalMouseButton5, KeyCode.Mouse4 },
            { KeyboardMouseCodes.AdditionalMouseButton6, KeyCode.Mouse5 },
            { KeyboardMouseCodes.AdditionalMouseButton7, KeyCode.Mouse6 },
        };
        #endregion

        #region Methods & Functions
        public static KeyCode ToUnityLegacyInputKeyCode(KeyboardMouseCodes code)
        {
            return code switch
            {
                KeyboardMouseCodes.MouseWheelLeft or
                KeyboardMouseCodes.MouseWheelDown or
                KeyboardMouseCodes.MouseWheelRight or
                KeyboardMouseCodes.MouseWheelUp => throw FormatArgumentException(code),
                _ => KeyboardMouseAdapter.LEGACY_UNITY_INPUT_KEYCODES[code],
            };
        }

        private static ArgumentException FormatArgumentException(KeyboardMouseCodes code)
        {
            const string MESSAGE = "{0}::{1}: The code {2} can not be casted to {3}.{4} enum value.";

            return new ArgumentException(string.Format(MESSAGE, 
                nameof(KeyboardMouseAdapter), 
                nameof(ToUnityLegacyInputKeyCode), 
                code, 
                nameof(UnityEngine), 
                nameof(KeyCode)));
        }
        #endregion
    }
}
