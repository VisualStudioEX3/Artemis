using System;
using UnityEditor;
using UnityEngine;
using VisualStudioEX3.Artemis.Framework.Core.Contracts.Attributes;

namespace VisualStudioEX3.Artemis.Framework.Core.Editor.Components.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(TexturePreviewAttribute))]
    public class TexturePreviewDrawer : PropertyDrawer
    {
        #region Constants
        private static readonly float FIELD_SIZE = EditorGUIUtility.singleLineHeight * 4f;
        private const string TYPE_EXCEPTION_MESSAGE = "The type of the property must be a " + nameof(Texture) + ", " + nameof(Texture2D) + ", " + nameof(RenderTexture) + ", " + nameof(Cubemap) + " or " + nameof(Sprite);
        #endregion

        #region Internal vars
        private string _tooltip = string.Empty;
        #endregion

        #region Properties
        private Type FieldType => this.fieldInfo.FieldType; 
        #endregion

        #region Methods & Functions
        public override bool CanCacheInspectorGUI(SerializedProperty property)
        {
            // This grants that the texture always is rendered.
            // Source: https://answers.unity.com/questions/1311926/texture2d-in-scriptableobjects-property-drawer-exp.html?childToView=1401566#answer-1401566
            return false;
        }

        private float GetCustomHeight(SerializedProperty property, GUIContent label) => TexturePreviewDrawer.FIELD_SIZE;

        private bool CheckPropertyType(SerializedProperty property)
        {
            return this.FieldType == typeof(Texture) ||
                   this.FieldType == typeof(Texture2D) ||
                   this.FieldType == typeof(RenderTexture) ||
                   this.FieldType == typeof(Cubemap) ||
                   this.FieldType == typeof(Sprite);
        }

        public sealed override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return this.CheckPropertyType(property) ? this.GetCustomHeight(property, label) : EditorGUIUtility.singleLineHeight;
        }

        private string GetTooltipAttributeValue()
        {
            if (string.IsNullOrEmpty(this._tooltip))
            {
                object[] customAttributes = this.fieldInfo.GetCustomAttributes(typeof(TooltipAttribute), false);
                if (customAttributes.Length > 0)
                    this._tooltip = (customAttributes[0] as TooltipAttribute).tooltip;
            }

            return this._tooltip;
        }

        private void DrawTexturePreviewField(Rect position, SerializedProperty property, GUIContent label)
        {
            this.CreatePrefixLabel(position, label);
            Rect fieldRect = this.CalculateFieldRect(position);
            this.ComposeInspectorField(fieldRect, property);
        }

        private void CreatePrefixLabel(Rect position, GUIContent label)
        {
            Rect prefixRect = position;
            prefixRect.width = EditorGUIUtility.labelWidth;
            EditorGUI.PrefixLabel(prefixRect, label);
        }

        private Rect CalculateFieldRect(Rect position)
        {
            Rect fieldRect = position;
            fieldRect.x = fieldRect.xMax - fieldRect.height;
            fieldRect.width = fieldRect.height;

            return fieldRect;
        }

        private void ComposeInspectorField(Rect fieldRect, SerializedProperty property)
        {
            bool allowSceneObjects = (this.attribute as TexturePreviewAttribute).allowSceneObjects;

            int indent = EditorGUI.indentLevel;
            {
                EditorGUI.indentLevel = 0;
                property.objectReferenceValue = EditorGUI.ObjectField(fieldRect, property.objectReferenceValue, this.FieldType, allowSceneObjects);
            }
            EditorGUI.indentLevel = indent;
        }

        private void ShowTypeExceptionMessage(Rect position) => EditorGUI.LabelField(position, TYPE_EXCEPTION_MESSAGE);
        #endregion

        #region Event listeners
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            label.tooltip = this.GetTooltipAttributeValue();

            if (this.CheckPropertyType(property))
                this.DrawTexturePreviewField(position, property, label);
            else
            {
                this.ShowTypeExceptionMessage(position);
                throw new ArgumentException($"{nameof(TexturePreviewAttribute)}: {TYPE_EXCEPTION_MESSAGE}");
            }
        }
        #endregion
    }
}
