using System;
using VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Models;

namespace VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Exceptions
{
    /// <summary>
    /// Exception for when trying to access an <see cref="InputAction"/> element thats not exists.
    /// </summary>
    public class InputActionNotFoundException : Exception
    {
        #region Constructor
        public InputActionNotFoundException(string actionName, string inputMapName)
            : base($"The {nameof(InputAction)} \"{actionName}\" not exists in the asset \"{inputMapName}\".")
        {
        } 
        #endregion
    }
}
