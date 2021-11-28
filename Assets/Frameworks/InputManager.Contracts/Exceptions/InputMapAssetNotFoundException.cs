using System;
using VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Models.Assets;

namespace VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Exceptions
{
    /// <summary>
    /// Exception for when trying to access an <see cref="InputMapAsset"/> element thats not exists.
    /// </summary>
    public class InputMapAssetNotFoundException : Exception
    {
        #region Constructor
        public InputMapAssetNotFoundException(string assetName)
            : base($"The {nameof(InputMapAsset)} \"{assetName}\" not exists in the list.")
        {
        }
        #endregion
    }
}
