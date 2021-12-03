using System.Collections.Generic;
using UnityEngine;
using VisualStudioEX3.Artemis.Assets.EnemySystem.Controllers;
using VisualStudioEX3.Artemis.Assets.LevelManagement.Constants;
using VisualStudioEX3.Artemis.Assets.Player.Controllers;
using VisualStudioEX3.Artemis.Assets.Scenes.Controllers;
using VisualStudioEX3.Artemis.Framework.Core.Components;

namespace VisualStudioEX3.Artemis.Assets.LevelManagement
{
    [AddComponentMenu(ComponentMenuPaths.LEVEL_MANAGER_CONTROLLER_COMPONENT_MENU_NAMESPACE), DisallowMultipleComponent]
    public class LevelManagerController : MonoBehaviourSingleton<LevelManagerController>
    {
        #region Inspector fields
        [SerializeField]
        private Renderer _editorBattlegroundGrid;
        [SerializeField]
        private Transform _enemiesRootObject;
        #endregion

        #region Properties
        /// <summary>
        /// Player Base reference.
        /// </summary>
        /// <remarks>Use it for enemy AI.</remarks>
        public PlayerBaseController PlayerBase { get; private set; }

        /// <summary>
        /// Enemy spawn locations.
        /// </summary>
        /// <remarks>Use it for spawn enemies from <see cref="EnemySpawnerController"/> instances.</remarks>
        public IReadOnlyList<EnemySpawnerController> EnemySpawnLocations { get; private set; }

        /// <summary>
        /// BattleGround Grid transform.
        /// </summary>
        /// <remarks>Use it to place the enemies when spawned them.</remarks>
        public Transform EnemiesRootTransform => this._enemiesRootObject;
        #endregion

        #region Initializers
        public override void Awake()
        {
            base.Awake();

            this.InitializeLevel();
            this.GetPlayerBaseObject();
            this.GetAllEnemySpawnersInScene();
        }

        public override void OnDestroy() => base.OnDestroy();
        #endregion

        #region Methods & Functions
        private void InitializeLevel()
        {
            EnvironmentManagerController.Instance.ResetPlayer();
            EnvironmentManagerController.Instance.DisableFloorScrollMaterialEffect();

            this._editorBattlegroundGrid.enabled = false;
        }

        private void GetPlayerBaseObject() => this.PlayerBase = GameObject.FindObjectOfType<PlayerBaseController>();

        private void GetAllEnemySpawnersInScene() => this.EnemySpawnLocations = GameObject.FindObjectsOfType<EnemySpawnerController>();
        #endregion
    }
}
