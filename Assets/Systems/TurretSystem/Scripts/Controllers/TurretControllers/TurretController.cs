using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using VisualStudioEX3.Artemis.Assets.Common.Controllers;
using VisualStudioEX3.Artemis.Assets.EnemySystem.Controllers;
using VisualStudioEX3.Artemis.Assets.LevelManagement;
using VisualStudioEX3.Artemis.Framework.Core.Contracts.Attributes;

namespace VisualStudioEX3.Artemis.Assets.TurretSystem.Controllers
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(HealthController), typeof(TurretWeaponController), typeof(NavMeshObstacle))]
    [RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
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

        #region Internal vars
        private TurretWeaponController _turretWeaponController;
        private int _targetRayCastLayerMask;
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

        #region Properties
        /// <summary>
        /// Gets the <see cref="HealthController"/> component of this turret.
        /// </summary>
        public HealthController Health { get; private set; }

        /// <summary>
        /// Gets the <see cref="Transform"/> target, if any target exist, else gets <see langword="null"/>.
        /// </summary>
        public Transform Target { get; private set; }
        #endregion

        #region Events
        /// <summary>
        /// Notifies when the controller starts to shoot.
        /// </summary>
        public event Action OnStartToShoot;

        /// <summary>
        /// Notifies when the controller stops to shoot.
        /// </summary>
        public event Action OnStopToShoot; 
        #endregion

        #region Initializers & Destructors
        private void Awake()
        {
            this.SetupHealthController();
            this.GenerateLayerMask();
            this.StartToSearchNearestTarget();
            this.SetupTurretWeaponController();
        }

        private void OnDestroy() => this.StopAllCoroutines();
        #endregion

        #region Update logic
        private void Update()
        {
            if (this.Target)
                this.UpdateForwardToTarget(this.Target, this.CalculateRotationSpeed());
        } 
        #endregion

        #region Methods & Functions
        private Transform GetTurretTransform() => this._rootTurretTransform;

        /// <summary>
        /// Setups the <see cref="TurretWeaponController"/> controller.
        /// </summary>
        /// <remarks>Overload this method if you need to get more than a single <see cref="AttackController"/> controller.</remarks>
        public virtual void SetupTurretWeaponController() => this._turretWeaponController = this.GetComponent<TurretWeaponController>();

        private void SetupHealthController()
        {
            this.Health = this.GetComponent<HealthController>();
            this.Health.OnDeath += this.OnDeath;
        }

        private IEnumerable<EnemyController> GetAllActiveEnemies() => LevelManagerController.Instance.GetAllActiveEnemies();

        private float GetDistanceToTarget(Transform target) => Vector3.Distance(this.GetTurretTransform().position, target.position);

        private bool IsTargetInRange(Transform target) => this.GetDistanceToTarget(target) <= this._searchRadius;

        private bool TryGetNearestTarget(out Transform target)
        {
            IEnumerable<EnemyController> activeEnemies = this.GetAllActiveEnemies();

            target = null;

            if (activeEnemies.Any())
            {
                activeEnemies = activeEnemies.OrderBy(e => this.GetDistanceToTarget(e.transform));
                target = activeEnemies.FirstOrDefault(e => this.IsTargetInRange(e.transform))?.transform;
            }

            return target;
        }

        private Vector3 GetForwardVectorToTarget(Transform target) => (target.position - this.GetTurretTransform().position).normalized;

        private bool IsATargetInRange(out Transform target) => this.TryGetNearestTarget(out target);

        private void GenerateLayerMask() => this._targetRayCastLayerMask = (1 << this._wallLayer) | (1 << this._targetLayer);

        private bool RaycastToTarget(Transform target, out RaycastHit hit) => Physics.Raycast(
            origin: this.GetTurretTransform().position,
            direction: this.GetForwardVectorToTarget(target),
            hitInfo: out hit,
            maxDistance: this._searchRadius,
            layerMask: this._targetRayCastLayerMask,
            queryTriggerInteraction: QueryTriggerInteraction.Collide);

        private bool IsTargetTag(RaycastHit hit) => hit.collider.CompareTag(this._targetTag);

        private bool IsTargetVisible(Transform target) => this.RaycastToTarget(target, out RaycastHit hit) && this.IsTargetTag(hit);

        private void StartToSearchNearestTarget() => this.StartCoroutine(this.SearchNearestTargetCoroutine());

        private void UpdateForwardToTarget(Transform target, float speed)
        {
            this.GetTurretTransform().forward = Vector3.Slerp(this.GetTurretTransform().forward, this.GetForwardVectorToTarget(target), speed);
            this.FixedLocalXAxisRotation();
        }

        private void FixedLocalXAxisRotation()
        {
            Vector3 currentLocalRotation = this.GetTurretTransform().localRotation.eulerAngles;
            currentLocalRotation.x = 0f;
            this.GetTurretTransform().localRotation = Quaternion.Euler(currentLocalRotation);
        }

        private Transform GetRandomEnemySpawnerTransform() => LevelManagerController.Instance.GetRandomEnemySpawnLocation().transform;

        private Transform GetTarget()
        {
            bool hasTarget = this.IsATargetInRange(out Transform target) && this.IsTargetVisible(target);

            if (hasTarget)
                this.StartShooting();
            else
            {
                target = this.GetRandomEnemySpawnerTransform(); // "Distracted" the turret aiming a random enemy spawner while not has an enemy target to aim.
                this.StopShooting();
            }

            return target;
        }

        /// <summary>
        /// Starts to shooting the target.
        /// </summary>
        /// <remarks>Overload this method if you need to manages more than a single <see cref="TurretWeaponController"/> controller.</remarks>
        public virtual void StartShooting()
        {
            this._turretWeaponController.HoldTrigger();
            this.OnStartToShoot?.Invoke();
        }

        /// <summary>
        /// Stops to shooting the target.
        /// </summary>
        /// <remarks>Overload this method if you need to manages more than a single <see cref="TurretWeaponController"/> controller.</remarks>
        public virtual void StopShooting()
        {
            this._turretWeaponController.ReleaseTrigger();
            this.OnStopToShoot?.Invoke();
        }

        private float CalculateRotationSpeed() => this._rotationSpeed * Time.deltaTime;

        private void DestroySelf() => GameObject.Destroy(this.gameObject);

        private void DrawRadiusGizmo()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(this.GetTurretTransform().position, this._searchRadius);
        }

        private void DrawForwardGizmo()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(this.GetTurretTransform().position, this.GetTurretTransform().forward * this._searchRadius);
        }

        private void DrawTargetGizmo()
        {
            if (this.Target)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(this.Target.position, 1f); 
            }
        }
        #endregion

        #region Event listeners
        private void OnDeath() => this.DestroySelf();

        private void OnDrawGizmosSelected()
        {
            this.DrawRadiusGizmo();
            this.DrawForwardGizmo();
            this.DrawTargetGizmo();
        }
        #endregion

        #region Coroutines
        private IEnumerator SearchNearestTargetCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(this._waitForSearchForNewTarget);

                this.Target = this.GetTarget();
            }
        }
        #endregion
    }
}
