using System;
using System.Collections;
using UnityEngine;
using VisualStudioEX3.Artemis.Assets.EconomySystem.Controllers;
using VisualStudioEX3.Artemis.Assets.LevelManagement;
using VisualStudioEX3.Artemis.Framework.Core.Components;
using VisualStudioEX3.Artemis.Framework.InputManager.Components;

namespace VisualStudioEX3.Artemis.Assets.Scenes.Controllers
{
    /// <summary>
    /// Main manager of the game.
    /// </summary>
    /// <remarks>All starts from here.</remarks>
    [DisallowMultipleComponent]
    public class GameManagerController : MonoBehaviour
    {
        #region Events
        /// <summary>
        /// Notifies when the all managers are initialized.
        /// </summary>
        public static event Action OnGameManagerIsIntialized;
        #endregion

        #region Initializer
        private IEnumerator Start()
        {
            yield return this.WaitForManagerInitialization<InputManager>();
            yield return this.WaitForManagerInitialization<SceneManager>();
            yield return this.WaitForManagerInitialization<EconomyManager>();

            GameManagerController.OnGameManagerIsIntialized?.Invoke();

            SceneManager.Instance.LoadStartScreen();
        }
        #endregion

        #region Methods & Functions
        private WaitUntil WaitForManagerInitialization<T>() where T : MonoBehaviourSingleton<T> => new(() => MonoBehaviourSingleton<T>.IsInitialized);
        #endregion
    }
}
