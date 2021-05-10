using Cinemachine;
using StateMachineBehaviours;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Behaviours
{
    public class Win : MonoBehaviour
    {
        public CinemachineVirtualCamera winCam;
        public int camPriority;
        public GameObject GoalCanvas;

        private GameObject player;
        private Animator animator;
        private int originalPriority;
        private bool levelComplete;
        private AudioSource goalAudio;

        private void Awake()
        {
            player = GameObject.FindWithTag("Player");
            animator = player.GetComponent<Animator>();
            originalPriority = winCam.Priority;
            goalAudio = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject != player)
            {
                return;
            }

            animator.SetTrigger((int)RobotAnimatorParameter.Win);
            winCam.Priority = camPriority;
            levelComplete = true;

            other.GetComponent<PlayerInput>().enabled = false;
            GoalCanvas.SetActive(true);
            goalAudio.Play();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject != player)
            {
                return;
            }

            winCam.Priority = originalPriority;
            levelComplete = false;

            other.GetComponent<PlayerInput>().enabled = true;
            GoalCanvas.SetActive(false);
        }

        public void OnSubmit(InputAction.CallbackContext context)
        {
            if (!levelComplete || !context.performed)
            {
                return;
            }

            Debug.Log("Submit");
        }
    }
}