using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using VisualStudioEX3.Artemis.Assets.Common.Controllers;

namespace VisualStudioEX3.Artemis.Assets.Lifebar.Controllers
{
    [DisallowMultipleComponent]
    public class LifebarController : MonoBehaviour
    {
        #region Internal vars
        private const string BORDER_IMAGE_NAME = "Border";
        private const string BAR_IMAGE_NAME = "Bar";
        #endregion

        #region Internal vars
        private Canvas _canvas;
        private Image _border;
        private Image _bar;

        private int _life;
        #endregion

        #region Inspector fields
        [SerializeField]
        private HealthController _healthController;
        [SerializeField]
        private Color _barColor = Color.red;
        [SerializeField]
        private Color _borderColor = Color.white;
        #endregion

        #region Initializers
        private void Awake()
        {
            this.ResolveComponents();
            this.SetupCanvasCamera();
            this.SetupColors();
            this.SubscribeEvents();
            this.GetInitialLifeValue();
            this.InitialSetup();
        }
        #endregion

        #region Update logic
        private void Update() => this.LookAtCamera(); 
        #endregion

        #region Methods & Functions
        private void ResolveComponents()
        {
            this.ResolveCanvasComponent();
            this.ResolveImageComponents();
        }

        private void ResolveImageComponents()
        {
            IEnumerable<Image> images = this.GetComponentsInChildren<Image>();
            this._border = images.First(e => e.name == BORDER_IMAGE_NAME);
            this._bar = images.First(e => e.name == BAR_IMAGE_NAME);
        }

        private void ResolveCanvasComponent() => this._canvas = this.GetComponentInChildren<Canvas>();

        private void SetupCanvasCamera() => this._canvas.worldCamera = Camera.main;

        private void SetupColors()
        {
            this._border.color = this._borderColor;
            this._bar.color = this._barColor;
        }

        private void GetInitialLifeValue() => this._life = this._healthController.Life;

        private void SubscribeEvents() => this._healthController.OnDamage += this.OnDamage;

        private void InitialSetup() => this.SetFillAmount(this._life);

        private void SetFillAmount(int currentLife) => this._bar.fillAmount = (float)currentLife / (float)this._life;

        private void LookAtCamera() => this.transform.forward = (Camera.main.transform.position - this.transform.position).normalized;
        #endregion

        #region Event listeners
        private void OnDamage(int damage, int currentLife) => this.SetFillAmount(currentLife);
        #endregion
    }
}
