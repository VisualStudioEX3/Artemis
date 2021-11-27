namespace VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Enums
{
    #region Enums
    /// <summary>
    /// Allowed keyboard and mouse button codes.
    /// </summary>
    public enum KeyboardMouseCodes
    {
        /// <summary>
        /// Not assigned (never returned as the result of a keystroke).
        /// </summary>
        None,
        /// <summary>
        /// The backspace key.
        /// </summary>
        Backspace,
        /// <summary>
        /// The tab key.
        /// </summary>
        Tab,
        /// <summary>
        /// The Clear key.
        /// </summary>
        Clear,
        /// <summary>
        /// Return key.
        /// </summary>
        Return,
        /// <summary>
        /// Escape key.
        /// </summary>
        Escape,
        /// <summary>
        /// Space key.
        /// </summary>
        Space,
        /// <summary>
        /// Exclamation mark key '!'.
        /// </summary>
        Exclaim,
        /// <summary>
        /// Double quote key '"'.
        /// </summary>
        DoubleQuote,
        /// <summary>
        /// Hash key '#'.
        /// </summary>
        Hash,
        /// <summary>
        /// Dollar sign key '$'.
        /// </summary>
        Dollar,
        /// <summary>
        /// Ampersand key '&'.
        /// </summary>
        Ampersand,
        /// <summary>
        /// Quote key '.
        /// </summary>
        Quote,
        /// <summary>
        /// Left Parenthesis key '('.
        /// </summary>
        LeftParen,
        /// <summary>
        /// Right Parenthesis key ')'.
        /// </summary>
        RightParen,
        /// <summary>
        /// Asterisk key '*'.
        /// </summary>
        Asterisk,
        /// <summary>
        /// Plus key '+'.
        /// </summary>
        Plus,
        /// <summary>
        /// Comma ',' key.
        /// </summary>
        Comma,
        /// <summary>
        /// Minus '-' key.
        /// </summary>
        Minus,
        /// <summary>
        /// Period '.' key.
        /// </summary>
        Period,
        /// <summary>
        /// Slash '/' key.
        /// </summary>
        Slash,
        /// <summary>
        /// The '0' key on the top of the alphanumeric keyboard.
        /// </summary>
        Alpha0,
        /// <summary>
        /// The '1' key on the top of the alphanumeric keyboard.
        /// </summary>
        Alpha1,
        /// <summary>
        /// The '2' key on the top of the alphanumeric keyboard.
        /// </summary>
        Alpha2,
        /// <summary>
        /// The '3' key on the top of the alphanumeric keyboard.
        /// </summary>
        Alpha3,
        /// <summary>
        /// The '4' key on the top of the alphanumeric keyboard.
        /// </summary>
        Alpha4,
        /// <summary>
        /// The '5' key on the top of the alphanumeric keyboard.
        /// </summary>
        Alpha5,
        /// <summary>
        /// The '6' key on the top of the alphanumeric keyboard.
        /// </summary>
        Alpha6,
        /// <summary>
        /// The '7' key on the top of the alphanumeric keyboard.
        /// </summary>
        Alpha7,
        /// <summary>
        /// The '8' key on the top of the alphanumeric keyboard.
        /// </summary>
        Alpha8,
        /// <summary>
        /// The '9' key on the top of the alphanumeric keyboard.
        /// </summary>
        Alpha9,
        /// <summary>
        /// Colon ':' key.
        /// </summary>
        Colon,
        /// <summary>
        /// Semicolon ';' key.
        /// </summary>
        Semicolon,
        /// <summary>
        /// Less than '<' key.
        /// </summary>
        Less,
        /// <summary>
        /// Equals '=' key.
        /// </summary>
        Equals,
        /// <summary>
        /// Greater than '>' key.
        /// </summary>
        Greater,
        /// <summary>
        /// Question mark '?' key.
        /// </summary>
        Question,
        /// <summary>
        /// At key '@'.
        /// </summary>
        At,
        /// <summary>
        /// Left square bracket key '['.
        /// </summary>
        LeftBracket,
        /// <summary>
        /// Backslash key '\'.
        /// </summary>
        Backslash,
        /// <summary>
        /// Right square bracket key ']'.
        /// </summary>
        RightBracket,
        /// <summary>
        /// Caret key '^'.
        /// </summary>
        Caret,
        /// <summary>
        /// Underscore '_' key.
        /// </summary>
        Underscore,
        /// <summary>
        /// Back quote key '`'.
        /// </summary>
        BackQuote,
        /// <summary>
        /// 'a' key.
        /// </summary>
        A,
        /// <summary>
        /// 'b' key.
        /// </summary>
        B,
        /// <summary>
        /// 'c' key.
        /// </summary>
        C,
        /// <summary>
        /// 'd' key.
        /// </summary>
        D,
        /// <summary>
        /// 'e' key.
        /// </summary>
        E,
        /// <summary>
        /// 'f' key.
        /// </summary>
        F,
        /// <summary>
        /// 'g' key.
        /// </summary>
        G,
        /// <summary>
        /// 'h' key.
        /// </summary>
        H,
        /// <summary>
        /// 'i' key.
        /// </summary>
        I,
        /// <summary>
        /// 'j' key.
        /// </summary>
        J,
        /// <summary>
        /// 'k' key.
        /// </summary>
        K,
        /// <summary>
        /// 'l' key.
        /// </summary>
        L,
        /// <summary>
        /// 'm' key.
        /// </summary>
        M,
        /// <summary>
        /// 'n' key.
        /// </summary>
        N,
        /// <summary>
        /// 'o' key.
        /// </summary>
        O,
        /// <summary>
        /// 'p' key.
        /// </summary>
        P,
        /// <summary>
        /// 'q' key.
        /// </summary>
        Q,
        /// <summary>
        /// 'r' key.
        /// </summary>
        R,
        /// <summary>
        /// 's' key.
        /// </summary>
        S,
        /// <summary>
        /// 't' key.
        /// </summary>
        T,
        /// <summary>
        /// 'u' key.
        /// </summary>
        U,
        /// <summary>
        /// 'v' key.
        /// </summary>
        V,
        /// <summary>
        /// 'w' key.
        /// </summary>
        W,
        /// <summary>
        /// 'x' key.
        /// </summary>
        X,
        /// <summary>
        /// 'y' key.
        /// </summary>
        Y,
        /// <summary>
        /// 'z' key.
        /// </summary>
        Z,
        /// <summary>
        /// The forward delete key.
        /// </summary>
        Delete,
        /// <summary>
        /// Numeric keypad 0.
        /// </summary>
        Keypad0,
        /// <summary>
        /// Numeric keypad 1.
        /// </summary>
        Keypad1,
        /// <summary>
        /// Numeric keypad 2.
        /// </summary>
        Keypad2,
        /// <summary>
        /// Numeric keypad 3.
        /// </summary>
        Keypad3,
        /// <summary>
        /// Numeric keypad 4.
        /// </summary>
        Keypad4,
        /// <summary>
        /// Numeric keypad 5.
        /// </summary>
        Keypad5,
        /// <summary>
        /// Numeric keypad 6.
        /// </summary>
        Keypad6,
        /// <summary>
        /// Numeric keypad 7.
        /// </summary>
        Keypad7,
        /// <summary>
        /// Numeric keypad 8.
        /// </summary>
        Keypad8,
        /// <summary>
        /// Numeric keypad 9.
        /// </summary>
        Keypad9,
        /// <summary>
        /// Numeric keypad '.'.
        /// </summary>
        KeypadPeriod,
        /// <summary>
        /// Numeric keypad '/'.
        /// </summary>
        KeypadDivide,
        /// <summary>
        /// Numeric keypad '*'.
        /// </summary>
        KeypadMultiply,
        /// <summary>
        /// Numeric keypad '-'.
        /// </summary>
        KeypadMinus,
        /// <summary>
        /// Numeric keypad '+'.
        /// </summary>
        KeypadPlus,
        /// <summary>
        /// Numeric keypad enter.
        /// </summary>
        KeypadEnter,
        /// <summary>
        /// Numeric keypad '='.
        /// </summary>
        KeypadEquals,
        /// <summary>
        /// Up arrow key.
        /// </summary>
        UpArrow,
        /// <summary>
        /// Down arrow key.
        /// </summary>
        DownArrow,
        /// <summary>
        /// Right arrow key.
        /// </summary>
        RightArrow,
        /// <summary>
        /// Left arrow key.
        /// </summary>
        LeftArrow,
        /// <summary>
        /// Insert key key.
        /// </summary>
        Insert,
        /// <summary>
        /// Home key.
        /// </summary>
        Home,
        /// <summary>
        /// End key.
        /// </summary>
        End,
        /// <summary>
        /// Page up.
        /// </summary>
        PageUp,
        /// <summary>
        /// Page down.
        /// </summary>
        PageDown,
        /// <summary>
        /// F1 function key.
        /// </summary>
        F1,
        /// <summary>
        /// F2 function key.
        /// </summary>
        F2,
        /// <summary>
        /// F3 function key.
        /// </summary>
        F3,
        /// <summary>
        /// F4 function key.
        /// </summary>
        F4,
        /// <summary>
        /// F5 function key.
        /// </summary>
        F5,
        /// <summary>
        /// F6 function key.
        /// </summary>
        F6,
        /// <summary>
        /// F7 function key.
        /// </summary>
        F7,
        /// <summary>
        /// F8 function key.
        /// </summary>
        F8,
        /// <summary>
        /// F9 function key.
        /// </summary>
        F9,
        /// <summary>
        /// F10 function key.
        /// </summary>
        F10,
        /// <summary>
        /// F11 function key.
        /// </summary>
        F11,
        /// <summary>
        /// F12 function key.
        /// </summary>
        F12,
        /// <summary>
        /// F13 function key.
        /// </summary>
        F13,
        /// <summary>
        /// F14 function key.
        /// </summary>
        F14,
        /// <summary>
        /// F15 function key.
        /// </summary>
        F15,
        /// <summary>
        /// Right shift key.
        /// </summary>
        RightShift,
        /// <summary>
        /// Left shift key.
        /// </summary>
        LeftShift,
        /// <summary>
        /// Right Control key.
        /// </summary>
        RightControl,
        /// <summary>
        /// Left Control key.
        /// </summary>
        LeftControl,
        /// <summary>
        /// Right Alt key.
        /// </summary>
        RightAlt,
        /// <summary>
        /// Left Alt key.
        /// </summary>
        LeftAlt,
        /// <summary>
        /// Alt Gr key.
        /// </summary>
        AltGr,
        /// <summary>
        /// The Left (or primary) mouse button.
        /// </summary>
        LeftMouseButton,
        /// <summary>
        /// Right mouse button (or secondary mouse button).
        /// </summary>
        RightMouseButton,
        /// <summary>
        /// Middle mouse button (or third button).
        /// </summary>
        MiddleMouseButton,
        /// <summary>
        /// Mouse wheel left scroll step.
        /// </summary>
        MouseWheelLeft,
        /// <summary>
        /// Mouse wheel right scroll step.
        /// </summary>
        MouseWheelRight,
        /// <summary>
        /// Mouse wheel up scroll step.
        /// </summary>
        MouseWheelUp,
        /// <summary>
        /// Mouse wheel down scroll step.
        /// </summary>
        MouseWheelDown,
        /// <summary>
        /// Additional (fourth) mouse button.
        /// </summary>
        AdditionalMouseButton4,
        /// <summary>
        /// Additional (fifth) mouse button.
        /// </summary>
        AdditionalMouseButton5,
        /// <summary>
        /// Additional (or sixth) mouse button.
        /// </summary>
        AdditionalMouseButton6,
        /// <summary>
        /// Additional (or seventh) mouse button.
        /// </summary>
        AdditionalMouseButton7,
    }
    #endregion
}
