using TMPro;
using UnityEngine;

namespace VisualStudioEX3.Artemis.Assets.EnemySystem.Controllers.UI
{
    [DisallowMultipleComponent]
    public class WaveStartsWarningController : MonoBehaviour
    {
        #region Constants
        private const string ANIMATOR_TRIGGER_EVENT_NAME = "Start";
        private const string WAVE_STARTS_WARNING_MESAGGE = "Wave {0} starts!";
        #endregion

        #region Internal vars
        private Animator _animator;
        private TextMeshProUGUI _TMProText;
        #endregion

        #region Initializers
        private void Awake()
        {
            this.ResolveAnimatorComponent();
            this.ResolveTMProComponent();
            this.HideText();
        }
        #endregion

        #region Methods & Functions
        private void ResolveAnimatorComponent() => this._animator = this.GetComponent<Animator>();

        private void ResolveTMProComponent() => this._TMProText = this.GetComponentInChildren<TextMeshProUGUI>();

        private void ShowText() => this._TMProText.gameObject.SetActive(true);

        private void HideText() => this._TMProText.gameObject.SetActive(false);

        private void PlayAnimation() => this._animator.SetTrigger(ANIMATOR_TRIGGER_EVENT_NAME);

        private void SetWaveNumber(int waveNumber) => this._TMProText.text = string.Format(WAVE_STARTS_WARNING_MESAGGE, waveNumber);

        /// <summary>
        /// Plays the animation.
        /// </summary>
        public void Play(int waveNumber)
        {
            this.PlayAnimation();
            this.ShowText();
            this.SetWaveNumber(waveNumber);
        } 
        #endregion
    }
}
