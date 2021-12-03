using System.Collections.Generic;
using UnityEngine;
using VisualStudioEX3.Artemis.Assets.EnemySystem.Controllers;
using VisualStudioEX3.Artemis.Assets.LevelManagement.Constants;
using VisualStudioEX3.Artemis.Assets.Player.Controllers;
using VisualStudioEX3.Artemis.Assets.Scenes.Controllers;

namespace VisualStudioEX3.Artemis.Assets.LevelManagement
{
    [AddComponentMenu(ComponentMenuPaths.LEVEL_MANAGER_CONTROLLER_COMPONENT_MENU_NAMESPACE), DisallowMultipleComponent]
    public class LevelManagerController : MonoBehaviour
    {
        #region Properties
        public PlayerBaseController PlayerBase { get; private set; }
        public IEnumerable<EnemySpawnerController> EnemySpawnLocations { get; private set; }
        #endregion

        #region Inspector fields
        [SerializeField]
        private Renderer _editorBattlegroundGrid;
        #endregion

        #region Initializers
        private void Awake()
        {
            this.InitializeLevel();
            this.GetPlayerBaseObject();
            this.GetAllEnemySpawnersInScene();
        }
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
