using System;
using UnityEngine;
using VisualStudioEX3.Artemis.Assets.EnemySystem.Controllers;
using VisualStudioEX3.Artemis.Framework.Core.Components;

namespace VisualStudioEX3.Artemis.Assets.EconomySystem.Controllers
{
    [DisallowMultipleComponent]
    public class EconomyManager : MonoBehaviourSingleton<EconomyManager>
    {
        #region Constants
        private const int DEFAULT_INITIAL_CREDITS = 15; 
        #endregion

        #region Inspector fields
        [SerializeField, Tooltip("Max amount of units that the player can earn."), Min(0)]
        private int _maxCredits;
        [SerializeField, Tooltip("Is recomended to setup an initial value to allow the player put a few turrets at the start."), Min(0)]
        private int _initialCurrentCredits = DEFAULT_INITIAL_CREDITS;
        #endregion

        #region Properties
        /// <summary>
        /// Current amount of credits.
        /// </summary>
        public int CurrentCredits { get; private set; }

        /// <summary>
        /// Max amount of credits that the player can earn.
        /// </summary>
        public int MaxCredits => this._maxCredits;
        #endregion

        #region Events
        /// <summary>
        /// Notifies when the player earn credits.
        /// </summary>
        /// <remarks>Returns the amount of credits earned and the current total credits.</remarks>
        public event Action<int, int> OnEarn;

        /// <summary>
        /// Notifies when the player achieve a sucessfull payment operation.
        /// </summary>
        /// <remarks>Returns the amount of credits used to pay and the rest of the credits left.</remarks>
        public event Action<int, int> OnSucessfullPayment;

        /// <summary>
        /// Notifies when the economy controller is reseted.
        /// </summary>
        public event Action OnReset;
        #endregion

        #region Initializers & Terminators
        public override void Awake()
        {
            base.Awake();
            this.CurrentCredits = this._initialCurrentCredits;
        }

        public override void OnDestroy() => base.OnDestroy();
        #endregion

        #region Methods & Functions
        public override bool IsPersistentBetweenScenes() => true;

        private void EarnUnits(int units)
        {
            this.CurrentCredits = Mathf.Min(this.CurrentCredits + units, this._maxCredits);
            this.OnEarn?.Invoke(units, this.CurrentCredits);
        }

        /// <summary>
        /// try to pay with an amount of credits.
        /// </summary>
        /// <param name="price">Units to pay.</param>
        /// <returns>Returns <see langword="true"/> if the player have enough credits and rest them from the current credits.</returns>
        public bool TryPayCredits(int price)
        {
            int afterPayCredits = this.CurrentCredits - price;

            if (afterPayCredits >= 0)
            {
                this.CurrentCredits = afterPayCredits;
                this.OnSucessfullPayment?.Invoke(price, this.CurrentCredits);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Reset to initial value the current amount of credits.
        /// </summary>
        public void ResetUnits()
        {
            this.CurrentCredits = this._initialCurrentCredits;
            this.OnReset?.Invoke();
        }
        #endregion

        #region Event listeners
        /// <summary>
        /// Gets a reward when a enemy dead.
        /// </summary>
        /// <param name="reward">Amount of credits to earn.</param>
        /// <remarks>All <see cref="EnemyController"/> instances must be subscribe this event listener to his <see cref="EnemyController.OnDead"/> event.</remarks>
        public void OnEnemyDead(int reward) => this.EarnUnits(reward);
        #endregion
    }
}
