using System.Collections;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using VisualStudioEX3.Artemis.Framework.Core.Components;

namespace VisualStudioEX3.Artemis.Framework.Core.Editor.Components
{
    [CustomEditor(typeof(ScrollMaterial))]
    public class ScrollMaterialEditor : UnityEditor.Editor
    {
        #region Internal vars
        private SerializedProperty _speed;
        private EditorCoroutine _coroutine;
        #endregion

        #region Methods & Functions
        private EditorCoroutine StartScroll() => EditorCoroutineUtility.StartCoroutine(this.EditorUpdate(), this);

        private void StopScroll() => EditorCoroutineUtility.StopCoroutine(this._coroutine);
        #endregion

        #region Coroutines
        private IEnumerator EditorUpdate()
        {
            while (true)
            {
                (this.target as ScrollMaterial).UpdateScroll();
                yield return null; 
            }
        } 
        #endregion

        #region Event listeners
        private void OnEnable()
        {
            this._speed = this.serializedObject.FindProperty(nameof(this._speed));
            this._coroutine = this.StartScroll();
        }

        private void OnDestroy() => this.StopScroll();
        #endregion
    }
}
