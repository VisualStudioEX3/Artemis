namespace VisualStudioEX3.Artemis.Framework.Core.Contracts.Enums
{
    #region Enums
    /// <summary>
    /// Events to define when the inspector buttons created by <see cref="ButtonAttribute"/> attribute must be disabled.
    /// </summary>
    public enum GUIButtonDisableEvents
    {
        /// <summary>
        /// The button is enable in play and editor modes.
        /// </summary>
        Never,
        /// <summary>
        /// Disable when the game enter in play mode.
        /// </summary>
        PlayMode,
        /// <summary>
        /// Disable when the game enter in editor mode.
        /// </summary>
        EditorMode
    } 
    #endregion
}
