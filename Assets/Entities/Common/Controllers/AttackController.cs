using System;
using System.Collections;
using UnityEngine;
using VisualStudioEX3.Artemis.Framework.Core.Contracts.Attributes;

namespace VisualStudioEX3.Artemis.Assets.Common.Controllers
{
    /// <summary>
    /// Attack Controller.
    /// </summary>
    /// <remarks>This component allow to perform continuous damage to a target when is in contact with it, or a single damage on impact (e.g. bullets).
    /// Works with collisions and triggers.</remarks>
    [DisallowMultipleComponent]
    public class AttackController : MonoBehaviour
    {
        #region Constants
        private const int MIN_DAMAGE = 1;
        private const int MAX_DAMAGE = 10;
        private const int DEFAULT_DAMAGE = 1;

        private const float DEFAULT_DAMAGE_TIME_INTERVAL = 1f;
        #endregion

        #region Internal vars
        private WaitForSeconds _damageTimeIntervalYield; 
        #endregion

        #region Inspector fields
        [SerializeField, Range(MIN_DAMAGE, MAX_DAMAGE)]
        private int _damage = DEFAULT_DAMAGE;
        [SerializeField, Tag]
        private string _targetTag;
        [SerializeField, Tooltip("When is a bullet, this component only apply damage the first time when contact a target enemy.")]
        private bool _isBullet = false;
        [SerializeField]
        private float _damageTimeInterval = DEFAULT_DAMAGE_TIME_INTERVAL;
        #endregion

        #region Events
        /// <summary>
        /// Notify when the object are impacted, and applied damage, to the target.
        /// </summary>
        /// <remarks>Only used when <see cref="_isBullet"/> field is <see langword="true"/>.</remarks>
        public event Action OnImpact;
        #endregion

        private void Awake() => this.CreateDamageTimeIntervalYield();

        #region Methods & Functions
        private void ClampDamageTimeIntervalField() => this._damageTimeInterval = Mathf.Max(this._damageTimeInterval, 0f);

        private void CreateDamageTimeIntervalYield() => this._damageTimeIntervalYield = new WaitForSeconds(this._damageTimeInterval);

        private bool CheckCollisionTag(GameObject gameObject) => !string.IsNullOrEmpty(this._targetTag) && gameObject.CompareTag(this._targetTag);

        private bool TryGetHealthControllerFromCollision(GameObject gameObject, out HealthController healthController)
        {
            return gameObject.TryGetComponent<HealthController>(out healthController);
        }

        private bool IsTargetEnemy(GameObject gameObject, out HealthController healthController)
        {
            healthController = null;

            return this.CheckCollisionTag(gameObject) && this.TryGetHealthControllerFromCollision(gameObject, out healthController);
        }

        private void ApplySingleDamage(HealthController health)
        {
            health.ApplyDamage(this._damage);
            this.OnImpact?.Invoke();
        }

        private void StartToApplyContinousDamage(HealthController target) => this.StartCoroutine(this.ContinuousDamageCoroutine(target));

        private void ProcessCollisionEnter(GameObject gameObject)
        {
            if (this.IsTargetEnemy(gameObject, out HealthController target))
                if (this._isBullet)
                    this.ApplySingleDamage(target);
                else
                    this.StartToApplyContinousDamage(target);
        }

        private void ProcessCollisionExit(GameObject gameObject)
        {
            if (this.CheckCollisionTag(gameObject))
                this.StopAllCoroutines();
        }
        #endregion

        #region Event listeners
        private void OnValidate() => this.ClampDamageTimeIntervalField();

        private void OnCollisionEnter(Collision collision) => this.ProcessCollisionEnter(collision.gameObject);

        private void OnCollisionExit(Collision collision) => this.ProcessCollisionExit(collision.gameObject);

        private void OnTriggerEnter(Collider other) => this.ProcessCollisionEnter(other.gameObject);

        private void OnTriggerExit(Collider other) => this.ProcessCollisionExit(other.gameObject);
        #endregion

        #region Coroutines
        private IEnumerator ContinuousDamageCoroutine(HealthController health)
        {
            while (true)
            {
                health.ApplyDamage(this._damage);
                yield return this._damageTimeIntervalYield;
            }
        } 
        #endregion
    }
}
