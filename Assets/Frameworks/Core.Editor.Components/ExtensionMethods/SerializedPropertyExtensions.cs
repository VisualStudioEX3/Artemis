using UnityEditor;

namespace VisualStudioEX3.Artemis.Framework.Core.Editor.Components.ExtensionMethods
{
    public static class SerializedPropertyExtensions
    {
        #region Constants
        private const string ARRAY_DATA_MASK = "Array.data[";
        #endregion

        #region Methods & Functions
        /// <summary>
        /// Check if the <see cref="SerializedProperty"/> is an array element.
        /// </summary>
        /// <param name="property"><see cref="SerializedProperty"/> instance.</param>
        /// <returns>Returns <see langword="true"/> if the <see cref="SerializedProperty"/> is an array element.</returns>
        public static bool IsArrayElement(this SerializedProperty property) => property.propertyPath.Contains(ARRAY_DATA_MASK);
        #endregion
    }
}
