using System;
using UnityEngine;
using VisualStudioEX3.Artemis.Assets.EnemySystem.Controllers;
using VisualStudioEX3.Artemis.Framework.Core.Components;

namespace VisualStudioEX3.Artemis.Assets.EconomySystem.Controllers
{
    [DisallowMultipleComponent]
    public class EconomyManager : MonoBehaviourSingleton<EconomyManager>
    {
        #region Inspector fields
        [SerializeField, Tooltip("Max amount of units that the player can earn."), Min(0)]
        private int _maxUnits;
        #endregion

        #region Properties
        /// <summary>
        /// Current amount of units.
        /// </summary>
        public int CurrentUnits { get; private set; }

        /// <summary>
        /// Max amount of units that the player can earn.
        /// </summary>
        public int MaxUnits => this._maxUnits;
        #endregion

        #region Events
        /// <summary>
        /// Notifies when the player earn units.
        /// </summary>
        public event Action<int> OnEarn;

        /// <summary>
        /// Notifies when the player achieve a sucessfull payment operation.
        /// </summary>
        /// <remarks>Returns the amount of units used to pay and the rest of the units left.</remarks>
        public event Action<int, int> OnSucessfullPayment;

        /// <summary>
        /// Notifies when the economy controller is reseted.
        /// </summary>
        public event Action OnReset;
        #endregion

        #region Methods & Functions
        private void EarnUnits(int units)
        {
            this.CurrentUnits = Mathf.Min(this.CurrentUnits + units, this._maxUnits);
            this.OnEarn?.Invoke(units);
        }

        /// <summary>
        /// try to pay with an amount of units.
        /// </summary>
        /// <param name="price">Units to pay.</param>
        /// <returns>Returns <see langword="true"/> if the player have enough units and rest them from the current units.</returns>
        public bool TryPayUnits(int price)
        {
            int afterPayUnits = this.CurrentUnits - price;

            if (afterPayUnits >= 0)
            {
                this.CurrentUnits = afterPayUnits;
                this.OnSucessfullPayment?.Invoke(price, this.CurrentUnits);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Reset to zero the current amount of units.
        /// </summary>
        public void ResetUnits()
        {
            this.CurrentUnits = 0;
            this.OnReset?.Invoke();
        }
        #endregion

        #region Event listeners
        /// <summary>
        /// Gets a reward when a enemy dead.
        /// </summary>
        /// <param name="reward">Amount of units to earn.</param>
        /// <remarks>All <see cref="EnemyController"/> instances must be subscribe this event listener to his <see cref="EnemyController.OnDead"/> event.</remarks>
        public void OnEnemyDead(int reward) => this.EarnUnits(reward);
        #endregion
    }
}
