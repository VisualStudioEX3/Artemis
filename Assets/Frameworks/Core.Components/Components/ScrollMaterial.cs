using UnityEngine;

namespace VisualStudioEX3.Artemis.Framework.Core.Components
{
    [DisallowMultipleComponent, ExecuteInEditMode]
    public class ScrollMaterial : MonoBehaviour
    {
        #region Internal vars
        private Material _material;
        #endregion

        #region Inspector fields
        [SerializeField]
        private Vector2 _speed = Vector2.one;
        [SerializeField]
        private bool _runInEditMode = false;
        #endregion

        #region Initializers
        private void Awake()
        {
            var renderer = this.GetComponent<MeshRenderer>();
            this._material = renderer.material;
        }
        #endregion

        #region Update logic
        private void Update()
        {
            this.UpdateScroll();
        }
        #endregion

        #region Methods & Functions
        public void UpdateScroll() => this._material.mainTextureOffset += Time.deltaTime * this._speed;
        #endregion
    }
}
