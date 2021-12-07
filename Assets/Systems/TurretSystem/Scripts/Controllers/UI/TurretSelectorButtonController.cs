using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VisualStudioEX3.Artemis.Assets.EconomySystem.Controllers;
using VisualStudioEX3.Artemis.Assets.TurretSystem.Services;

namespace VisualStudioEX3.Artemis.Assets.TurretSystem.Controllers.UI
{
    [DisallowMultipleComponent]
    public class TurretSelectorButtonController : MonoBehaviour
    {
        #region Constants
        private const string PRICE_LABEL_CONTROL_NAME = "Price";
        private const string PRICE_LABEL_TEXT = "{0} credits";
        #endregion

        #region Internal vars
        private Button _button;
        private TextMeshProUGUI _priceTextTMPro;
        private TurretController _prefabReference;
        #endregion

        #region Inspector fields
        [SerializeField]
        private int _turretTypeIndex;
        #endregion

        #region Intializers
        private void Awake() => this.OnEnable();
        #endregion

        #region Methods & Functions
        private void SubscribeEvents()
        {
            this.UnsubscribeEvents();

            EconomyManager.Instance.OnEarn += this.OnCreditsEarned;
            EconomyManager.Instance.OnSucessfullPayment += this.OnSuccessfullPayment;
        }

        private void UnsubscribeEvents()
        {
            EconomyManager.Instance.OnEarn -= this.OnCreditsEarned;
            EconomyManager.Instance.OnSucessfullPayment -= this.OnSuccessfullPayment;
        }

        private void ResolveComponents()
        {
            this.ResolveButtonComponent();
            this.ResolvePriceTMProComponent();
        }

        private void ResolveButtonComponent() => this._button ??= this.GetComponent<Button>();

        private void ResolveTurretPrefab() => this._prefabReference ??= TurretControllerFactory.Instance.GetInstance(this._turretTypeIndex);

        private void ResolvePriceTMProComponent() => this._priceTextTMPro ??= this.GetComponentsInChildren<TextMeshProUGUI>().First(e => e.name == PRICE_LABEL_CONTROL_NAME);

        private int GetPrice() => this._prefabReference.Price;

        private int GetCurrentCredits() => EconomyManager.Instance.CurrentUnits;

        private void SetPriceLabel() => this._priceTextTMPro.text = string.Format(PRICE_LABEL_TEXT, this._prefabReference.Price);

        private void EnableButton() => this._button.interactable = true;

        private void DisableButton() => this._button.interactable = false;

        private void CheckIfHaveEnoughCreditsToBuy()
        {
            if (this.GetPrice() > this.GetCurrentCredits())
                this.DisableButton();
            else
                this.EnableButton();
        }

        public void BuyAndCreateTurret()
        {
            TurretSelectorDialogController.Instance.TurretPlacementCaller.CreateTurret(this._prefabReference);
            TurretSelectorDialogController.Instance.CloseDialog();
        }
        #endregion

        #region Event listeners
        private void OnEnable()
        {
            this.SubscribeEvents();
            this.ResolveComponents();
            this.ResolveTurretPrefab();
            this.SetPriceLabel();
            this.CheckIfHaveEnoughCreditsToBuy();
        }

        private void OnCreditsEarned(int earned, int currentTotal) => this.CheckIfHaveEnoughCreditsToBuy();

        private void OnSuccessfullPayment(int paid, int currentTotal) => this.CheckIfHaveEnoughCreditsToBuy();
        #endregion
    }
}
