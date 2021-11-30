using UnityEngine;
using VisualStudioEX3.Artemis.Assets.EnemySystem.Constants;

namespace VisualStudioEX3.Artemis.Assets.EnemySystem.Models
{
    [CreateAssetMenu(fileName = "New Wave Asset", menuName = AssetMenuPaths.WAVE_ASSET_MENU_NAMESPACE)]
    public class WaveAsset : ScriptableObject
    {
        #region Public fields
        public EnemyWaveData[] enemyTypes;
        #endregion
    }
}
