using UnityEngine;
using VisualStudioEX3.Artemis.Assets.Common.Controllers;

namespace VisualStudioEX3.Artemis.Assets.Player.Controllers
{
    /// <summary>
    /// Player Base Controller.
    /// </summary>
    [DisallowMultipleComponent, RequireComponent(typeof(HealthController), typeof(Rigidbody), typeof(SphereCollider))]
    public class PlayerBaseController : MonoBehaviour
    {
    }
}
