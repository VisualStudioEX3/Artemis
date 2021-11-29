using UnityEngine;
using VisualStudioEX3.Artemis.Framework.Core.Components;

namespace VisualStudioEX3.Artemis.SceneController
{
    public class StartSceneController : MonoBehaviour
    {
        #region Methods & Functions
        public void StartNewGame() => SceneManager.Instance.LoadNextLevel(); 
        #endregion
    }
}
