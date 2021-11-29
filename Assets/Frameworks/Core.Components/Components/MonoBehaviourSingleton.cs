using System;
using UnityEngine;

namespace VisualStudioEX3.Artemis.Framework.Core.Components
{
    /// <summary>
    /// Base class to implement <see cref="MonoBehaviour"/> derived classes as singleton instances.
    /// </summary>
    /// <typeparam name="T">Type of the <see cref="MonoBehaviour"/> derived class.</typeparam>
    /// <remarks>Remember call base.<see cref="Awake"/> and base.<see cref="OnDestroy"/> events, when overload these events, to the right work of the singleton.</remarks>
    public abstract class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        #region Properties
        /// <summary>
        /// Gets if this singleton are initialized.
        /// </summary>
        /// <remarks>Use this property to ensure that the instance is created.</remarks>
        public bool IsInitialized => MonoBehaviourSingleton<T>.Instance;

        /// <summary>
        /// Unique instance of this <see cref="MonoBehaviour"/> derived class.
        /// </summary>
        public static T Instance { get; private set; }
        #endregion

        #region Initializers
        public virtual void Awake()
        {
            if (MonoBehaviourSingleton<T>.Instance != null)
                throw new InvalidOperationException(
                        $"{nameof(MonoBehaviourSingleton<T>)}<{typeof(T)}>: Error to initialize singleton instance. A previous instance is created on \"{this.gameObject.scene.name}\" scene on \"{this.gameObject.name}\" game object!");

            MonoBehaviourSingleton<T>.Instance = this as T;

            if (this.IsPersistentBetweenScenes())
                DontDestroyOnLoad(this.transform.root.gameObject);
        }

        public virtual void OnDestroy() => MonoBehaviourSingleton<T>.Instance = null;
        #endregion

        #region Methods & Functions
        /// <summary>
        /// Determine if this instance must be persists between scene loads.
        /// </summary>
        /// <returns>By default returns <see langword="false"/>.</returns>
        /// <remarks>Override this function to internally call <see cref="UnityEngine.Object.DontDestroyOnLoad(UnityEngine.Object)"/> function, referencing the root of this <see cref="GameObject"/>, on the initialization.</remarks>
        public virtual bool IsPersistentBetweenScenes() => false;
        #endregion
    }
}
