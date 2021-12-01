using UnityEngine;

namespace VisualStudioEX3.Artemis.Framework.Core.Contracts.Interfaces
{
    public interface IMathHelper
    {
        #region Methods & Functions
        /// <summary>
        /// Clamps <see cref="Vector3"/> fields.
        /// </summary>
        /// <param name="vector">Vector to clamp.</param>
        /// <param name="min">Min values.</param>
        /// <param name="max">Max values.</param>
        /// <returns></returns>
        Vector3 ClampVector(Vector3 vector, Vector3 min, Vector3 max); 
        #endregion
    }
}
