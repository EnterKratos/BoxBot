using Cinemachine;
using StateMachineBehaviours;
using UnityEngine;

namespace Behaviours
{
    public class Win : MonoBehaviour
    {
        public CinemachineVirtualCamera winCam;
        public int camPriority;

        private GameObject player;
        private Animator animator;

        private void Awake()
        {
            player = GameObject.FindWithTag("Player");
            animator = player.GetComponent<Animator>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject != player)
            {
                return;
            }

            animator.SetTrigger((int)RobotAnimatorParameter.Win);
            winCam.Priority = camPriority;
        }
    }
}