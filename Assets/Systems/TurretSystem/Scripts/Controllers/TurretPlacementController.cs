using UnityEngine;
using VisualStudioEX3.Artemis.Assets.TurretSystem.Controllers.UI;

namespace VisualStudioEX3.Artemis.Assets.TurretSystem.Controllers
{
    public class TurretPlacementController : MonoBehaviour
    {
        #region Inspector fields
        [SerializeField]
        private Renderer _triggerRenderer;
        #endregion

        #region Methods & Functions
        /// <summary>
        /// Opens the turret selector dialog.
        /// </summary>
        public void OpenTurretSelectorDialog() => TurretSelectorDialogController.Instance.OpenDialog(this);

        /// <summary>
        /// Creates a new instance of a <see cref="TurretController"/> prefab.
        /// </summary>
        public void CreateTurret(TurretController prefab)
        {
            this.CreateTurretInstance(prefab);
            this.DisableTrigger();
        }

        private void CreateTurretInstance(TurretController prefab)
        {
            TurretController instance = GameObject.Instantiate(prefab, this.transform);
            instance.Health.OnDeath += this.EnableTrigger;
        }

        private void EnableTrigger() => this._triggerRenderer.gameObject.SetActive(true);

        private void DisableTrigger() => this._triggerRenderer.gameObject.SetActive(false);
        #endregion
    }
}
