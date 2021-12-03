using UnityEngine;
using VisualStudioEX3.Artemis.Framework.Core.Contracts.Constants;
using VisualStudioEX3.Artemis.Framework.Core.Contracts.Interfaces;
using VisualStudioEX3.Artemis.Framework.Core.ServiceProvider;

namespace VisualStudioEX3.Artemis.Framework.Core.Components.Transforms
{
    /// <summary>
    /// Component to rotate a game object.
    /// </summary>
    [AddComponentMenu(ComponentMenuPaths.ROTATE_TRANSFORM_COMPONENT_MENU_NAMESPACE), DisallowMultipleComponent]
    public sealed class RotateTransform : MonoBehaviour
    {
        #region Internal vars
        private IMathHelper _mathHelper;
        #endregion

        #region Public vars
        public float speed = 1f;
        [Tooltip("Step defined in degrees.")]
        public float step = 1f;
        public Vector3 axis = Vector3.zero;
        public Space relativeTo = Space.Self;
        #endregion

        private void Awake() => this._mathHelper = CoreServiceFactory.Factory.GetService<IMathHelper>();

        #region Update logic
        void Update()
        {
            this.ClampVector();

            if (Application.isPlaying)
                this.RotateVector();
        }
        #endregion

        #region Methos & Functions
        private Vector3 ClampVector() => this.axis = this._mathHelper.ClampVector(this.axis, Vector3.one * -1f, Vector3.one);

        private Vector3 CalculateStep() => this.axis * this.step * Time.deltaTime * this.speed;

        private void RotateVector() => this.transform.Rotate(this.CalculateStep(), this.relativeTo); 
        #endregion
    }
}
