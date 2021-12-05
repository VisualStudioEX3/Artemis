using System;
using UnityEditor;
using UnityEngine;
using VisualStudioEX3.Artemis.Framework.Core.Contracts.Attributes;

namespace VisualStudioEX3.Artemis.Framework.Core.Editor.Components.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(TagAttribute))]
    public class TagDrawer : PropertyDrawer
    {
        #region Constants
        private const string DEFAULT_TAG = "Untagged";
        #endregion

        #region Methods & Functions
        private bool CheckPropertyType(SerializedProperty property) => property.propertyType == SerializedPropertyType.String;

        private string GetTagString(SerializedProperty property) => string.IsNullOrEmpty(property.stringValue.Trim()) ? TagDrawer.DEFAULT_TAG : property.stringValue;

        private void ComposeTagField(Rect position, SerializedProperty property, GUIContent label)
        {
            property.stringValue = EditorGUI.TagField(position, label, this.GetTagString(property));
        }

        private ArgumentException FormatArgumentException(string propertyName)
        {
            return new ArgumentException($"{nameof(TagAttribute)}: The property \"{propertyName}\" must be an string field.");
        }
        #endregion

        #region Event listeners
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (this.CheckPropertyType(property))
                this.ComposeTagField(position, property, label);
            else
                throw this.FormatArgumentException(property.name);
        }
        #endregion
    }
}
