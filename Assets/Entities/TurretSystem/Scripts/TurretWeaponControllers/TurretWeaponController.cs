using System;
using System.Collections;
using UnityEngine;
using VisualStudioEX3.Artemis.Assets.Bullets.Controllers;
using VisualStudioEX3.Artemis.Framework.Core.Contracts.Interfaces;
using VisualStudioEX3.Artemis.Framework.Core.ServiceProvider;

namespace VisualStudioEX3.Artemis.Assets.TurretSystem.Controllers
{
    public class TurretWeaponController : MonoBehaviour
    {
        #region Internal vars
        private IUnityObjectPoolService<BulletController> _bulletPool;
        private Coroutine _holdTriggerCoroutineInstance;
        #endregion

        #region Inspector fields
        [SerializeField]
        private BulletController _bulletPrefab;
        [SerializeField]
        private int _maxAmmoCapacity;
        [SerializeField]
        private float _fireRatio;
        [SerializeField]
        private Transform _sourceShoot;
        #endregion

        #region Events
        /// <summary>
        /// Notifies when the controller shoot a bullet.
        /// </summary>
        public event Action OnShoot;

        /// <summary>
        /// Notifies when the controller trying to shoot but don't have available bullets.
        /// </summary>
        public event Action OnNotMoreBullets;
        #endregion

        #region Initializers & Terminators
        public virtual void Awake() => this.CreateBulletPoolService();

        public virtual void OnDestroy() => this.DestroyBulletPoolService();
        #endregion

        #region Methods & Functions
        private void CreateBulletPoolService()
        {
            this._bulletPool = (IUnityObjectPoolService<BulletController>)CoreServiceFactory.Factory.GetService(typeof(IUnityObjectPoolService<>), typeof(BulletController));
            this._bulletPool.Initialize(
                prefab: this._bulletPrefab,
                capacity: this._maxAmmoCapacity,
                availableInstancePredicate: (bullet) => !bullet.gameObject.activeInHierarchy,
                releaseInstancePredicate: (bullet) => bullet.gameObject.SetActive(false));
            this._bulletPool.ReleaseAllInstances();
        }

        private void DestroyBulletPoolService() => this._bulletPool.Dispose();

        private void Shoot()
        {
            if (this.TryGetNewBullet(out BulletController bullet))
            {
                this.InstantiateBullet(bullet);
                this.NotifyOnShoot();
            }
            else
                this.NotifyOnNotMoreBullets();
        }

        private bool TryGetNewBullet(out BulletController bullet) => this._bulletPool.TryGetNext(out bullet);

        private void InstantiateBullet(BulletController bullet)
        {
            bullet.transform.position = this._sourceShoot.position;
            bullet.transform.forward = this._sourceShoot.forward;
            bullet.gameObject.SetActive(true);
        }

        private void NotifyOnShoot() => this.OnShoot?.Invoke();

        private void NotifyOnNotMoreBullets() => this.OnNotMoreBullets?.Invoke();

        private WaitForSeconds CreateWaitRatioYield() => new(this._fireRatio);

        /// <summary>
        /// Hold the trigger to start to shoot the weapon.
        /// </summary>
        public void HoldTrigger() => this._holdTriggerCoroutineInstance ??= this.StartCoroutine(this.HoldTriggerCoroutine());

        /// <summary>
        /// Release the trigger to stop the shoot the weapon.
        /// </summary>
        public void ReleaseTrigger()
        {
            if (this._holdTriggerCoroutineInstance is not null)
            {
                this.StopCoroutine(this._holdTriggerCoroutineInstance);
                this._holdTriggerCoroutineInstance = null;
            }
        }
        #endregion

        #region Coroutines
        private IEnumerator HoldTriggerCoroutine()
        {
            WaitForSeconds waitYield = this.CreateWaitRatioYield();

            while (true)
            {
                this.Shoot();
                yield return waitYield;
            }
        } 
        #endregion
    }
}
