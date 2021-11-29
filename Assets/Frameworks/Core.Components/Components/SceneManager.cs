using UnityEngine;
using VisualStudioEX3.Artemis.Framework.Core.Contracts.Constants;
using VisualStudioEX3.Artemis.Framework.Core.Contracts.Models;

namespace VisualStudioEX3.Artemis.Framework.Core.Components
{
    [AddComponentMenu(ComponentMenuPaths.SCENE_MANAGER_COMPONENT_MENU_NAMESPACE), DisallowMultipleComponent]
    public class SceneManager : MonoBehaviourSingleton<SceneManager>
    {
        #region Inspector fields
        [SerializeField]
        private SceneAsset[] _scenes;
        #endregion

        #region Initializers & Terminators
        public override void Awake() => base.Awake();

        public override void OnDestroy() => base.OnDestroy();
        #endregion

        #region Methods & Functions
        public override bool IsPersistentBetweenScenes() => true;
        #endregion
    }
}
