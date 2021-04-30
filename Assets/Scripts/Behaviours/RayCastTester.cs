using UnityEngine;

namespace Behaviours
{
    [ExecuteAlways]
    public class RayCastTester : MonoBehaviour
    {
        public LayerMask layerMask;
        public Vector3 direction;

        private void Update()
        {
            Debug.DrawRay(transform.position, direction, Color.blue, 0.1f);

            var hits = Physics.RaycastAll(transform.position, direction, direction.magnitude, layerMask);

            foreach (var raycastHit in hits)
            {
                Debug.Log(raycastHit.collider, this);
            }
        }
    }
}