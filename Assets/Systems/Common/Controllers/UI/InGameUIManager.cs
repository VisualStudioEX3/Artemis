using UnityEngine;
using VisualStudioEX3.Artemis.Framework.Core.Components;

namespace VisualStudioEX3.Artemis.Assets.Common.Controllers.UI
{
    [DisallowMultipleComponent]
    public class InGameUIManager : MonoBehaviourSingleton<InGameUIManager>
    {
        #region Inspector fields
        [SerializeField]
        private Transform _inGameUIRootTransform;
        #endregion

        #region Initializers & Terminators
        public override void Awake()
        {
            base.Awake();
            this.Hide();
        }

        public override void OnDestroy() => base.OnDestroy();
        #endregion

        #region Methods & Functions
        /// <summary>
        /// Shows the In-Game User Interface.
        /// </summary>
        public void Show() => this._inGameUIRootTransform.gameObject.SetActive(true);

        /// <summary>
        /// Hides the In-Game User Interface.
        /// </summary>
        public void Hide() => this._inGameUIRootTransform.gameObject.SetActive(false);
        #endregion
    }
}
