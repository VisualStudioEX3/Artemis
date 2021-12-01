using UnityEngine;
using VisualStudioEX3.Artemis.Framework.Core.Contracts.Interfaces;

namespace VisualStudioEX3.Artemis.Framework.Core.Services
{
    public class MathHelper : IMathHelper
    {
        #region Methods & Functions
        public Vector3 ClampVector(Vector3 vector, Vector3 min, Vector3 max)
        {
            Vector3 v = vector;

            for (int i = 0; i < 3; i++)
                v[i] = Mathf.Clamp(v[i], min[i], max[i]);

            return v;
        }
        #endregion
    }
}
