using System.Collections;
using UnityEngine;

namespace VisualStudioEX3.Artemis.Assets.TurretSystem.Controllers
{
    public class MiniGunTurretWeaponController : TurretWeaponController
    {
        #region Inspector fields
        [Header("MiniGun Behaviour"), SerializeField]
        private Transform _rotator;
        [SerializeField]
        private float _idleRotationSpeed;
        [SerializeField]
        private float _shootingRotationSpeed;
        [SerializeField]
        private float _rotationLerpSpeed;
        [SerializeField]
        private float _shootingScale;
        #endregion

        #region Initializers
        private IEnumerator Start()
        {
            while (true)
            {

                yield return null;
            }
        }
        #endregion

        #region Methods & Functions
        private void RotateBarrels() { } 
        #endregion
    }
}
