namespace VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Enums
{
    #region Enums
    /// <summary>
    /// Mouse input mode.
    /// </summary>
    public enum MouseInputModes
    {
        None,
        /// <summary>
        /// Mouse position behaviour.
        /// </summary>
        /// <remarks>Process mouse position in screen space.</remarks>
        MousePosition,
        /// <summary>
        /// Mouse axis behaviour.
        /// </summary>
        /// <remarks>Process the mouse vector direction from the center of the screen.</remarks>
        MouseAxis,
        /// <summary>
        /// Mouse delta behaviour.
        /// </summary>
        /// <remarks>Process mouse differences from the position in last frame with the current position.</remarks>
        MouseDelta
    }
    #endregion
}
