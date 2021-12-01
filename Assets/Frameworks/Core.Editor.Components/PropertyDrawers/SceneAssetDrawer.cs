using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using VisualStudioEX3.Artemis.Framework.Core.Editor.Components.ExtensionMethods;
using SceneAsset = VisualStudioEX3.Artemis.Framework.Core.Contracts.Models.SceneAsset;
using UnitySceneAsset = UnityEditor.SceneAsset;

namespace VisualStudioEX3.Artemis.Framework.Core.Editor.Components.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(SceneAsset))]
    public class SceneAssetDrawer : PropertyDrawer
    {
        #region Internal vars
        private SerializedProperty _sceneAssetReference;
        private SerializedProperty _assetPath;
        private SerializedProperty _scenePath;
        private SerializedProperty _sceneIndex;

        private UnitySceneAsset _sceneAssetEditor;

        private string _gUID;
        #endregion

        #region Methods & Functions
        private void ResolveProperties(SerializedProperty source,
            out SerializedProperty sceneAssetReference,
            out SerializedProperty assetPath,
            out SerializedProperty scenePath,
            out SerializedProperty sceneIndex)
        {
            sceneAssetReference = source.FindPropertyRelative("_asset");
            assetPath = source.FindPropertyRelative("_assetPath");
            scenePath = source.FindPropertyRelative("_scenePath");
            sceneIndex = source.FindPropertyRelative("_sceneIndex");
        }

        private bool TryGetUnitySceneAssetGUID(SerializedProperty sceneAssetReference, out string gUID)
        {
            Object objectReference = sceneAssetReference.objectReferenceValue;
            gUID = string.Empty;

            return objectReference && AssetDatabase.TryGetGUIDAndLocalFileIdentifier(objectReference, out gUID, out long _);
        }

        private UnitySceneAsset LoadUnitySceneAssetFromGUID(string gUID)
        {
            return AssetDatabase.LoadAssetAtPath<UnitySceneAsset>(AssetDatabase.GUIDToAssetPath(gUID));
        }

        private Rect CalculatePropertyFieldRect(Rect position, SerializedProperty property)
        {
            Rect propertyFieldRect = position;

            if (property.IsArrayElement())
                propertyFieldRect.height -= 2f;

            return propertyFieldRect;
        }

        private void ComposeUnitySceneAssetEditorField(Rect propertyFieldRect,
            GUIContent label,
            ref UnitySceneAsset unitySceneAsset)
        {
            unitySceneAsset = (UnitySceneAsset)EditorGUI.ObjectField(propertyFieldRect, label, unitySceneAsset, typeof(UnitySceneAsset), true);
        }

        private void UpdateUnitySceneAssetReference(SerializedProperty sceneAssetReference, UnitySceneAsset unitySceneAsset)
        {
            sceneAssetReference.objectReferenceValue = unitySceneAsset is not null ? unitySceneAsset : null;
        }

        private void FillData(UnitySceneAsset unitySceneAsset, SerializedProperty assetPath, SerializedProperty scenePath, SerializedProperty sceneIndex)
        {
            assetPath.stringValue = AssetDatabase.GetAssetOrScenePath(unitySceneAsset);
            scenePath.stringValue = assetPath.stringValue.Replace("Assets/", string.Empty).Replace(".unity", string.Empty);
            sceneIndex.intValue = SceneUtility.GetBuildIndexByScenePath(assetPath.stringValue);
        }

        private void ResetData(SerializedProperty assetPath, SerializedProperty scenePath, SerializedProperty sceneIndex)
        {
            assetPath.stringValue = scenePath.stringValue = string.Empty;
            sceneIndex.intValue = -1;
        }

        private void UpdateSceneAssetData(SerializedProperty assetPath,
            SerializedProperty scenePath,
            SerializedProperty sceneIndex,
            UnitySceneAsset unitySceneAsset)
        {
            if (unitySceneAsset)
                this.FillData(unitySceneAsset, assetPath, scenePath, sceneIndex);
            else
                this.ResetData(assetPath, scenePath, sceneIndex);
        }
        #endregion

        #region Event listeners
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            this.ResolveProperties(property,
                out this._sceneAssetReference,
                out this._assetPath,
                out this._scenePath,
                out this._sceneIndex);

            if (this.TryGetUnitySceneAssetGUID(this._sceneAssetReference, out this._gUID))
                this._sceneAssetEditor = this.LoadUnitySceneAssetFromGUID(this._gUID);

            Rect propertyFieldRect = this.CalculatePropertyFieldRect(position, property);

            this.ComposeUnitySceneAssetEditorField(propertyFieldRect, label, ref this._sceneAssetEditor);
            this.UpdateUnitySceneAssetReference(this._sceneAssetReference, this._sceneAssetEditor);
            this.UpdateSceneAssetData(this._assetPath, this._scenePath, this._sceneIndex, this._sceneAssetEditor);
        }
        #endregion
    }
}