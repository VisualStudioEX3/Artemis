using UnityEngine;
using VisualStudioEX3.Artemis.Framework.Core.Contracts.Constants;

namespace VisualStudioEX3.Artemis.Framework.Core.Components.Materials
{
    /// <summary>
    /// Scroll material offset.
    /// </summary>
    /// <remarks>Use this component to apply a smooth scroll effect on the <see cref="Material"/> used by the <see cref="Renderer"/> associated with this <see cref="GameObject"/>.</remarks>
    [DisallowMultipleComponent, ExecuteInEditMode, AddComponentMenu(ComponentMenuPaths.SCROLL_MATERIAL_COMPONENT_MENU_NAMESPACE)]
    public class ScrollMaterial : MonoBehaviour
    {
        #region Internal vars
        private Material _material;
        #endregion

        #region Inspector fields
        [SerializeField]
        private Vector2 _speed = Vector2.one;
        [SerializeField]
        private bool _useSharedMaterial = false;
        [SerializeField, Tooltip("Enabled this to allow test the effect in edit mode.")]
        private bool _runInEditMode = false;
        #endregion

        #region Initializers & Terminators
        private void Awake() => this.GetRendererMaterialData();
        
        private void OnDestroy() => this.OnDisable();
        #endregion

        #region Update logic
        private void Update()
        {
            if (Application.isPlaying) 
                this.UpdateScroll();
        }
        #endregion

        #region Methods & Functions
        private void GetRendererMaterialData()
        {
            MeshRenderer renderer = this.GetComponent<MeshRenderer>();
            this._material = this._useSharedMaterial ? renderer.sharedMaterial : renderer.material;
        }

        /// <summary>
        /// Updates the material offset with the current settings.
        /// </summary>
        public void UpdateScroll() => this._material.mainTextureOffset += Time.deltaTime * this._speed;

        /// <summary>
        /// Resets the the material offset.
        /// </summary>
        public void ResetScroll() => this._material.mainTextureOffset = Vector2.zero;
        #endregion

        #region Event listeners
        private void OnDisable() => this.ResetScroll(); 
        #endregion
    }
}
