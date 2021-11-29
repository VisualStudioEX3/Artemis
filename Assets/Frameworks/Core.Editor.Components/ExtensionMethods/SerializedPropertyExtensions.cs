using UnityEditor;

namespace VisualStudioEX3.Artemis.Framework.Core.Editor.Components.ExtensionMethods
{
    public static class SerializedPropertyExtensions
    {
        #region Methods & Functions
        /// <summary>
        /// Check if the <see cref="SerializedProperty"/> is an array element.
        /// </summary>
        /// <param name="property"><see cref="SerializedProperty"/> instance.</param>
        /// <returns>Returns <see langword="true"/> if the <see cref="SerializedProperty"/> is an array element.</returns>
        public static bool IsArrayElement(this SerializedProperty property)
        {
            return IsArrayElement(property, out int _);
        }

        /// <summary>
        /// Check if the <see cref="SerializedProperty"/> is an array element.
        /// </summary>
        /// <param name="property"><see cref="SerializedProperty"/> instance.</param>
        /// <param name="index">Out parameter that returns the array element index.</param>
        /// <returns>Return <see langword="true"/> if the <see cref="SerializedProperty"/> is an array element.</returns>
        public static bool IsArrayElement(this SerializedProperty property, out int index)
        {
            const string ARRAY_DATA_END_MASK = "Array.data";

            int start = property.propertyPath.LastIndexOf('[') + 1;
            int end = property.propertyPath.LastIndexOf(']') - 1;

            if (start > 0)
            {
                index = (start == end) 
                    ? int.Parse(property.propertyPath[start].ToString()) 
                    : int.Parse(property.propertyPath.Substring(start, end));

                return property.propertyPath.Substring(0, start - 1).EndsWith(ARRAY_DATA_END_MASK);
            }

            index = -1;

            return false;
        } 
        #endregion
    }
}
