using System;
using UnityEngine;
using UnityEngine.AI;
using VisualStudioEX3.Artemis.Assets.Common.Controllers;
using VisualStudioEX3.Artemis.Assets.LevelManagement;

namespace VisualStudioEX3.Artemis.Assets.EnemySystem.Controllers
{
    /// <summary>
    /// Enemy Controller.
    /// </summary>
    /// <remarks>Defines the enemy behaviour.</remarks>
    [DisallowMultipleComponent, RequireComponent(typeof(HealthController), typeof(AttackController), typeof(NavMeshAgent)), RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public class EnemyController : MonoBehaviour
    {
        #region Constants
        private const int MIN_REWARD = 5;
        private const int MAX_REWARD = 50;
        private const int DEFAULT_REWARD = 5;

        private const float MIN_SPEED = 1f;
        private const float MAX_SPEED = 10f;
        private const float DEFAULT_SPEED = 1f;

        private const int MIN_PRIORITY = 0;
        private const int MAX_PRIORITY = 10;
        private const int DEFAULT_PRIORITY = 1;
        #endregion

        #region Internal vars
        private HealthController _healthController; 
        #endregion

        #region Inspector fields
        [SerializeField, Range(MIN_REWARD, MAX_REWARD)]
        private int _reward = DEFAULT_REWARD;
        [SerializeField, Range(MIN_SPEED, MAX_SPEED)]
        private float _speed = DEFAULT_SPEED;
        [SerializeField, Range(MIN_PRIORITY, MAX_PRIORITY)]
        private int _priority = DEFAULT_PRIORITY;
        [SerializeField]
        private float _scaleFactor = 1f;
        #endregion

        #region Events
        /// <summary>
        /// Notifies when the enemy is dead.
        /// </summary>
        /// <remarks>Returns an amount of coins as reward.</remarks>
        public event Action<int> OnDead;
        #endregion

        #region Properties
        /// <summary>
        /// Use this value to modify the final scale of the enemy when is spawned.
        /// </summary>
        public float ScaleFactor => this._scaleFactor;

        /// <summary>
        /// Gets if this enemy is alive.
        /// </summary>
        public bool IsAlive => this._healthController.Life > 0;
        #endregion

        #region Initializers
        private void Awake()
        {
            this._healthController = this.GetComponent<HealthController>();
            this.SetupOnDeadEvent();
        }

        private void Start() => this.SetupNavAgent();
        #endregion

        #region Methods & Functions
        private void SetupOnDeadEvent() => this.GetComponent<HealthController>().OnDeath += this.OnEnemyDead;

        private void SetupNavAgent()
        {
            NavMeshAgent agent = this.GetComponent<NavMeshAgent>();

            agent.speed = this._speed;
            agent.radius = this._scaleFactor;
            agent.avoidancePriority = this._priority;

            agent.SetDestination(LevelManagerController.Instance.PlayerBase.transform.position);
        }
        #endregion

        #region Event listeners
        private void OnEnemyDead()
        {
            this.gameObject.SetActive(false);
            this.OnDead?.Invoke(this._reward);
        }
        #endregion
    }
}
