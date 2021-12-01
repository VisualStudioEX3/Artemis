using System;
using UnityEngine;

namespace VisualStudioEX3.Artemis.Assets.EnemySystem.Controllers
{
    /// <summary>
    /// Base class for enemies.
    /// </summary>
    public class EnemyController : MonoBehaviour
    {
        #region Events
        /// <summary>
        /// Notifies when the enemy is dead.
        /// </summary>
        public event Action OnDead; 
        #endregion
    }
}
