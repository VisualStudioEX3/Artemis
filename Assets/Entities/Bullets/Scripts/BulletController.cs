using System.Collections;
using UnityEngine;
using VisualStudioEX3.Artemis.Assets.Common.Controllers;

namespace VisualStudioEX3.Artemis.Assets.Bullets.Controllers
{
    [DisallowMultipleComponent, RequireComponent(typeof(AttackController), typeof(Rigidbody))]
    public class BulletController : MonoBehaviour
    {
        #region Constants
        private const float MIN_SPEED = 0.5f;
        private const float MAX_SPEED = 10f;
        private const float DEFAULT_SPEED = 1f;

        private const float MIN_LIFE_TIME = 1f;
        private const float MAX_LIFE_TIME = 10f;
        private const float DEFAULT_LIFE_TIME = 1f; 
        #endregion

        #region Internal vars
        private AttackController _attackController;
        private Rigidbody _rigidBody; 
        #endregion

        #region Inspector fields
        [SerializeField, Range(MIN_SPEED, MAX_SPEED)]
        private float _speed = DEFAULT_SPEED;
        
        [SerializeField, Range(MIN_LIFE_TIME, MAX_LIFE_TIME)]
        private float _lifeTime = DEFAULT_LIFE_TIME;
        #endregion

        #region Initializers
        private void Awake()
        {
            this.SetupAttackController();
            this.SetupRigidBody();
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(this._lifeTime);
            this.DisableBullet();
        }
        #endregion

        #region Methods & Functions
        private void SetupAttackController()
        {
            this._attackController = this.GetComponent<AttackController>();
            this._attackController.OnImpact += this.OnImpact;
        }

        private void SetupRigidBody()
        {
            this._rigidBody = this.GetComponent<Rigidbody>();
            this.SetupForceToRigidBody();
        }

        private Vector3 CalculateVectorImpulse() => this.transform.forward * this._speed;

        private void SetupForceToRigidBody() => this._rigidBody.AddForce(force: this.CalculateVectorImpulse(), mode: ForceMode.Impulse);

        private void DisableBullet() => this.gameObject.SetActive(false);
        #endregion

        #region Event listeners
        private void OnImpact() => this.DisableBullet();
        #endregion
    }
}
