using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VisualStudioEX3.Artemis.Assets.EnemySystem.Controllers;
using VisualStudioEX3.Artemis.Assets.EnemySystem.Models;
using VisualStudioEX3.Artemis.Framework.Core.Components;

namespace VisualStudioEX3.Artemis.Assets.EnemySystem.Services
{
    /// <summary>
    /// Enemy Controller Factory.
    /// </summary>
    /// <remarks>Singleton factory that stores the all <see cref="EnemyController"/> instances from all waves and serves them.</remarks>
    public class EnemyControllerFactory : MonoBehaviourSingleton<EnemyControllerFactory>
    {
        #region Internal vars
        private List<EnemyController> _instances;
        private int _deaths;
        private WaveController _waveController;
        #endregion

        #region Initializer & Terminator
        public override void Awake()
        {
            base.Awake();

            this._waveController = this.GetComponent<WaveController>();
            this._instances = new();
            this._deaths = 0;
        }

        public override void OnDestroy()
        {
            this.DestroyAllInstances();
            base.OnDestroy();
        } 
        #endregion

        #region Methods & Functions
        /// <summary>
        /// Generate all <see cref="EnemyController"/> instances for all waves in the level.
        /// </summary>
        /// <param name="waves"><see cref="WaveAsset"/> list.</param>
        /// <remarks>All instances are create disabled.</remarks>
        public void GenerateInstances(WaveAsset[] waves)
        {
            foreach (WaveAsset wave in waves)
                this.CreateInstancesFromEnemyWaveAsset(wave);
        }

        private void CreateInstancesFromEnemyWaveAsset(WaveAsset asset)
        {
            foreach (EnemyWaveData enemyType in asset.enemyTypes)
                for (int i = 0; i < enemyType.count; i++)
                    this.CreateEnemyInstance(enemyType.enemy);
        }

        private void CreateEnemyInstance(EnemyController enemyType)
        {
            EnemyController instance = GameObject.Instantiate(enemyType);

            instance.gameObject.SetActive(false);
            this._instances.Add(instance);
        }

        private void DestroyAllInstances()
        {
            for (int i = 0; i < this._instances.Count; i++)
                GameObject.DestroyImmediate(this._instances[i]);
        }

        private bool IsUnusedEnemy(EnemyController enemy) => this.IsDisabledEnemy(enemy) && this.IsEnemyAlive(enemy);

        private bool IsDisabledEnemy(EnemyController enemy) => !enemy.gameObject.activeInHierarchy;

        private bool IsActiveEnemy(EnemyController enemy) => !this.IsDisabledEnemy(enemy) && this.IsEnemyAlive(enemy);

        private bool IsEnemyOfType(EnemyController enemy, Type type) => enemy.GetType() == type;

        private bool IsReadyEnemyToSpawn(EnemyController enemy, Type enemyType) => this.IsEnemyOfType(enemy, enemyType) && this.IsUnusedEnemy(enemy);

        private bool IsEnemyAlive(EnemyController enemy) => enemy.IsAlive;

        /// <summary>
        /// Gets the first instance of the requested type.
        /// </summary>
        /// <param name="enemyType"><see cref="EnemyController"/> based type.</param>
        /// <returns>Returns the first available instance of the requested type.</returns>
        public EnemyController GetEnemyInstance(Type enemyType) => this._instances.FirstOrDefault(e => this.IsReadyEnemyToSpawn(e, enemyType));

        /// <summary>
        /// Gets all active <see cref="EnemyController"/> instances in scene.
        /// </summary>
        /// <returns>Returns an enumeration with all active <see cref="EnemyController"/> instances in scene.</returns>
        public IEnumerable<EnemyController> GetAllActiveEnemies() => this._instances.Where(e => this.IsActiveEnemy(e));
        #endregion
    }
}
