using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VisualStudioEX3.Artemis.Assets.LevelManagement;
using VisualStudioEX3.Artemis.Framework.Core.Components;

namespace VisualStudioEX3.Artemis.Assets.GameOver.Controllers
{
    [DisallowMultipleComponent]
    public class GameOverController : MonoBehaviourSingleton<GameOverController>
    {
        #region Initializers
        public override void Awake()
        {
            base.Awake();
            this.Hide();
        }

        public override void OnDestroy() => base.OnDestroy();
        #endregion

        #region Methods & Functions
        public override bool IsPersistentBetweenScenes() => true;

        /// <summary>
        /// Return to the main screen.
        /// </summary>
        /// <remarks>This method is called from the <see cref="UnityEvent"/> event in the <see cref="Button"/> component.</remarks>
        public void ReturnToMainScreen()
        {
            this.Hide();
            this.LoadStartScreen();
        }

        private void LoadStartScreen() => SceneManager.Instance.LoadStartScreen();

        /// <summary>
        /// Shows the game over screen.
        /// </summary>
        public void Show() => this.gameObject.SetActive(true);

        private void Hide() => this.gameObject.SetActive(false);
        #endregion
    }
}
