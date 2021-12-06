using System.Collections;
using UnityEngine;

namespace VisualStudioEX3.Artemis.Assets.Explosion.Controllers
{
    [DisallowMultipleComponent]
    public class ExplosionController : MonoBehaviour
    {
        #region Internal vars
        private float _currentScale = 0f;
        private Material _material;
        private WaitForSeconds _durationYield;
        #endregion

        #region Inspector fields
        [SerializeField]
        private float _growSpeed = 1f;
        [SerializeField]
        private float _maxScale = 3f;
        [SerializeField]
        private float _duration = 2.5f;
        #endregion

        #region Initializers & Terminators
        private IEnumerator Start()
        {
            this.GetMaterial();
            this.GetWaitYield();

            yield return this._durationYield;

            this.Disable();
        }

        private void OnDestroy() => this.StopAllCoroutines();
        #endregion

        #region Update logic
        private void Update()
        {
            this.UpdateScaleValue();
            this.UpdateObjectScale();
            this.UpdateAlphaValue();
        }
        #endregion

        #region Methods & Functions
        private WaitForSeconds GetWaitYield() => new WaitForSeconds(Mathf.Max(this._duration, 0f));

        private float CalculateGrowSpeed() => this._growSpeed * Time.deltaTime;

        private void GetMaterial() => this._material = this.GetComponent<Renderer>().material;

        private void UpdateObjectScale() => this.transform.localScale = Vector3.one * this._currentScale;

        private void UpdateScaleValue() => this._currentScale = Mathf.Lerp(this._currentScale, this._maxScale, this.CalculateGrowSpeed());

        private void UpdateAlphaValue() => this._material.color = Color.Lerp(this._material.color, Color.clear, this.CalculateGrowSpeed());

        private void Disable() => this.gameObject.SetActive(false);
        #endregion
    }
}
