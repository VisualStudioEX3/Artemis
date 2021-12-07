using System;
using UnityEngine;
using VisualStudioEX3.Artemis.Framework.Core.Components;

namespace VisualStudioEX3.Artemis.Assets.TurretSystem.Controllers.UI
{
    [DisallowMultipleComponent]
    public class TurretSelectorDialogController : MonoBehaviourSingleton<TurretSelectorDialogController>
    {
        #region Constants
        private const string SOURCE_EXCEPTION = nameof(TurretSelectorDialogController) + "::{0}: ";
        private const string NULL_CALLER_ARGUMENT_EXCEPTION_MESSAGE = SOURCE_EXCEPTION + "The caller argument can not be null.";
        #endregion

        #region Inspector fields
        [SerializeField, Tooltip("Used to activate or deactivate all dialog objects without disable this component.")]
        private GameObject _baseDialogNode; 
        #endregion

        #region Properties
        /// <summary>
        /// Gets if the dialog is open.
        /// </summary>
        public bool IsOpen => this._baseDialogNode.activeInHierarchy;

        /// <summary>
        /// <see cref="TurretPlacementController"/> instance that has called the dialog.
        /// </summary>
        public TurretPlacementController TurretPlacementCaller { get; private set; }
        #endregion

        #region Initializers & Terminators
        public override void Awake()
        {
            base.Awake();
            this.DisableDialog();
        }

        public override void OnDestroy() => base.OnDestroy(); 
        #endregion

        #region Methods & Functions
        public override bool IsPersistentBetweenScenes() => true;

        private void EnableDialog() => this._baseDialogNode.SetActive(true);

        private void DisableDialog() => this._baseDialogNode.SetActive(false);

        private ArgumentNullException FormatArgumentNullException() => new(string.Format(NULL_CALLER_ARGUMENT_EXCEPTION_MESSAGE, nameof(this.FormatArgumentNullException)));

        /// <summary>
        /// Opens the turret selector dialog.
        /// </summary>
        /// <param name="caller"><see cref="TurretPlacementController"/> instance that call the dialog.</param>
        /// <exception cref="ArgumentException">If the caller argument is <see langword="null"/>.</exception>
        public void OpenDialog(TurretPlacementController caller)
        {
            if (caller is null)
                throw this.FormatArgumentNullException();

            this.TurretPlacementCaller = caller;
            this.EnableDialog();
        }

        /// <summary>
        /// Closes the dialog.
        /// </summary>
        public void CloseDialog() => this.DisableDialog();
        #endregion
    }
}
