using UnityEngine;
using VisualStudioEX3.Artemis.Assets.LevelGenerator.Models;
using VisualStudioEX3.Artemis.Framework.Core.Contracts.Attributes;
using VisualStudioEX3.Artemis.Framework.Core.Contracts.Enums;

namespace VisualStudioEX3.Artemis.Assets.LevelGenerator.Services
{
    public class LevelGenerator : MonoBehaviour
    {
        #region Constants
        private const float GRID_SIZE = 20f;
        private const float LEFT_GRID = -10;
        private const float TOP_GRID = 10f;
        #endregion

        #region Inspector fields
        [SerializeField]
        private LevelTemplateAsset _levelTemplate;

        [Header("Bitmap pixel color element assignation"), SerializeField, ColorUsage(showAlpha: false, hdr: false)]
        private Color _wall = Color.black;
        [SerializeField, ColorUsage(showAlpha: false, hdr: false)]
        private Color _enemySpawnPoint = Color.red;
        [SerializeField, ColorUsage(showAlpha: false, hdr: false)]
        private Color _turretPlacement = Color.green;
        [SerializeField, ColorUsage(showAlpha: false, hdr: false)]
        private Color _playerBaseLocation = Color.blue;

        [Space, SerializeField]
        private Transform _gridFloor;

        [SerializeField, Space, Button(customLabel: "Generate level", size: GUIButtonSize.Large, disableOn: GUIButtonDisableEvents.PlayMode)]
        public string _generateButtonName = nameof(GenerateLevel);
        #endregion

        #region Methods & Functions
        private void GenerateLevel()
        {
            print("Button tests");
        }

        private Vector3 ToGridCoordinates(int x, int y) => new(x: LEFT_GRID + x, y: 0f, z: TOP_GRID - y);

        private void ProcessWallBitmap(Texture2D bitmap)
        {
            
        }
        #endregion
    }
}
