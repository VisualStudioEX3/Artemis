using System;
using UnityEngine;
using VisualStudioEX3.Artemis.Assets.EnemySystem.Constants;
using VisualStudioEX3.Artemis.Assets.EnemySystem.Controllers;

namespace VisualStudioEX3.Artemis.Assets.EnemySystem.Models
{
    [Serializable]
    public struct EnemyWaveData
    {
        #region Public vars
        /// <summary>
        /// Enemy type to spawn.
        /// </summary>
        public EnemyController enemy;

        /// <summary>
        /// Number of enemies of this type.
        /// </summary>
        public int count;

        /// <summary>
        /// Wait time before start to spawn.
        /// </summary>
        [Tooltip(TooltipMessageConstants.TIME_IN_SECONDS_TOOLTIP_MESSAGE)]
        public float startToSpawnDelay;

        /// <summary>
        /// Maximum time to rando wait to spawn the next enemy.
        /// </summary>
        [Tooltip(TooltipMessageConstants.TIME_IN_SECONDS_TOOLTIP_MESSAGE)]
        public float maxDelayBetweenSpawns;
        #endregion
    }
}
