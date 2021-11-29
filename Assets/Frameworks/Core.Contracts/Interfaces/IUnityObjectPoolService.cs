using System;
using System.Collections.Generic;

namespace VisualStudioEX3.Artemis.Framework.Core.Contracts.Interfaces
{
    /// <summary>
    /// Generic object pool service for work with <see cref="UnityEngine.Object"/> instances.
    /// </summary>
    /// <typeparam name="T"><see cref="UnityEngine.Object"/> based type of the prefab used to create instances.</typeparam>
    public interface IUnityObjectPoolService<T> : IDisposable where T : UnityEngine.Object
    {
        #region Properties
        /// <summary>
        /// Is the object pool initialized?
        /// </summary>
        bool IsInitialized { get; }
        
        /// <summary>
        /// Read only list of all instances in this object pool.
        /// </summary>
        IReadOnlyList<T> Instances { get; }
        
        /// <summary>
        /// Prefab model to create the instances.
        /// </summary>
        T Prefab { get; }
        
        /// <summary>
        /// Total count of the instances in this object pool.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Count of all available instances in this object pool.
        /// </summary>
        /// <see cref="AvailableInstancePredicate"/>
        int AvailableCount { get; }

        /// <summary>
        /// Predicate to define the condition filter to get the available instances.
        /// </summary>
        /// <remarks>Remember to implement mechanims in the object to create this predicate.</remarks>
        Func<T, bool> AvailableInstancePredicate { get; }

        /// <summary>
        /// Predicate to define how to release all unavailable instances.
        /// </summary>
        /// <remarks>Remember to implement mechanims in the object to create this predicate.</remarks>
        Action<T> ReleaseInstancePredicate { get; } 
        #endregion

        #region Events
        /// <summary>
        /// Notifies when try to get an available instance but not are one available.
        /// </summary>
        event Action OnNotAvailableInstance;
        #endregion

        #region Methods & Functions
        /// <summary>
        /// Initialize this object pool.
        /// </summary>
        /// <param name="prefab">Prefab model to create the instances. Must be an <see cref="UnityEngine.Object"/> based type.</param>
        /// <param name="capacity">Ammount of instances to create.</param>
        /// <param name="availableInstancePredicate">Predicate to define the condition filter to get the available instances.</param>
        /// <param name="releaseInstancePredicate">Predicate to define how to release all unavailable instances.</param>
        void Initialize(T prefab, int capacity, Func<T, bool> availableInstancePredicate, Action<T> releaseInstancePredicate);

        /// <summary>
        /// Try to get the next available instance.
        /// </summary>
        /// <param name="instance">The available instance.</param>
        /// <returns>Returns <see langword="true"/> if it found an available instance. In case that not have any available instance, returns <see langword="false"/> and raises the <see cref="OnNotAvailableInstance"/> event.</returns>
        bool TryGetNext(out T instance);

        /// <summary>
        /// Release all instance.
        /// </summary>
        /// <see cref="ReleaseInstancePredicate"/>
        void ReleaseAllInstances(); 
        #endregion
    }
}