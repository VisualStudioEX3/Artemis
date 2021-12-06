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
        #endregion

        #region Inspector fields
        [SerializeField]
        private float _growSpeed = 1f;
        [SerializeField]
        private float _maxScale = 3f;
        #endregion

        #region Initializers
        private IEnumerator Start()
        {
            this.GetMaterial();

            while (!this.IsReachMaxScale())
            {
                this.UpdateScaleValue();
                this.UpdateObjectScale();
                this.UpdateAlphaValue();

                yield return null;
            }

            this.Disable();
        }
        #endregion

        #region Methods & Functions
        private float CalculateGrowSpeed() => this._growSpeed * Time.deltaTime;

        private void GetMaterial() => this._material = this.GetComponent<Renderer>().material;

        private bool IsReachMaxScale() => Mathf.Approximately(this._currentScale, this._maxScale);

        private void UpdateObjectScale() => this.transform.localScale = Vector3.one * this._currentScale;

        private void UpdateScaleValue() => this._currentScale = Mathf.Lerp(this._currentScale, this._maxScale, this.CalculateGrowSpeed());

        private void UpdateAlphaValue() => this._material.color = Color.Lerp(this._material.color, Color.clear, this.CalculateGrowSpeed());

        private void Disable() => this.gameObject.SetActive(false);
        #endregion

        private void OnTriggerEnter(Collider other)
        {
            print(other.name);
        }
    }
}
