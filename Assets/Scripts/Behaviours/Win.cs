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
        private int originalPriority;

        private void Awake()
        {
            player = GameObject.FindWithTag("Player");
            animator = player.GetComponent<Animator>();
            originalPriority = winCam.Priority;
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

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject != player)
            {
                return;
            }

            winCam.Priority = originalPriority;
        }
    }
}