using UnityEngine;
using VisualStudioEX3.Artemis.Assets.Entities.LevelGenerator.Constants;

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
        public Texture2D _walls;
        public Texture2D _enemySpawners;
        public Texture2D _turretPlacements;
        public Texture2D _playerBase;
        #endregion
    }
}
