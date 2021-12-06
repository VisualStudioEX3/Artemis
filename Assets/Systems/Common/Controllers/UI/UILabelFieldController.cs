using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace VisualStudioEX3.Artemis.Assets.Common.Controllers.UI
{
    [DisallowMultipleComponent, ExecuteInEditMode]
    public class UILabelFieldController : MonoBehaviour
    {
        #region Constants
        private const string LABEL_GAMEOBJECT_NAME = "Label";
        private const string VALUE_GAMEOBJECT_NAME = "Value";

        private const string DEFAULT_LABEL_VALUE = "New UI text field";
        #endregion

        #region Internal vars
        private TextMeshProUGUI _labelTMPro;
        private TextMeshProUGUI _valueTMPro;
        #endregion

        #region Public vars
        public string label = DEFAULT_LABEL_VALUE;
        public string value;
        #endregion

        #region Initializers
        private void Awake() => this.ResolveTextMeshProComponents();
        #endregion

        #region Methods & Functions
        private void ResolveTextMeshProComponents()
        {
            IEnumerable<TextMeshProUGUI> components = this.GetComponentsInChildren<TextMeshProUGUI>();

            this._labelTMPro = components.First(e => e.name == LABEL_GAMEOBJECT_NAME);
            this._valueTMPro = components.First(e => e.name == VALUE_GAMEOBJECT_NAME);
        }

        private void SetLabel(string text) => this.SafeSetValueToTMProControl(this._labelTMPro, text);
        
        private void SetValue(string text) => this.SafeSetValueToTMProControl(this._valueTMPro, text);

        private void SafeSetValueToTMProControl(TextMeshProUGUI control, string text)
        {
            if (control)
                control.text = text;
        }
        #endregion

        #region Event listeners
        private void OnValidate()
        {
            this.SetLabel(this.label);
            this.SetValue(this.value);
        }

        private void Reset() => this.OnValidate();
        #endregion
    }
}
