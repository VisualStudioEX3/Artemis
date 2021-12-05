using System;
using UnityEditor;
using UnityEngine;
using VisualStudioEX3.Artemis.Framework.Core.Contracts.Attributes;

namespace VisualStudioEX3.Artemis.Framework.Core.Editor.Components.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(LayerAttribute))]
    public class LayerDrawer : PropertyDrawer
    {
        #region Methods & Functions
        private bool CheckPropertyType(SerializedProperty property) => property.propertyType == SerializedPropertyType.Integer;

        private void DrawControl(Rect position, SerializedProperty property, GUIContent label)
        {
            property.intValue = EditorGUI.LayerField(position, label, property.intValue);
        }

        private ArgumentException FormatArgumentException(string propertyName)
        {
            return new ArgumentException($"{nameof(LayerAttribute)}: The property \"{propertyName}\" must be an integer field.");
        }
        #endregion

        #region Event listeners
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (this.CheckPropertyType(property))
                this.DrawControl(position, property, label);
            else
                throw this.FormatArgumentException(property.name);
        }
        #endregion
    }
}
