using UnityEngine;
using VisualStudioEX3.Artemis.Assets.Common.Controllers.UI;
using VisualStudioEX3.Artemis.Assets.Scenes.Controllers;

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
        private void Awake() => GameManagerController.OnGameManagerIsIntialized += this.OnGameManagerIsInitialized;

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

        private void SetUnitsValue(int units) => this._label.Value = units.ToString();
        #endregion

        #region Event listeners
        private void OnGameManagerIsInitialized() => this.SubscribeEvents();

        private void OnEarn(int earned, int currentTotal) => this.SetUnitsValue(currentTotal);

        private void OnSucessfullPayment(int paid, int currentTotal) => this.SetUnitsValue(currentTotal);

        private void OnReset() => this.SetUnitsValue(0); 
        #endregion
    }
}
