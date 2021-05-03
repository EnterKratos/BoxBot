using UnityEngine;

namespace Behaviours
{
    public class Turret : MonoBehaviour
    {
        public Transform directionBegin;
        public Transform directionEnd;

        private GameObject player;
        private Killable playerKillable;
        private readonly float rayLength = 100f;

        private void Awake()
        {
            player = GameObject.FindWithTag("Player");
            playerKillable = player.GetComponent<Killable>();
        }

        private void Update()
        {
            var rayCastDetails = new RayCastDetails
            {
                Origin = transform.position.AddY(.5f),
                Direction = (directionEnd.position - directionBegin.position) * rayLength
            };

            Debug.DrawRay(rayCastDetails.Origin, rayCastDetails.Direction, Color.red, 1f);
            var result = Physics.Raycast(
                rayCastDetails.Origin,
                rayCastDetails.Direction,
                out var hit,
                rayCastDetails.Direction.magnitude);

            if (!result || hit.collider.gameObject != player)
            {
                return;
            }

            playerKillable.Kill();
        }
    }
}