using System;
using VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Models;

namespace VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Exceptions
{
    /// <summary>
    /// Exception for when trying to access an <see cref="InputAxis"/> element thats not exists.
    /// </summary>
    public class InputAxisNotFoundException : Exception
    {
        #region Constructor
        public InputAxisNotFoundException(string axisName, string inputMapName)
            : base($"The {nameof(InputAxis)} \"{axisName}\" not exists in the asset \"{inputMapName}\".")
        {
        } 
        #endregion
    }
}
