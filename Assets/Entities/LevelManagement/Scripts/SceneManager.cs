using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using VisualStudioEX3.Artemis.Assets.LevelManagement.Constants;
using VisualStudioEX3.Artemis.Framework.Core.Components;
using VisualStudioEX3.Artemis.Framework.Core.Contracts.Models;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace VisualStudioEX3.Artemis.Assets.LevelManagement
{
    /// <summary>
    /// Scene Manager.
    /// </summary>
    /// <remarks>This component ease to manage the different game scenes loading and unloading operations. 
    /// It's accessible as singleton instance from others scripts.</remarks>
    [AddComponentMenu(ComponentMenuPaths.SCENE_MANAGER_COMPONENT_MENU_NAMESPACE), DisallowMultipleComponent]
    public class SceneManager : MonoBehaviourSingleton<SceneManager>
    {
        #region Internal vars
        private int _currentLevelIndex = -1;
        private SceneAsset _currentScene;
        #endregion

        #region Inspector fields
        [SerializeField]
        private SceneAsset _enviroment;
        [SerializeField]
        private SceneAsset _startScreen;
        [SerializeField]
        private SceneAsset[] _scenes;
        #endregion

        #region Events
        /// <summary>
        /// Notifies when starts to load a new scene.
        /// </summary>
        public event Action OnLoadSceneStarts;

        /// <summary>
        /// Notifies the progress of the scene load process.
        /// </summary>
        public event Action<float> OnLoadSceneProgress;

        /// <summary>
        /// Notifies when the scene is loaded.
        /// </summary>
        public event Action OnLoadSceneFinished;

        /// <summary>
        /// Notifies when starts to unload a new scene.
        /// </summary>
        public event Action OnUnloadSceneStarts;

        /// <summary>
        /// Notifies the progress of the scene unload process.
        /// </summary>
        public event Action<float> OnUnloadSceneProgress;

        /// <summary>
        /// Notifies when the scene is unloaded.
        /// </summary>
        public event Action OnUnloadSceneFinished;
        #endregion

        #region Initializers & Terminators
        public override void Awake()
        {
            base.Awake();
            this.LoadEnvironment();
        }

        public override void OnDestroy() => base.OnDestroy();
        #endregion

        #region Methods & Functions
        public override bool IsPersistentBetweenScenes() => true;

        /// <summary>
        /// Loads the environment level.
        /// </summary>
        public void LoadEnvironment()
        {
            if (this._scenes.Length == 0)
                throw new InvalidOperationException($"{nameof(SceneManager)}::{nameof(this.LoadEnvironment)}: No are scene to load!");

            this.StartCoroutine(this.LoadSceneCoroutine(this._enviroment, LoadSceneMode.Single, isFixedScene: true));
        }

        /// <summary>
        /// Loads the start screen level.
        /// </summary>
        public void LoadStartScreen()
        {
            if (this._scenes.Length == 0)
                throw new InvalidOperationException($"{nameof(SceneManager)}::{nameof(this.LoadNextLevel)}: No are scene to load!");

            this.StartCoroutine(this.LoadSceneCoroutine(this._startScreen, LoadSceneMode.Additive, isFixedScene: false));
        }

        /// <summary>
        /// Loads the next level.
        /// </summary>
        /// <remarks>Loads de the next available level, or if not has levels left, loads the start screen level.</remarks>
        public void LoadNextLevel()
        {
            if (this._scenes.Length == 0)
                throw new InvalidOperationException($"{nameof(SceneManager)}::{nameof(this.LoadNextLevel)}: No are scenes to load!");

            if (++this._currentLevelIndex > this._scenes.Length)
                this.LoadStartScreen();
            else
                this.StartCoroutine(this.LoadSceneCoroutine(this._scenes[this._currentLevelIndex], LoadSceneMode.Additive, isFixedScene: false));
        }

        private bool IsNullSceneAsset(SceneAsset scene) => string.IsNullOrEmpty(scene);
        #endregion

        #region Coroutines
        private IEnumerator LoadSceneCoroutine(SceneAsset scene, LoadSceneMode loadMode, bool isFixedScene)
        {
            if (!this.IsNullSceneAsset(this._currentScene))
                yield return this.UnloadScene(this._currentScene);

            if (!this.IsNullSceneAsset(scene))
            {
                if (!isFixedScene)
                    this._currentScene = scene;

                yield return this.LoadScene(scene, loadMode);
            }
            else
                throw new ArgumentNullException($"{nameof(SceneManager)}::{nameof(LoadSceneCoroutine)}: The scene #{this._currentLevelIndex} is a null reference!");
        }

        private Coroutine LoadScene(SceneAsset scene, LoadSceneMode loadMode)
        {
            return this.StartCoroutine(this.AsyncSceneOperationCoroutine(
                scene: scene,
                operation: (asyncOperation) => UnitySceneManager.LoadSceneAsync(scene, loadMode),
                onStarts: this.OnLoadSceneStarts,
                onProgress: this.OnLoadSceneProgress,
                onFinished: this.OnLoadSceneFinished));
        }

        private Coroutine UnloadScene(SceneAsset scene)
        {
            return this.StartCoroutine(this.AsyncSceneOperationCoroutine(
                scene: scene,
                operation: (asyncOperation) => UnitySceneManager.UnloadSceneAsync(scene),
                onStarts: this.OnUnloadSceneStarts,
                onProgress: this.OnUnloadSceneProgress,
                onFinished: this.OnUnloadSceneFinished));
        }

        private IEnumerator AsyncSceneOperationCoroutine(
            SceneAsset scene,
            Func<SceneAsset, AsyncOperation> operation,
            Action onStarts,
            Action<float> onProgress,
            Action onFinished)
        {
            onStarts?.Invoke();

            AsyncOperation asyncOperation = operation(scene);

            while (!asyncOperation.isDone)
            {
                onProgress?.Invoke(asyncOperation.progress);
                yield return null;
            }

            onFinished?.Invoke();
        } 
        #endregion
    }
}
