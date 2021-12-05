using System;
using UnityEngine;

namespace VisualStudioEX3.Artemis.Assets.Common.Controllers
{
    /// <summary>
    /// Health controller.
    /// </summary>
    /// <remarks>This component able to manage the life of an object and how to apply damage to it.</remarks>
    [DisallowMultipleComponent]
    public class HealthController : MonoBehaviour
    {
        #region Inspector fields
        [SerializeField]
        private int _life;
        #endregion

        #region Events
        /// <summary>
        /// Notifies when the life reach to zero.
        /// </summary>
        public event Action OnDeath;

        /// <summary>
        /// Notifies when the object gets damage.
        /// </summary>
        /// <remarks>Returns the damage received and the current life.</remarks>
        public event Action<int, int> OnDamage;
        #endregion

        #region Methods & Functions
        /// <summary>
        /// Apply damage.
        /// </summary>
        /// <param name="damage">Amount of damage.</param>
        public void ApplyDamage(int damage)
        {
            if (!this.IsDead())
            {
                this.SubstractLife(damage);
                this.NotifyDamage(damage);
                this.CheckAndNotifyDeath();
            }
        }

        private void SubstractLife(int damage) => this._life = Mathf.Max(this._life - damage, 0);

        private void NotifyDamage(int damage) => this.OnDamage?.Invoke(damage, this._life);

        private void CheckAndNotifyDeath()
        {
            if (this.IsDead())
                this.OnDeath?.Invoke();
        }

        private bool IsDead() => this._life == 0;
        #endregion
    }
}
