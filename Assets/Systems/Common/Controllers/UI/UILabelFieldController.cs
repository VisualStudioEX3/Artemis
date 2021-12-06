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
        [SerializeField]
        private string _label = DEFAULT_LABEL_VALUE;
        [SerializeField]
        private string _value;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the text label name.
        /// </summary>
        public string Label
        {
            get => this._label;
            set => this.SetLabel(value);
        }

        /// <summary>
        /// Gets or sets the text value field.
        /// </summary>
        public string Value
        {
            get => this._value;
            set => this.SetValue(value);
        }
        #endregion

        #region Initializers
        private void Awake()
        {
            this.ResolveTextMeshProComponents();
            this.OnValidate();
        }
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
            this.SetLabel(this._label);
            this.SetValue(this._value);
        }

        private void Reset() => this.OnValidate();
        #endregion
    }
}
