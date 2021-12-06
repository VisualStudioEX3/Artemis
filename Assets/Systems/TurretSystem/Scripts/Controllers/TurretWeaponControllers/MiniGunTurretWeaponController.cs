using UnityEngine;

namespace VisualStudioEX3.Artemis.Assets.TurretSystem.Controllers
{
    public class MiniGunTurretWeaponController : TurretWeaponController
    {
        #region Internal vars
        private TurretController _turretController;
        private float _currentRotationSpeed;
        private float _targetRotationSpeed;
        #endregion

        #region Inspector fields
        [Header("MiniGun Behaviour"), SerializeField]
        private Transform _rotator;
        [SerializeField]
        private float _idleRotationSpeed;
        [SerializeField]
        private float _shootingRotationSpeed;
        [Space, SerializeField]
        private float _lerpBetweenStatesSpeed;
        #endregion

        #region Initializers
        public override void Awake()
        {
            base.Awake();
            this.SubscribeShootEvents();
            this.InitializeSettings();
        }

        public override void OnDestroy()
        {
            this.UnsubscribeShootEvents();
            base.OnDestroy();
        }
        #endregion

        #region Update logic
        private void Update()
        {
            this.UpdateRotationSpeedValue();
            this.RotateCannon();
        } 
        #endregion

        #region Methods & Functions
        private void InitializeSettings() => this.OnStopToShoot();

        private void SubscribeShootEvents()
        {
            _turretController = this.GetComponent<TurretController>();
            _turretController.OnStartToShoot += this.OnStartToShoot;
            _turretController.OnStopToShoot += this.OnStopToShoot;
        }

        private void UnsubscribeShootEvents()
        {
            _turretController.OnStartToShoot -= this.OnStartToShoot;
            _turretController.OnStopToShoot -= this.OnStopToShoot;
        }

        private void UpdateRotationSpeedValue() => this._currentRotationSpeed = Mathf.Lerp(this._currentRotationSpeed, this._targetRotationSpeed, this.CalculateLerpSpeed());

        private float CalculateLerpSpeed() => this._lerpBetweenStatesSpeed * Time.deltaTime;

        private float CalculateRotationSpeed() => this._currentRotationSpeed * Time.deltaTime;

        private void RotateCannon() => this._rotator.Rotate(Vector3.forward * this.CalculateRotationSpeed());
        #endregion

        #region Event listeners
        private void OnStartToShoot() => this._targetRotationSpeed = this._shootingRotationSpeed;

        private void OnStopToShoot() => this._targetRotationSpeed = this._idleRotationSpeed; 
        #endregion
    }
}
