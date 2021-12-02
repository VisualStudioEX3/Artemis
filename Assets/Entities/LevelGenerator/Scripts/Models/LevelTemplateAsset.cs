using UnityEngine;
using VisualStudioEX3.Artemis.Assets.Entities.LevelGenerator.Constants;
using VisualStudioEX3.Artemis.Framework.Core.Contracts.Attributes;

namespace VisualStudioEX3.Artemis.Assets.LevelGenerator 
{
    /// <summary>
    /// Level Template.
    /// </summary>
    /// <remarks>Level templates are defined using 20x20 pixel bitmaps using basic colors for each element: walls, enemy spawners, turret placements and player base locations.</remarks>
    [CreateAssetMenu(fileName = "New Level Template Asset", menuName = AssetMenuPaths.LEVEL_TEMPLATE_ASSET_MENU_NAMESPACE)]
    public class LevelTemplateAsset : ScriptableObject
    {
        #region Public vars
        [TexturePreview, Tooltip("Bitmap that define the walls.")]
        public Texture2D _walls;
        
        [TexturePreview, Tooltip("Bitmap that define the enemy spawn points.")]
        public Texture2D _enemySpawners;

        [TexturePreview, Tooltip("Bitamp that define the available turret placements.")]
        public Texture2D _turretPlacements;

        [TexturePreview, Tooltip("Bitmap that define the player base location.")]
        public Texture2D _playerBase;
        #endregion
    }
}
