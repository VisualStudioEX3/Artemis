using System;
using UnityEditor;
using UnityEngine;
using VisualStudioEX3.Artemis.Framework.Core.Contracts.Attributes;
using VisualStudioEX3.Artemis.Framework.Core.Contracts.Enums;

namespace VisualStudioEX3.Artemis.Framework.Core.Editor.Components.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(ButtonAttribute))]
    public class ButtonDrawer : PropertyDrawer
    {
        #region Constants
        private static readonly float DEFAULT_BUTTON_SIZE = EditorGUIUtility.singleLineHeight + (EditorGUIUtility.standardVerticalSpacing * 2f);
        private static readonly float LARGE_BUTTON_SIZE = EditorGUIUtility.singleLineHeight * 2f;
        private static readonly float MINI_BUTTON_SIZE = EditorGUIUtility.singleLineHeight;
        #endregion;

        #region Intenral vars
        private ButtonAttribute _attribute;
        #endregion

        #region Methods & Functions
        private bool CheckPropertyType(SerializedProperty property) => property.propertyType == SerializedPropertyType.String;

        private float GetCustomHeight(SerializedProperty property, GUIContent label)
        {
            this._attribute ??= (ButtonAttribute)this.attribute;

            return this._attribute.size switch
            {
                GUIButtonSize.Large => ButtonDrawer.LARGE_BUTTON_SIZE,
                GUIButtonSize.Mini => ButtonDrawer.MINI_BUTTON_SIZE,
                _ => ButtonDrawer.DEFAULT_BUTTON_SIZE,
            };
        }

        private GUIContent GetButtonLabel(GUIContent label) => string.IsNullOrEmpty(this._attribute.customLabel) ? label : new GUIContent(this._attribute.customLabel);

        private bool EnableGUI() => this._attribute.disableOn == GUIButtonDisableEvents.Never ||
                                    (this._attribute.disableOn == GUIButtonDisableEvents.EditorMode && EditorApplication.isPlaying) ||
                                    (this._attribute.disableOn == GUIButtonDisableEvents.PlayMode && !EditorApplication.isPlaying);

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return this.CheckPropertyType(property)
                ? this.GetCustomHeight(property, label)
                : EditorGUIUtility.singleLineHeight;
        }

        private bool ComposeInspectorButton(Rect position, GUIContent label, GUIStyle style)
        {
            return GUI.Button(EditorGUI.IndentedRect(position), this.GetButtonLabel(label), style);
        }

        private GUIStyle GetButtonStyle() => this._attribute.size == GUIButtonSize.Mini ? EditorStyles.miniButton : GUI.skin.button;

        private bool HasTargetMethod(SerializedProperty property) => !string.IsNullOrEmpty(property.stringValue);

        private UnityEngine.Object GetTargetUnityObject(SerializedProperty property) => property.serializedObject.targetObject;

        private void InvokeTargetMethod(SerializedProperty property)
        {
            (this.GetTargetUnityObject(property) as MonoBehaviour).Invoke(property.stringValue, 0f);
            this.ForceUpdateMonoBehaviourTarget(property);
        }

        private void ForceUpdateMonoBehaviourTarget(SerializedProperty property)
        {
            // Force in edit mode to update the Monobehaviour Update logic (needed to Invoke() call can be executed).
            if (!EditorApplication.isPlaying && !EditorApplication.isCompiling)
                EditorUtility.SetDirty(property.serializedObject.targetObject);
        }

        private ArgumentException FormatArgumentException(SerializedProperty property)
        {
            string targetObjectName = this.GetTargetUnityObject(property).name;

            GUI.enabled = true; // Ensure that not disabled the next controls in the inspector.

            return new ArgumentException($"{nameof(ButtonAttribute)}: The target method name is empty! Check if the string variable has a value!", paramName: targetObjectName);
        }
        #endregion

        #region Event listeners
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = this.EnableGUI();

            if (this.ComposeInspectorButton(position, label, this.GetButtonStyle()))
                if (this.HasTargetMethod(property))
                    this.InvokeTargetMethod(property);
                else
                    throw this.FormatArgumentException(property);

            GUI.enabled = true;
        }
        #endregion
    }
}
