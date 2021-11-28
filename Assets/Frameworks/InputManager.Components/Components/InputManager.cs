using System;
using System.Linq;
using UnityEngine;
using VisualStudioEX3.Artemis.Framework.Core.Components;
using VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Constants;
using VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Exceptions;
using VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Interfaces;
using VisualStudioEX3.Artemis.Framework.InputManager.Contracts.Models.Assets;
using VisualStudioEX3.Artemis.Framework.InputManager.ServiceProvider;

namespace VisualStudioEX3.Artemis.Framework.InputManager.Components
{
    /// <summary>
    /// Input Manager component.
    /// </summary>
    /// <remarks>This component manages all game input states. It's accessible as singleton instance from others scripts.</remarks>
    [AddComponentMenu(ComponentMenuPaths.INPUT_MANAGER_COMPONENT_MENU_NAMESPACE), DisallowMultipleComponent]
    public class InputManager : MonoBehaviourSingleton<InputManager>
    {
        #region Constants
        private static readonly string ARGUMENT_NULL_EXCEPTION_MESSAGE = $"{nameof(InputManager)}::{{0}}: The name can not be null or empty.";
        #endregion

        #region Internal vars
        private IInputAxisProcessor _inputAxisProcessor;
        private IInputActionProcessor _inputActionProcessor;
        #endregion

        #region Serializable fields
        [SerializeField]
        private InputMapAsset[] _inputMaps;
        #endregion

        #region Initializers & Finalizers
        public override void Awake()
        {
            base.Awake();

            InputManagerServiceFactory serviceFactory = InputManagerServiceFactory.Factory;

            this._inputAxisProcessor = serviceFactory.GetService<IInputAxisProcessor>();
            this._inputActionProcessor = serviceFactory.GetService<IInputActionProcessor>();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }
        #endregion

        #region Update logic
        private void Update()
        {
            foreach (InputMapAsset asset in this._inputMaps)
                if (asset.Enabled)
                    this.UpdateInputMapAsset(asset);
        }

        private void UpdateInputMapAsset(InputMapAsset asset)
        {
            this._inputAxisProcessor.Update(asset.Axes);
            this._inputActionProcessor.Update(asset.Actions);
        }
        #endregion

        #region Methods & Functions
        public override bool IsPersistentBetweenScenes() => true;

        /// <summary>
        /// Gets the requested <see cref="InputMapAsset"/> instance.
        /// </summary>
        /// <param name="name"><see cref="InputMapAsset"/> name. Is the asset name in project.</param>
        /// <returns>Returns the <see cref="InputMapAsset"/> instance.</returns>
        /// <exception cref="InputMapAssetNotFoundException">If the name not matches with any element in the list then raises an exception.</exception>
        /// <exception cref="ArgumentNullException">If the name is empty or <see langword="null"/> string.</exception>
        public InputMapAsset GetInputMap(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw this.FormatArgumentNullException(nameof(GetInputMap));

            try
            {
                return this._inputMaps.Where(e => e.name == name).First();
            }
            catch (InvalidOperationException)
            {
                throw new InputMapAssetNotFoundException(name);
            }
        }

        private ArgumentNullException FormatArgumentNullException(string name)
        {
            return new ArgumentNullException(string.Format(InputManager.ARGUMENT_NULL_EXCEPTION_MESSAGE, name));
        }
        #endregion
    }
}
