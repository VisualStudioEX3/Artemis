using UnityEngine;
using VisualStudioEX3.Artemis.SceneController;

namespace VisualStudioEX3.Artemis.Assets.LevelManager
{
    public class LevelManagerController : MonoBehaviour
    {
        #region Initializers
        private void Awake() => EnvironmentManagerController.Instance.ResetPlayer();
        #endregion
    }
}
