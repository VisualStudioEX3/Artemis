using System;
using UnityEngine;
using VisualStudioEX3.Artemis.Assets.TurretSystem.Controllers;
using VisualStudioEX3.Artemis.Framework.Core.Components;

namespace VisualStudioEX3.Artemis.Assets.TurretSystem.Services
{
    [DisallowMultipleComponent]
    public class TurretControllerFactory : MonoBehaviourSingleton<TurretControllerFactory>
    {
        #region Constants
        private const string SOURCE_EXCEPTION_HEADER = nameof(TurretControllerFactory) + "::{0}: ";
        private const string EMPTY_FACTORY_EXCEPTION_MESSAGE = SOURCE_EXCEPTION_HEADER + "The factory prefab list is empty!";
        private const string INDEX_OUT_OF_RANGE_EXCEPTION_MESSAGE = SOURCE_EXCEPTION_HEADER + "The index type requested is out of range (index: {1}).";
        #endregion

        #region Inspector fields
        [Header("Remember to use the index in the array as type to select the right turret prefab."), SerializeField]
        private TurretController[] _prefabs;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the number of prefabs.
        /// </summary>
        public int NumberOfAvailableTypes => this._prefabs.Length;
        #endregion

        #region Methods & Functions
        /// <summary>
        /// Gets a reference to the requested <see cref="TurretController"/> prefab.
        /// </summary>
        /// <param name="indexType">Index in the prefab array.</param>
        /// <returns>Returns a reference to the requestes <see cref="TurretController"/> prefab.</returns>
        public TurretController GetInstance(int indexType)
        {
            if (this.IsFactoryEmpty())
                throw this.FormatInvalidOperationException();

            if (!this.IsIndexClamped(indexType))
                throw this.FormatIndexOutOfRangeException(indexType);

            return this._prefabs[indexType];
        }

        private bool IsIndexClamped(int index) => index >= 0 && index < this._prefabs.Length;

        private bool IsFactoryEmpty() => this._prefabs.Length == 0;

        private InvalidOperationException FormatInvalidOperationException() => new(string.Format(EMPTY_FACTORY_EXCEPTION_MESSAGE, nameof(FormatInvalidOperationException)));

        private IndexOutOfRangeException FormatIndexOutOfRangeException(int index)
        {
            return new(string.Format(INDEX_OUT_OF_RANGE_EXCEPTION_MESSAGE, nameof(IsIndexClamped), index));
        }
        #endregion
    }
}
