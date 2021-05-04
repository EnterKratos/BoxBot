using UnityEngine;

namespace Behaviours
{
    public class Turret : MonoBehaviour
    {
        public Transform directionBegin;
        public Transform directionEnd;
        public Transform rayCastOrigin;
        public float laserSpeed = 0.1f;
        public Transform[] lasers;

        private GameObject player;
        private Killable playerKillable;
        private readonly float rayLength = 100f;
        private bool fired;

        private void Awake()
        {
            player = GameObject.FindWithTag("Player");
            playerKillable = player.GetComponent<Killable>();
        }

        private void Update()
        {
            var rayCastDetails = new RayCastDetails
            {
                Origin = rayCastOrigin.position,
                Direction = (directionEnd.position - directionBegin.position) * rayLength
            };

            Debug.DrawRay(rayCastDetails.Origin, rayCastDetails.Direction, Color.red, 1f);
            var result = Physics.Raycast(
                rayCastDetails.Origin,
                rayCastDetails.Direction,
                out var hit,
                rayCastDetails.Direction.magnitude);

            if (fired || !result || hit.collider.gameObject != player)
            {
                return;
            }

            fired = true;

            foreach (var laser in lasers)
            {
                var laserComponent = laser.GetComponent<Laser>();

                laserComponent.Fire(hit.point, laserSpeed, () => playerKillable.Kill());
            }
        }
    }
}