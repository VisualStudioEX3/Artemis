using UnityEngine;
using VisualStudioEX3.Artemis.Framework.Core.Contracts.Enums;

namespace VisualStudioEX3.Artemis.Framework.Core.Contracts.Attributes
{
    /// <summary>
    /// Attribute used to make a string variable in a script be a button that can invoke a method (method name is defined in the string value).
    /// </summary>
    /// <remarks>Only methods without parameters.</remarks>
    public class ButtonAttribute : PropertyAttribute
    {
        #region Public vars
        public readonly string customLabel;
        public readonly EditorGUIButtonSize size;
        public readonly EditorGUIButtonDisableEvents disableOn;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="customLabel">Custom label to show in the button. By default using the variable name.</param>
        /// <param name="size">The button size. By default using the default button size.</param>
        /// <param name="disableOn">Disable the button when enter in the selected mode (by default is enable for play and editor modes).</param>
        public ButtonAttribute(string customLabel = "", EditorGUIButtonSize size = EditorGUIButtonSize.Normal, EditorGUIButtonDisableEvents disableOn = EditorGUIButtonDisableEvents.Never)
        {
            this.customLabel = customLabel;
            this.size = size;
            this.disableOn = disableOn;
        }
        #endregion
    }
}
