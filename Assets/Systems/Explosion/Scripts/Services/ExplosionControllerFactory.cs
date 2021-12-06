using UnityEngine;
using VisualStudioEX3.Artemis.Assets.Explosion.Controllers;
using VisualStudioEX3.Artemis.Framework.Core.Components;
using VisualStudioEX3.Artemis.Framework.Core.Contracts.Interfaces;
using VisualStudioEX3.Artemis.Framework.Core.ServiceProvider;

namespace VisualStudioEX3.Artemis.Assets.Entities.Explosion.Scripts.Services
{
    public class ExplosionControllerFactory : MonoBehaviourSingleton<ExplosionControllerFactory>
    {
        #region Internal vars
        private IUnityObjectPoolService<ExplosionController> _explosionPool;
        #endregion

        #region Inspector fields
        [SerializeField]
        private ExplosionController _prefab;
        [SerializeField]
        private int _maxExplosions = 20;
        #endregion

        #region Initializers & Terminators
        public override void Awake()
        {
            base.Awake();
            this.CreateExplosionPool();
        }

        public override void OnDestroy()
        {
            this._explosionPool.Dispose();
            base.OnDestroy();
        }
        #endregion

        #region Methods & Functions
        private void CreateExplosionPool()
        {
            this._explosionPool = (IUnityObjectPoolService<ExplosionController>)CoreServiceFactory.Factory.GetService(typeof(IUnityObjectPoolService<>), typeof(ExplosionController));
            this._explosionPool.Initialize(
                prefab: this._prefab,
                capacity: this._maxExplosions,
                availableInstancePredicate: (explosion) => !explosion.gameObject.activeInHierarchy,
                releaseInstancePredicate: (explosion) => explosion.gameObject.SetActive(false));
            this._explosionPool.ReleaseAllInstances();
        }

        public bool TryGetInstance(out ExplosionController instance)
        {
            bool ret = this._explosionPool.TryGetNext(out instance);

            if (!ret)
            {
                print("No explosion available.");
            }

            return ret;
        }
        #endregion
    }
}
