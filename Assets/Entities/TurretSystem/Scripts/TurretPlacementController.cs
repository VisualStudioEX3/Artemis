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
            // Testing Turret 1:
            GameObject.Instantiate(this._testTurret, this.transform);

            this.DisableTrigger(); // TODO: Replaces disable trigger by disabling the gameObject itself.
        }

        private void DisableTrigger() => this._triggerRenderer.enabled = false;
        #endregion
    }
}
