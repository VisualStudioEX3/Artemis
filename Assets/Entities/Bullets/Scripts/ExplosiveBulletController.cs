using UnityEngine;
using VisualStudioEX3.Artemis.Assets.Entities.Explosion.Scripts.Services;
using VisualStudioEX3.Artemis.Assets.Explosion.Controllers;

namespace VisualStudioEX3.Artemis.Assets.Bullets.Controllers
{
    public class ExplosiveBulletController : BulletController
    {
        #region Event listeners
        public override void OnTriggerEnter(Collider other)
        {
            this.InstanceExplosion();
            base.OnTriggerEnter(other);
        }

        public override void OnCollisionEnter(Collision collision)
        {
            this.InstanceExplosion();
            base.OnCollisionEnter(collision);
        }
        #endregion

        #region Methods & Functions
        private void InstanceExplosion()
        {
            if (ExplosionControllerFactory.Instance.TryGetInstance(out ExplosionController instance))
            {
                instance.transform.position = this.transform.position;
                instance.transform.rotation = Quaternion.identity;
                instance.gameObject.SetActive(true);
            }
        } 
        #endregion
    }
}
