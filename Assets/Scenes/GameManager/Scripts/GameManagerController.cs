using System.Collections;
using UnityEngine;
using VisualStudioEX3.Artemis.Assets.LevelManagement;
using VisualStudioEX3.Artemis.Framework.InputManager.Components;

namespace VisualStudioEX3.Artemis.Assets.Scenes.Controllers
{
    public class GameManagerController : MonoBehaviour
    {
        #region Initializer
        private IEnumerator Start()
        {
            yield return new WaitUntil(() => InputManager.IsInitialized);
            yield return new WaitUntil(() => SceneManager.IsInitialized);

            SceneManager.Instance.LoadStartScreen();
        } 
        #endregion
    }
}
