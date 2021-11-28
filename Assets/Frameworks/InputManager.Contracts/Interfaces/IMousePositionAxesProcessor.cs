using UnityEngine;

namespace VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Interfaces
{
    /// <summary>
    /// Service used to check the game input values from mouse position and axes.
    /// </summary>
    public interface IMousePositionAxesProcessor
    {
        #region Properties
        /// <summary>
        /// Is the mouse in movement.
        /// </summary>
        bool IsMouseMoving { get; }

        /// <summary>
        /// Gets the mouse position.
        /// </summary>
        Vector2 MousePosition { get; }

        /// <summary>
        /// Gets the mouse delta in normalized range [0..1].
        /// </summary>
        Vector2 MouseDelta { get; }
        #endregion

        #region Methods & Functions
        /// <summary>
        /// Update the mouse position and mouse delta properties.
        /// </summary>
        void Update();
        #endregion
    }
}
