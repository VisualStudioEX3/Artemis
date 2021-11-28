using System.Collections.Generic;
using VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Models;

namespace VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Interfaces
{
    /// <summary>
    /// Service used to process and update the user input axes.
    /// </summary>
    public interface IInputAxisProcessor
    {
        #region Properties
        /// <summary>
        /// Is the mouse in movement?
        /// </summary>
        bool IsMouseMoving { get; }
        #endregion

        #region Methods & Functions
        /// <summary>
        /// Update the <see cref="InputAxis"/> states.
        /// </summary>
        /// <param name="axis"><see cref="InputAxis"/> list.</param>
        void Update(IEnumerable<InputAxis> axes); 
        #endregion
    }
}
