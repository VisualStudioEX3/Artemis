using UnityEngine;

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
            // TODO: Implements instanciating of the turret.
            this.DisableTrigger();
        }

        private void DisableTrigger() => this._triggerRenderer.enabled = false;
        #endregion
    }
}
