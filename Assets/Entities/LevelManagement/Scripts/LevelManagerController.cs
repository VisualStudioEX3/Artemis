using UnityEngine;
using VisualStudioEX3.Artemis.Assets.LevelManagement.Constants;
using VisualStudioEX3.Artemis.Assets.Scenes.Controllers;

namespace VisualStudioEX3.Artemis.Assets.LevelManagement
{
    [AddComponentMenu(ComponentMenuPaths.SCENE_MANAGER_COMPONENT_MENU_NAMESPACE), DisallowMultipleComponent]
    public class LevelManagerController : MonoBehaviour
    {
        #region Initializers
        private void Awake() => this.InitializeLevel(); 
        #endregion

        #region Methods & Functions
        private void InitializeLevel()
        {
            EnvironmentManagerController.Instance.ResetPlayer();
            EnvironmentManagerController.Instance.DisableFloorScrollMaterialEffect();
        } 
        #endregion
    }
}
