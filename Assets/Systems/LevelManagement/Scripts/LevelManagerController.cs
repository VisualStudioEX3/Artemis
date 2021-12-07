using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VisualStudioEX3.Artemis.Assets.Common.Controllers;
using VisualStudioEX3.Artemis.Assets.Common.Controllers.UI;
using VisualStudioEX3.Artemis.Assets.EnemySystem.Controllers;
using VisualStudioEX3.Artemis.Assets.GameOver.Controllers;
using VisualStudioEX3.Artemis.Assets.LevelManagement.Constants;
using VisualStudioEX3.Artemis.Assets.Player.Controllers;
using VisualStudioEX3.Artemis.Assets.Scenes.Controllers;
using VisualStudioEX3.Artemis.Assets.StageClear.Controllers;
using VisualStudioEX3.Artemis.Framework.Core.Components;

namespace VisualStudioEX3.Artemis.Assets.LevelManagement
{
    [AddComponentMenu(ComponentMenuPaths.LEVEL_MANAGER_CONTROLLER_COMPONENT_MENU_NAMESPACE), DisallowMultipleComponent]
    public class LevelManagerController : MonoBehaviourSingleton<LevelManagerController>
    {
        #region Constants
        private const float DEFAULT_WAIT_UNTIL_SHOW_GAME_OVER_SCREEN = 3f;
        #endregion

        #region Inspector fields
        [SerializeField]
        private Renderer _editorBattlegroundGrid;
        [SerializeField]
        private Transform _enemiesRootObject;
        [SerializeField, Min(0)]
        private float _waitUntilShowGameOverScreen = DEFAULT_WAIT_UNTIL_SHOW_GAME_OVER_SCREEN;
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
            this.ShowInGameUI();
            this.HideDebugFloorGrid();
        }

        private IEnumerator Start()
        {
            yield return this.GetWaitUntilWaveManagerIsInitializedYield();
            this.InitializeLevel();
        }

        public override void OnDestroy()
        {
            this.HideInGameUI();
            base.OnDestroy();
        }
        #endregion

        #region Methods & Functions
        private void InitializeLevel()
        {
            this.ResetEnvironmentScene();
            this.GetPlayerBaseObject();
            this.GetAllEnemySpawnersInScene();
            this.SubscribeEvents();
        }

        private void ResetEnvironmentScene()
        {
            EnvironmentManagerController.Instance.ResetPlayer();
            EnvironmentManagerController.Instance.DisableFloorScrollMaterialEffect();
        }

        private WaitUntil GetWaitUntilWaveManagerIsInitializedYield() => new(() => WaveManager.IsInitialized);

        private void SubscribeEvents()
        {
            this.SubscribeOnLevelCompletedEvent();
            this.SubscribeOnPlayerBaseIsDestroyed();
        }

        private void SubscribeOnLevelCompletedEvent() => WaveManager.Instance.OnAllWavesCompleted += this.OnLevelCompleted;

        private void SubscribeOnPlayerBaseIsDestroyed()
        {
            HealthController healthController = this.PlayerBase.GetComponent<HealthController>();
            healthController.OnDeath += this.OnPlayerBaseIsDestroyed;
        }

        private void ShowInGameUI() => InGameUIManager.Instance.Show();

        private void HideInGameUI() => InGameUIManager.Instance.Hide();

        private void HideDebugFloorGrid() => this._editorBattlegroundGrid.enabled = false;

        private void GetPlayerBaseObject() => this.PlayerBase = GameObject.FindObjectOfType<PlayerBaseController>();

        private void GetAllEnemySpawnersInScene() => this.EnemySpawnLocations = GameObject.FindObjectsOfType<EnemySpawnerController>();

        /// <summary>
        /// Gets all active <see cref="EnemyController"/> instances in scene.
        /// </summary>
        /// <returns>Returns an enumeration with all active <see cref="EnemyController"/> instances in scene.</returns>
        public IEnumerable<EnemyController> GetAllActiveEnemies() => WaveManager.Instance.GetAllActiveEnemies();

        /// <summary>
        /// Gets randomly any of the <see cref="EnemySpawnerController"/> instances.
        /// </summary>
        /// <returns>Returns an <see cref="EnemySpawnerController"/> instance.</returns>
        public EnemySpawnerController GetRandomEnemySpawnLocation() => WaveManager.Instance.GetRandomEnemySpawnLocation();

        private void ShowsStageClearScreen() => StageClearController.Instance.Show();

        private void ShowGameOverScreen() => GameOverController.Instance.Show();

        private void StartGameOverScreen() => this.StartCoroutine(this.GameOverCoroutine());
        #endregion

        #region Event listeners
        private void OnLevelCompleted() => this.ShowsStageClearScreen();

        private void OnPlayerBaseIsDestroyed() => this.StartGameOverScreen();
        #endregion

        #region Coroutines
        private IEnumerator GameOverCoroutine()
        {
            yield return new WaitForSeconds(this._waitUntilShowGameOverScreen);
            this.ShowGameOverScreen();
        }
        #endregion
    }
}
