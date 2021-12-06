using System.Collections;
using UnityEngine;
using VisualStudioEX3.Artemis.Assets.EconomySystem.Controllers;
using VisualStudioEX3.Artemis.Assets.LevelManagement;
using VisualStudioEX3.Artemis.Framework.Core.Components;
using VisualStudioEX3.Artemis.Framework.InputManager.Components;

namespace VisualStudioEX3.Artemis.Assets.Scenes.Controllers
{
    public class GameManagerController : MonoBehaviour
    {
        #region Initializer
        private IEnumerator Start()
        {
            yield return this.WaitForManagerInitialization<InputManager>();
            yield return this.WaitForManagerInitialization<SceneManager>();
            yield return this.WaitForManagerInitialization<EconomyManager>();

            SceneManager.Instance.LoadStartScreen();
        }
        #endregion

        #region Methods & Functions
        private WaitUntil WaitForManagerInitialization<T>() where T : MonoBehaviourSingleton<T> => new(() => MonoBehaviourSingleton<T>.IsInitialized);
        #endregion
    }
}
