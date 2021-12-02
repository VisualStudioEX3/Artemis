using UnityEngine;
using VisualStudioEX3.Artemis.Assets.LevelManagement.Constants;
using VisualStudioEX3.Artemis.Assets.Scenes.Controllers;

namespace VisualStudioEX3.Artemis.Assets.LevelManagement
{
    [AddComponentMenu(ComponentMenuPaths.LEVEL_MANAGER_CONTROLLER_COMPONENT_MENU_NAMESPACE), DisallowMultipleComponent]
    public class LevelManagerController : MonoBehaviour
    {
        #region Inspector fields
        [SerializeField]
        private Renderer _editorBattlegroundGrid;
        #endregion

        #region Initializers
        private void Awake() => this.InitializeLevel();
        #endregion

        #region Methods & Functions
        private void InitializeLevel()
        {
            EnvironmentManagerController.Instance.ResetPlayer();
            EnvironmentManagerController.Instance.DisableFloorScrollMaterialEffect();

            this._editorBattlegroundGrid.enabled = false;
        }
        #endregion
    }
}
