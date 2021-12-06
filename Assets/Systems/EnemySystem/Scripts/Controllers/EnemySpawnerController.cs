using System;
using UnityEngine;
using VisualStudioEX3.Artemis.Assets.EnemySystem.Services;
using VisualStudioEX3.Artemis.Assets.LevelManagement;

namespace VisualStudioEX3.Artemis.Assets.EnemySystem.Controllers
{
    /// <summary>
    /// Enemy Spawaner.
    /// </summary>
    /// <remarks>This component manages <see cref="EnemyController"/> instances.</remarks>
    public class EnemySpawnerController : MonoBehaviour
    {
        #region Methods & Functions
        /// <summary>
        /// Spawn an instance of <see cref="EnemyController"/>.
        /// </summary>
        /// <typeparam name="T"><see cref="EnemyController"/> based type.</typeparam>
        public void SpawnEnemy(Type enemyType)
        {
            EnemyController instance = EnemyControllerFactory.Instance.GetEnemyInstance(enemyType);

            instance.transform.parent = LevelManagerController.Instance.EnemiesRootTransform;
            instance.transform.position = this.transform.position;
            instance.transform.localScale = this.transform.localScale * instance.ScaleFactor;
            instance.transform.forward = this.transform.forward;

            instance.gameObject.SetActive(true);
        }
        #endregion
    }
}
