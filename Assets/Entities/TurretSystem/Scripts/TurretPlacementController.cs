using UnityEngine;

namespace VisualStudioEX3.Artemis.Assets.TurretSystem.Controllers
{
    public class TurretPlacementController : MonoBehaviour
    {
        #region Inspector fields
        [SerializeField]
        private Renderer _triggerRenderer;
        [SerializeField]
        private TurretController _testTurret;
        #endregion

        #region Methods & Functions
        public void CreateTurret()
        {
            // TODO: Implements instanciating of the turret.
            TurretController instance = GameObject.Instantiate(this._testTurret, this.transform);
            instance.Health.OnDeath += this.EnableTrigger;

            this.DisableTrigger();
        }

        private void EnableTrigger() => this._triggerRenderer.gameObject.SetActive(true);

        private void DisableTrigger() => this._triggerRenderer.gameObject.SetActive(false);
        #endregion
    }
}
