using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VisualStudioEX3.Artemis.Assets.EnemySystem.Controllers;
using VisualStudioEX3.Artemis.Assets.LevelManagement;
using VisualStudioEX3.Artemis.Framework.Core.Contracts.Attributes;

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

        private const float MIN_SEARCH_RADIUS = 5f;
        private const float MAX_SEARCH_RADIUS = 50f;
        private const float DEFAULT_SEARCH_RADIUS = 15f;
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
        [SerializeField, Layer, Tooltip("Layer used to block the raycast.")]
        private int _wallLayer;
        [SerializeField, Layer, Tooltip("Layer to lookup only for enemies.")]
        private int _targetLayer;
        [SerializeField, Tag, Tooltip("Tag value used to indentify the enemy attacks on OnCollisionStay event.")]
        private string _targetTag;
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

        private Vector3 GetForwardVectorToTarget(Transform target) => (target.position - this.transform.position).normalized;

        private bool IsATargetInRange(out Transform target) => this.TryGetNearestTarget(out target) && this.IsDistanceToTargetInRange(target.transform);

        private int GenerateLayerMask() => (1 << this._wallLayer) | (1 << this._targetLayer);

        private bool RaycastToTarget(Transform target) => Physics.Raycast(
            origin: this._rootTurretTransform.position, 
            direction: this.GetForwardVectorToTarget(target), 
            maxDistance: this._searchRadius,
            layerMask: this.GenerateLayerMask());

        private bool IsTargetVisible(Transform target) => this.RaycastToTarget(target);

        private void StartToSearchNearestTarget() => this.StartCoroutine(this.SearchNearestTargetCoroutine());

        private void StartToLookAtNewTarget(Transform target)
        {
            if (this._lookAtTargetCoroutineInstance is not null)
                this.StopCoroutine(this._lookAtTargetCoroutineInstance);

            this._lookAtTargetCoroutineInstance = this.StartCoroutine(this.LookAtTargetCoroutine(target));
        }

        private void UpdateForwardToTarget(Transform target, float speed)
        {
            this._rootTurretTransform.forward = Vector3.Slerp(this._rootTurretTransform.forward, this.GetForwardVectorToTarget(target), speed);
        }

        private Transform GetRandomEnemySpawnerTransform() => LevelManagerController.Instance.GetRandomEnemySpawnLocation().transform;

        private Transform GetTarget()
        {
            bool hasTarget = this.IsATargetInRange(out Transform target) && this.IsTargetVisible(target);

            if (!hasTarget)
                target = this.GetRandomEnemySpawnerTransform(); // "Distracted" the turret aimin a random enemy spawner while not has an enemy target to aim.

            return target;
        }

        private float CalculateRotationSpeed() => this._rotationSpeed * Time.deltaTime;

        private void DrawRadiusGizmo()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(this._rootTurretTransform.position, this._searchRadius);
        }

        private void DrawForwardGizmo()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(this._rootTurretTransform.position, this._rootTurretTransform.forward * this._searchRadius);
        }
        #endregion

        #region Event listeners
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag(this._targetTag))
                print("Attacked by enemy!");
        }

        private void OnDrawGizmosSelected()
        {
            this.DrawRadiusGizmo();
            this.DrawForwardGizmo();
        }
        #endregion

        #region Coroutines
        private IEnumerator SearchNearestTargetCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(this._waitForSearchForNewTarget);

                this.StartToLookAtNewTarget(this.GetTarget());
            }
        }

        private IEnumerator LookAtTargetCoroutine(Transform target)
        {
            while (target.gameObject.activeInHierarchy)
            {
                this.UpdateForwardToTarget(target, this.CalculateRotationSpeed());

                yield return null;
            }
        }
        #endregion
    }
}
