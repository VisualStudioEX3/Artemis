using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VisualStudioEX3.Artemis.Assets.LevelManagement;
using VisualStudioEX3.Artemis.Framework.Core.Components;

namespace VisualStudioEX3.Artemis.Assets.StageClear.Controllers
{
    [DisallowMultipleComponent]
    public class StageClearController : MonoBehaviourSingleton<StageClearController>
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
        /// Load the next level.
        /// </summary>
        /// <remarks>This method is called from the <see cref="UnityEvent"/> event in the <see cref="Button"/> component.</remarks>
        public void GoToTheNextGridArena()
        {
            this.Hide();
            this.LoadNextLevel();
        }

        private void LoadNextLevel() => SceneManager.Instance.LoadNextLevel();

        /// <summary>
        /// Shows the stage clear screen.
        /// </summary>
        public void Show() => this.gameObject.SetActive(true);

        private void Hide() => this.gameObject.SetActive(false);
        #endregion
    }
}
