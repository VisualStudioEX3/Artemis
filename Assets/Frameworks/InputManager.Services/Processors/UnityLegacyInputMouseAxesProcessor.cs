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
        public bool IsMouseMoving => this.MouseDelta != Vector2.zero;
        public Vector2 MousePosition { get; private set; }
        public Vector2 MouseAxis { get; private set; }
        public Vector2 MouseDelta { get; private set; }
        #endregion

        #region Methods & Functions
        private Vector2 ProcessMouseAxis(Vector2 position)
        {
            if (position == Vector2.zero)
                return Vector2.zero;

            Vector2 screenCenter = new Vector2(Screen.width, Screen.height) * 0.5f;
            Vector2 mouseAxis = (position - screenCenter).normalized;

            return mouseAxis;
        }

        private Vector2 ProcessMouseDelta(Vector2 currentPosition, Vector2 lastPosition)
        {
            return (currentPosition - lastPosition).normalized;
        }

        public void Update()
        {
            this.MousePosition = Input.mousePosition;
            this.MouseAxis = this.ProcessMouseAxis(this.MousePosition);
            this.MouseDelta = this.ProcessMouseDelta(this.MousePosition, this._lastPosition);

            this._lastPosition = this.MousePosition;
        }
        #endregion
    }
}
