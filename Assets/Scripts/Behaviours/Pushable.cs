using DG.Tweening;
using UnityEngine;

namespace Behaviours
{
    public class Pushable : MonoBehaviour
    {
        public LayerMask obstructionsLayer;

        public bool CanPush(RayCastDetails rayCastDetails)
        {
            Debug.DrawRay(rayCastDetails.Origin, rayCastDetails.Direction, Color.blue, 1f);

            return !Physics.Raycast(rayCastDetails.Origin, rayCastDetails.Direction, rayCastDetails.Direction.magnitude, obstructionsLayer);
        }

        public void Push(Vector3 targetPosition, float duration)
        {
            transform.DOMove(targetPosition, duration);
        }
    }
}