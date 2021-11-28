using UnityEngine;
using VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Interfaces;

namespace VisualStudioEX3.Artemis.Framework.InputManager.Services.Processors
{
    public class UnityLegacyInputMouseAxesProcessor : IMousePositionAxesProcessor
    {
        #region Internal vars
        private Vector2 _lastPosition = Input.mousePosition;
        #endregion

        #region Properties
        public bool IsMouseMoving => this.MousePosition != Vector2.zero;
        public Vector2 MousePosition { get; private set; }
        public Vector2 MouseDelta { get; private set; }
        #endregion

        #region Methods & Functions
        public void Update()
        {
            this.MousePosition = Input.mousePosition;
            this.MouseDelta = (this.MousePosition - this._lastPosition).normalized;
            this._lastPosition = this.MousePosition;
        } 
        #endregion
    }
}
