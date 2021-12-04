using System;
using UnityEngine;
using UnityEngine.AI;
using VisualStudioEX3.Artemis.Assets.LevelManagement;

namespace VisualStudioEX3.Artemis.Assets.EnemySystem.Controllers
{
    /// <summary>
    /// Base class for enemies.
    /// </summary>
    public class EnemyController : MonoBehaviour
    {
        #region Inspector fields
        [SerializeField]
        private int _health = 5;
        [SerializeField]
        private int _reward = 5;
        [SerializeField]
        private float _speed = 1f;
        #endregion

        #region Events
        /// <summary>
        /// Notifies when the enemy is dead.
        /// </summary>
        /// <remarks>Returns an amount of coins as reward.</remarks>
        public event Action<int> OnDead;
        #endregion

        #region Initializers
        private void Start() => this.SetupNavAgent();
        #endregion

        #region Methods & Functions
        private void SetupNavAgent()
        {
            NavMeshAgent agent = this.GetComponent<NavMeshAgent>();

            agent.speed = this._speed;
            agent.SetDestination(LevelManagerController.Instance.PlayerBase.transform.position);
        }
        #endregion

        #region Event listeners
        private void OnCollisionEnter(Collision collision)
        {
            // TODO: Get the component of the collision object.
            // TODO: If is a <turret bullet type>, apply the shoot damage.
        } 
        #endregion
    }
}
