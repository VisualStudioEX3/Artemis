using System;
using System.Collections.Generic;
using UnityEngine;
using VisualStudioEX3.Artemis.Assets.EnemySystem.Controllers;
using VisualStudioEX3.Artemis.Assets.LevelGenerator.Controller;
using VisualStudioEX3.Artemis.Assets.LevelGenerator.Services;
using VisualStudioEX3.Artemis.Assets.Player.Controllers;
using VisualStudioEX3.Artemis.Framework.Core.Contracts.Attributes;
using VisualStudioEX3.Artemis.Framework.Core.Contracts.Enums;
using VisualStudioEX3.Artemis.Turret.Placement;

namespace VisualStudioEX3.Artemis.Assets.LevelGenerator.Controllers
{
    [DisallowMultipleComponent]
    public class LevelGeneratorController : MonoBehaviour
    {
        #region Constants
        private const float GRID_SIZE = 20f;
        private const float LEFT_GRID = -10f;
        private const float TOP_GRID = -9f;

        private static readonly string FINISH_BITMAP_PROCESS_LOG_MENSSAGE = $"{nameof(LevelGeneratorController)}::{{0}}: Finished to process the \"{{1}}\" bitmap.";
        #endregion

        #region Internal vars
        private bool _isPlayerBaseInstantiate = false;
        #endregion

        #region Inspector fields
        [SerializeField, TexturePreview]
        private Texture2D _levelBitmapTemplate;

        [Header("Bitmap pixel color element assignation"), SerializeField, ColorUsage(showAlpha: false, hdr: false)]
        private Color _wallColor = Color.black;
        [SerializeField, ColorUsage(showAlpha: false, hdr: false)]
        private Color _enemySpawnPointColor = Color.red;
        [SerializeField, ColorUsage(showAlpha: false, hdr: false)]
        private Color _turretPlacementColor = Color.green;
        [SerializeField, ColorUsage(showAlpha: false, hdr: false)]
        private Color _playerBaseLocationColor = Color.blue;

        [Header("Prefabs"), SerializeField]
        private WallTileController _wallPrefab;
        [SerializeField]
        private EnemySpawnerController _enemySpawnerPrefab;
        [SerializeField]
        private TurretPlacementController _turretPlacementPrefab;
        [SerializeField]
        private PlayerBaseController _playerBasePrefab;

        [Header("Root objects to place the elements"), SerializeField]
        private Transform _wallRootObject;
        [SerializeField]
        private Transform _enemySpawnersRootObject;
        [SerializeField]
        private Transform _turretPlacementsRootObject;
        [SerializeField]
        private Transform _playerBaseRootObject;

        [SerializeField, Space, Button(customLabel: "Generate level", size: EditorGUIButtonSize.Large, disableOn: EditorGUIButtonDisableEvents.PlayMode)]
        public string _generateButtonName = nameof(GenerateLevel);
        [SerializeField, Button(customLabel: "Clear level", size: EditorGUIButtonSize.Mini, disableOn: EditorGUIButtonDisableEvents.PlayMode)]
        public string _clearButtonName = nameof(ClearLevel);
        [SerializeField]
        private bool _clearWalls = true;
        [SerializeField]
        private bool _clearEnemySpawners = true;
        [SerializeField]
        private bool _clearTurretPlacements = true;
        [SerializeField]
        private bool _clearPlayerBase = true;
        #endregion

        #region Methods & Functions
        private void GenerateLevel()
        {
            this.ClearLevel();
            this.ProcessBitmaps();
        }

        private void ClearLevel() => this.DestroyInstances();

        private void ProcessBitmaps()
        {
            this.ProcessWallBitmap();
            this.ProcessEnemySpawnerBitmap();
            this.ProcessTurretPlacementBitmap();
            this.ProcessPlayerBaseBitmap();
        }

        private void DestroyInstances()
        {
            if (this._clearWalls)
                this.DestroyAll<WallTileController>(this._wallRootObject);

            if (this._clearEnemySpawners)
                this.DestroyAll<EnemySpawnerController>(this._enemySpawnersRootObject);

            if (this._clearTurretPlacements)
                this.DestroyAll<TurretPlacementController>(this._turretPlacementsRootObject);

            if (this._clearPlayerBase)
            {
                this.DestroyAll<PlayerBaseController>(this._playerBaseRootObject);
                this._isPlayerBaseInstantiate = false;
            }
        }

        private IEnumerable<T> DetachAndGetInstances<T>(Transform parent) where T : MonoBehaviour
        {
            parent.DetachChildren();    // The new Prefab workflow (since last time I use Unity) not allow to destroy objects from prefabs.
                                        // I detached first from his Transform parent to then located by their component to destroy them.

            return GameObject.FindObjectsOfType<T>();
        }

        private void DestroyAll<T>(Transform parent) where T : MonoBehaviour
        {
            foreach (T item in this.DetachAndGetInstances<T>(parent))
                GameObject.DestroyImmediate(item.gameObject);
        }

        private Vector3 ToGridCoordinates(Vector2 bitmapPosition) => new(x: LEFT_GRID + bitmapPosition.x, y: 0f, z: TOP_GRID + bitmapPosition.y);

        private string ProcessFinishBimtapProcessLogMessage(string source, string bitmap) => string.Format(FINISH_BITMAP_PROCESS_LOG_MENSSAGE, source, bitmap);

        private void LogFinishBitmapProcess(string source, string bitmap) => print(this.ProcessFinishBimtapProcessLogMessage(source, bitmap));

        private void InstantiatePrefab(Transform target, GameObject prefab, Vector3 gridPosition, bool isStatic)
        {
            var instance = GameObject.Instantiate(prefab, target);

            instance.transform.localPosition = gridPosition;
            instance.transform.localRotation = Quaternion.identity;
            instance.isStatic = isStatic;
        }

        private void ProcessBitmap(Color colorMask, Action<Vector2> onPixelMatch, string caller)
        {
            BitmapLevelProcessor bitmapLevelProcessor = new(this._levelBitmapTemplate, colorMask, GRID_SIZE);

            bitmapLevelProcessor.OnElementFound += onPixelMatch;
            bitmapLevelProcessor.Process();

            this.LogFinishBitmapProcess(caller, this._levelBitmapTemplate.name);

            bitmapLevelProcessor.OnElementFound -= onPixelMatch;
        }

        private void ProcessWallBitmap() => this.ProcessBitmap(
            colorMask: this._wallColor,
            onPixelMatch: this.OnWallInstantiate,
            caller: nameof(ProcessWallBitmap));

        private void ProcessEnemySpawnerBitmap() => this.ProcessBitmap(
            colorMask: this._enemySpawnPointColor,
            onPixelMatch: this.OnEnemySpawnerInstantiate,
            caller: nameof(ProcessEnemySpawnerBitmap));

        private void ProcessTurretPlacementBitmap() => this.ProcessBitmap(
            colorMask: this._turretPlacementColor,
            onPixelMatch: this.OnTurretPlacementInstantiate,
            caller: nameof(ProcessTurretPlacementBitmap));

        private void ProcessPlayerBaseBitmap() => this.ProcessBitmap(
            colorMask: this._playerBaseLocationColor,
            onPixelMatch: this.OnPlayerBaseInstantiate,
            caller: nameof(ProcessPlayerBaseBitmap));
        #endregion

        #region Event listeners
        private void OnWallInstantiate(Vector2 bitmapPosition)
        {
            Vector3 gridPosition = this.ToGridCoordinates(bitmapPosition);
            this.InstantiatePrefab(this._wallRootObject, this._wallPrefab.gameObject, gridPosition, isStatic: true);
        }

        private void OnEnemySpawnerInstantiate(Vector2 bitmapPosition)
        {
            Vector3 gridPosition = this.ToGridCoordinates(bitmapPosition);
            this.InstantiatePrefab(this._enemySpawnersRootObject, this._enemySpawnerPrefab.gameObject, gridPosition, isStatic: false);
        }

        private void OnTurretPlacementInstantiate(Vector2 bitmapPosition)
        {
            Vector3 gridPosition = this.ToGridCoordinates(bitmapPosition);
            this.InstantiatePrefab(this._turretPlacementsRootObject, this._turretPlacementPrefab.gameObject, gridPosition, isStatic: false);
        }

        private void OnPlayerBaseInstantiate(Vector2 bitmapPosition)
        {
            if (!this._isPlayerBaseInstantiate)
            {
                Vector3 gridPosition = this.ToGridCoordinates(bitmapPosition);
                this.InstantiatePrefab(this._playerBaseRootObject, this._playerBasePrefab.gameObject, gridPosition, isStatic: false);
                this._isPlayerBaseInstantiate = true;
            }
        }
        #endregion
    }
}
