using UnityEngine;
using VisualStudioEX3.Artemis.Assets.EconomySystem.Controllers;
using VisualStudioEX3.Artemis.Assets.TurretSystem.Services;

namespace VisualStudioEX3.Artemis.Assets.TurretSystem.Controllers
{
    public class TurretPlacementController : MonoBehaviour
    {
        #region Inspector fields
        [SerializeField]
        private Renderer _triggerRenderer;
        #endregion

        #region Methods & Functions
        public void CreateTurret()
        {
            // TODO: Implements economy system and apply a cost to each turret.
            // TODO: Implements an UI to select the type of turret to instantiate.
            if (EconomyManager.Instance.TryPayUnits(0))
            {
                TurretController prefab = this.GetTurretOfType(0);
                this.CreateTurretInstance(prefab);
                this.DisableTrigger();
            }
        }

        private TurretController GetTurretOfType(int indexType) => TurretControllerFactory.Instance.GetInstance(indexType);

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
