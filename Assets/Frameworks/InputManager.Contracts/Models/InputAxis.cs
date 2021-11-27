using System;
using UnityEngine;
using VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Enums;

namespace VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Models
{
    [Serializable]
    public class InputAxis
    {
        #region Constants
        private const float MIN_SENSITIVITY = 0.05f;
        private const float MAX_SENSITIVITY = 3f;
        private const float DEFAULT_SENSITIVITY = 1f;
        #endregion

        #region Public vars
        public string name;
        [Space]

        public MouseInputModes mouseBehaviour = MouseInputModes.MousePosition;

        [Header("Keyboard navigation")]
        public InputAction LeftKey;
        public InputAction RightKey;
        public InputAction DownKey;
        public InputAction UpKey;
        [Space]

        /// <summary>
        /// Invert Y axis.
        /// </summary>
        public bool invertYAxis = false;

        /// <summary>
        /// Axis sensitivity factor.
        /// </summary>
        [Range(InputAxis.MIN_SENSITIVITY, InputAxis.MAX_SENSITIVITY)]
        public float sensitivity = InputAxis.DEFAULT_SENSITIVITY;

        /// <summary>
        /// Normalize axis.
        /// </summary>
        /// <remarks>Use this property to fix the diagonal ranges on 360º/free movements.</remarks>
        [Tooltip("Normalize the diagonal ranges."), Space]
        public bool normalize = true;
        #endregion

        public InputAxis()
        {
            this.normalize = true;
        }
    }
}
