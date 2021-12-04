using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VisualStudioEX3.Artemis.Assets.EnemySystem.Controllers;
using VisualStudioEX3.Artemis.Assets.LevelManagement;

namespace VisualStudioEX3.Artemis.Assets.TurretSystem.Controllers
{
    [DisallowMultipleComponent]
    public class TurretController : MonoBehaviour
    {
        #region Constants
        private const float MIN_WAIT_FOR_SEARCH_TARGET = 0.5f;
        private const float MAX_WAIT_FOR_SEARCH_TARGET = 10f;
        private const float DEFAULT_WAIT_FOR_SEARCH_TARGET = 1f;

        private const float MIN_ROTATION_SPEED = 0.5f;
        private const float MAX_ROTATION_SPEED = 10f;
        private const float DEFAULT_ROTATION_SPEED = 1f;

        private const float MIN_SEARCH_RADIUS = 1f;
        private const float MAX_SEARCH_RADIUS = 10f;
        private const float DEFAULT_SEARCH_RADIUS = 2.5f;

        private const bool DEFAULT_CAN_ROTATE = true;
        #endregion

        #region Inspector fields
        [SerializeField]
        private Transform _rootTurretTransform;
        [Header("AIM settings"), SerializeField, Range(MIN_WAIT_FOR_SEARCH_TARGET, MAX_WAIT_FOR_SEARCH_TARGET)]
        private float _waitForSearchForNewTarget = DEFAULT_WAIT_FOR_SEARCH_TARGET;
        [SerializeField, Range(MIN_SEARCH_RADIUS, MAX_SEARCH_RADIUS)]
        private float _searchRadius = DEFAULT_SEARCH_RADIUS;
        [SerializeField, Range(MIN_ROTATION_SPEED, MAX_ROTATION_SPEED)]
        private float _rotationSpeed = DEFAULT_ROTATION_SPEED;
        [SerializeField]
        private bool _canRotate = DEFAULT_CAN_ROTATE;
        #endregion

        #region Internal vars
        private Coroutine _lookAtTargetCoroutineInstance;
        #endregion

        #region Initializers & Destructors
        private void Awake() => this.StartToSearchNearestTarget();

        private void OnDestroy() => this.StopAllCoroutines();
        #endregion

        #region Methods & Functions
        private IEnumerable<EnemyController> GetAllActiveEnemies() => LevelManagerController.Instance.GetAllActiveEnemies();

        private bool IsDistanceToTargetInRange(Transform target) => Vector3.Distance(this.transform.position, target.position) <= this._searchRadius;

        private bool TryGetNearestTarget(out Transform target)
        {
            IEnumerable<EnemyController> activeEnemies = this.GetAllActiveEnemies();
            EnemyController nearestEnemy = activeEnemies.FirstOrDefault(e => this.IsDistanceToTargetInRange(e.transform));

            target = nearestEnemy == default(EnemyController) ? null : nearestEnemy.transform;

            return target;
        }

        private Vector3 GetForwardVectorToTarget(Transform target) => target.forward - this.transform.forward;

        private Quaternion GetRotationQuaternionToTheTarget(Transform target) => Quaternion.LookRotation(this.GetForwardVectorToTarget(target), Vector3.up);

        private bool IsATargetInRange(out Transform target) => this.TryGetNearestTarget(out target) && this.IsDistanceToTargetInRange(target.transform);

        private void StartToSearchNearestTarget() => this.StartCoroutine(this.SearchNearestTargetCoroutine());

        private void StartToLookAtNewTarget(Transform target)
        {
            if (this._lookAtTargetCoroutineInstance is not null)
                this.StopCoroutine(this._lookAtTargetCoroutineInstance);

            this._lookAtTargetCoroutineInstance = this.StartCoroutine(this.LookAtTargetCoroutine(target));
        }
        #endregion

        #region Coroutines
        private IEnumerator SearchNearestTargetCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(this._waitForSearchForNewTarget);

                if (!this.IsATargetInRange(out Transform target))
                    target = LevelManagerController.Instance.GetRandomEnemySpawnLocation().transform; // "Distracted" the turret while not has a target to look at.

                this.StartToLookAtNewTarget(target);
            }
        }

        private IEnumerator LookAtTargetCoroutine(Transform target)
        {
            while (target.gameObject.activeInHierarchy)
            {
                Quaternion lookRotationToTarget = this.GetRotationQuaternionToTheTarget(target);
                float maxDegreesDelta = this._rotationSpeed * Time.deltaTime;

                this._rootTurretTransform.rotation = Quaternion.RotateTowards(this._rootTurretTransform.rotation, lookRotationToTarget, maxDegreesDelta);

                yield return null;
            }
        }
        #endregion
    }
}
