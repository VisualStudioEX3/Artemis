using System.Collections;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEngine;
using VisualStudioEX3.Artemis.Framework.Core.Components.Materials;

namespace VisualStudioEX3.Artemis.Framework.Core.Editor.Components.Materials
{
    [CustomEditor(typeof(ScrollMaterial))]
    public class ScrollMaterialEditor : UnityEditor.Editor
    {
        #region Internal vars
        private SerializedProperty _speed;
        private SerializedProperty _runInEditMode;

        private EditorCoroutine _coroutine;
        #endregion

        #region Methods & Functions
        private EditorCoroutine StartScroll() => EditorCoroutineUtility.StartCoroutine(this.EditorUpdate(), this);
        
        private void StopScroll() => EditorCoroutineUtility.StopCoroutine(this._coroutine);
        
        private ScrollMaterial GetTarget() => this.target as ScrollMaterial;
        
        private bool RunInEditMode() => this._runInEditMode.boolValue;
        
        private bool IsComponentEnabled() => this.GetTarget().enabled;
        
        private bool CanUpdate() => this.RunInEditMode() && this.IsComponentEnabled();
        #endregion

        #region Coroutines
        private IEnumerator EditorUpdate()
        {
            while (true)
            {
                this.GetTarget().UpdateScroll();

                yield return new WaitUntil(() => this.CanUpdate());
                yield return null; // Wait for "end frame" event in editor.
            }
        } 
        #endregion

        #region Event listeners
        private void OnEnable()
        {
            this._speed = this.serializedObject.FindProperty(nameof(this._speed));
            this._runInEditMode = this.serializedObject.FindProperty(nameof(this._runInEditMode));

            this._coroutine = this.StartScroll();
        }

        private void OnDisable()
        {
            this.StopScroll();
            this.GetTarget().ResetScroll();
        }

        private void OnDestroy() => this.OnDisable();
        #endregion
    }
}
