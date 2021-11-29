using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VisualStudioEX3.Artemis.Framework.Core.Contracts.Interfaces;

namespace VisualStudioEX3.Artemis.Framework.Core.Services
{
    public class UnityObjectPoolService<T> : IUnityObjectPoolService<T> where T : UnityEngine.Object
    {
        #region Internal vars
        private bool _disposedValue;
        private List<T> _instances;
        #endregion

        #region Properties
        public bool IsInitialized => this._instances is not null;
        public IReadOnlyList<T> Instances => this._instances;
        public int Count => this._instances.Count;
        public int AvailableCount => this._instances.Count(this.AvailableInstancePredicate);
        public T Prefab { get; private set; }
        public Func<T, bool> AvailableInstancePredicate { get; private set; }
        public Action<T> ReleaseInstancePredicate { get; private set; }
        #endregion

        #region Events
        public event Action OnNotAvailableInstance;
        #endregion

        #region Destructor
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposedValue)
            {
                if (disposing)
                    this.DestroyAllInstances();

                this._disposedValue = true;
            }
        }

        public void Dispose()
        {
            this.Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region Methods & Functions
        public void Initialize(T prefab, int capacity, Func<T, bool> availableInstancePredicate, Action<T> releaseInstancePredicate)
        {
            this._instances = new List<T>(capacity);

            this.Prefab = prefab;
            this.AvailableInstancePredicate = availableInstancePredicate;
            this.ReleaseInstancePredicate = releaseInstancePredicate;

            this.CreateAllInstances();
        }

        private void CreateAllInstances()
        {
            for (int i = 0; i < this.Count; i++)
                this._instances.Add(this.CreateInstance());
        }

        private T CreateInstance() => GameObject.Instantiate<T>(this.Prefab);

        private void DestroyAllInstances()
        {
            for (int i = 0; i < this.Count; i++)
                this.DestroyInstance(i);
        }

        private void DestroyInstance(int index) => GameObject.DestroyImmediate(this._instances[index]);

        public bool TryGetNext(out T instance)
        {
            try
            {
                instance = this._instances.First(this.AvailableInstancePredicate);

                return true;
            }
            catch
            {
                this.OnNotAvailableInstance?.Invoke();
                instance = null;

                return false;
            }
        }

        public void ReleaseAllInstances()
        {
            foreach (T instance in this._instances)
                this.ReleaseInstancePredicate(instance);
        }
        #endregion
    }
}
