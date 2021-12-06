using UnityEngine;
using VisualStudioEX3.Artemis.Assets.Common.Controllers.UI;

namespace VisualStudioEX3.Artemis.Assets.EconomySystem.Controllers.UI
{
    [DisallowMultipleComponent]
    public class EconomyUIController : MonoBehaviour
    {
        #region Inspector fields
        [SerializeField]
        private UILabelFieldController _label;
        #endregion

        #region Intializers & Terminators
        private void Awake() => this.SubscribeEvents();

        private void OnDestroy() => this.UnsubscribeEvents(); 
        #endregion

        #region Methods & Functions
        private void SubscribeEvents()
        {
            EconomyManager.Instance.OnEarn += this.OnEarn;
            EconomyManager.Instance.OnSucessfullPayment += this.OnSucessfullPayment;
            EconomyManager.Instance.OnReset += this.OnReset;
        }

        private void UnsubscribeEvents()
        {
            EconomyManager.Instance.OnEarn -= this.OnEarn;
            EconomyManager.Instance.OnSucessfullPayment -= this.OnSucessfullPayment;
            EconomyManager.Instance.OnReset -= this.OnReset;
        }

        private void SetUnitsValue(int units) => this._label.value = units.ToString();
        #endregion

        #region Event listeners
        private void OnEarn(int units) => this.SetUnitsValue(units);

        private void OnSucessfullPayment(int paid, int left) => this.SetUnitsValue(left);

        private void OnReset() => this.SetUnitsValue(0); 
        #endregion
    }
}
