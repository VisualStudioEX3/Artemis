using UnityEngine;

namespace VisualStudioEX3.Artemis.Framework.Core.Components
{
    /// <summary>
    /// Fade material component.
    /// </summary>
    /// <remarks>Use this component to setup a fade-in fade-out effect based on the alpha <see cref="Color"/> component of the <see cref="Renderer"/> material.</remarks>
    public class FadeMaterial : MonoBehaviour
    {
        #region Internal vars
        private Material _material;
        #endregion

        #region Inspector fields
        /// <summary>
        /// Fade-in fade-out speed.
        /// </summary>
        public float speed = 1f;
        #endregion

        #region Initializers
        private void Awake()
        {
            MeshRenderer renderer = this.GetComponent<MeshRenderer>();
            this._material = renderer.material;
        }
        #endregion

        #region Update logic
        void Update()
        {
            Color color = this._material.color;
            color.a = Mathf.PingPong(Time.time * this.speed, 1f);
            this._material.color = color;
        } 
        #endregion
    }
}
