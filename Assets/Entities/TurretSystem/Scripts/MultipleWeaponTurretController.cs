using System;

namespace VisualStudioEX3.Artemis.Assets.TurretSystem.Controllers
{
    /// <summary>
    /// Multiple weapon turret controller.
    /// </summary>
    /// <remarks>This controller able to manage multiple <see cref="TurretWeaponController"/> components in this object.</remarks>
    public class MultipleWeaponTurretController : TurretController
    {
        #region Internal vars
        private TurretWeaponController[] _weaponControllers;
        #endregion

        #region Methods & Functions
        public override void SetupTurretWeaponController() => this._weaponControllers = this.GetComponents<TurretWeaponController>();

        public override void StartShooting() => this.DoTriggerAction((weapon) => weapon.HoldTrigger());

        public override void StopShooting() => this.DoTriggerAction((weapon) => weapon.ReleaseTrigger());

        private void DoTriggerAction(Action<TurretWeaponController> triggerAction)
        {
            foreach (TurretWeaponController weapon in this._weaponControllers)
                triggerAction(weapon);
        }
        #endregion
    }
}
