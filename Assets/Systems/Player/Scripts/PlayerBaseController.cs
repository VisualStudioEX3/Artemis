using System.Collections;
using UnityEngine;
using VisualStudioEX3.Artemis.Assets.Common.Controllers;
using VisualStudioEX3.Artemis.Assets.Explosion.Controllers;
using VisualStudioEX3.Artemis.Assets.GameOver.Controllers;

namespace VisualStudioEX3.Artemis.Assets.Player.Controllers
{
    /// <summary>
    /// Player Base Controller.
    /// </summary>
    [DisallowMultipleComponent, RequireComponent(typeof(HealthController), typeof(Rigidbody), typeof(SphereCollider))]
    public class PlayerBaseController : MonoBehaviour
    {
        #region Constants
        private const float DEFAULT_WAIT_UNTIL_SHOW_GAME_OVER_SCREEN = 3f; 
        #endregion

        #region Internal vars
        private HealthController _healthController;
        #endregion

        #region Inspector fields
        [SerializeField]
        private GameObject _root;
        [SerializeField]
        private ExplosionController _explosion;
        [SerializeField, Min(0)]
        private float _waitUntilShowGameOverScreen = DEFAULT_WAIT_UNTIL_SHOW_GAME_OVER_SCREEN;
        #endregion

        #region Initializers
        private void Awake() => this.SetupHealthComponent();
        #endregion

        #region Methods & Functions
        private void ResolveHealthComponent() => this._healthController = this.GetComponent<HealthController>();

        private void SubscribeHealthEvents() => this._healthController.OnDeath += this.OnDead;

        private void SetupHealthComponent()
        {
            this.ResolveHealthComponent();
            this.SubscribeHealthEvents();
        }

        private void DisableObject() => this._root.SetActive(false);

        private void Explode()
        {
            this._explosion.transform.parent = null;
            this._explosion.gameObject.SetActive(true);
        }

        private void SetGameOverScreen() => this.StartCoroutine(this.GameOverCoroutine());
        #endregion

        #region Event listeners
        private void OnDead()
        {
            this.Explode();
            this.DisableObject();
            this.SetGameOverScreen();
        }
        #endregion

        #region Coroutines
        private IEnumerator GameOverCoroutine()
        {
            yield return new WaitForSeconds(this._waitUntilShowGameOverScreen);
            GameOverController.Instance.Show();
        } 
        #endregion
    }
}
