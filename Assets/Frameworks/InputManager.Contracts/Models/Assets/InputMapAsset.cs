using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Constants;
using VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Exceptions;

namespace VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Models.Assets
{
    /// <summary>
    /// Asset that defines user input actions and axes bindings.
    /// </summary>
    [CreateAssetMenu(fileName = "New Input Map Asset", menuName = AssetMenuPaths.INPUT_MAP_ASSET_MENU_NAMESPACE)]
    public class InputMapAsset : ScriptableObject
    {
        #region Constants
        private static readonly string ARGUMENT_NULL_EXCEPTION_MESSAGE = $"{nameof(InputMapAsset)}::{{0}}: The name can not be null or empty.";
        #endregion

        #region Inspector fields
        [SerializeField]
        private bool _enabled = true;
        [SerializeField]
        private InputAxis[] _axes;
        [SerializeField]
        private InputAction[] _actions;
        #endregion

        #region Properties
        /// <summary>
        /// Gets if the asset is enabled.
        /// </summary>
        /// <remarks>Any disabled <see cref="InputMapAsset"/> are ignored by the Input Manager.</remarks>
        public bool Enabled => this._enabled;

        /// <summary>
        /// Read only list of the defined axes.
        /// </summary>
        public IReadOnlyList<InputAxis> Axes => this._axes;

        /// <summary>
        /// Read only list of the defined actions.
        /// </summary>
        public IReadOnlyList<InputAction> Actions => this._actions;
        #endregion

        #region Methods & Functions
        /// <summary>
        /// Gets the requested <see cref="InputAxis"/>.
        /// </summary>
        /// <param name="name">Name of the axis in this map.</param>
        /// <returns>Returns the requested <see cref="InputAxis"/> item.</returns>
        /// <exception cref="InputAxisNotFoundException">If the name not matches with any element in the list then raises an exception.</exception>
        /// <exception cref="ArgumentNullException">If the name is empty or <see langword="null"/> string.</exception>
        public InputAxis GetAxis(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw this.FormatArgumentNullException(nameof(GetAxis));

            try
            {
                return this._axes.Where(e => e.name == name).First();
            }
            catch (InvalidOperationException)
            {
                throw new InputAxisNotFoundException(name, this.name);
            }
        }

        /// <summary>
        /// Gets the requested <see cref="InputAction"/>.
        /// </summary>
        /// <param name="name">Name of the axis in this map.</param>
        /// <returns>Returns the requested <see cref="InputAction"/> item.</returns>
        /// <exception cref="InputActionNotFoundException">If the name not matches with any element in the list then raises an exception.</exception>
        /// <exception cref="ArgumentNullException">If the name is empty or <see langword="null"/> string.</exception>
        public InputAction GetAction(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw this.FormatArgumentNullException(nameof(GetAction));

            try
            {
                return this._actions.Where(e => e.name == name).First();
            }
            catch (InvalidOperationException)
            {
                throw new InputActionNotFoundException(name, this.name);
            }
        }

        private ArgumentNullException FormatArgumentNullException(string name)
        {
            return new ArgumentNullException(string.Format(InputMapAsset.ARGUMENT_NULL_EXCEPTION_MESSAGE, name));
        }
        #endregion
    }
}
