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
        private Scene nextLevel;
        private bool loadNextLevel;
        private PlayerInput menuInput;

        private void Awake()
        {
            player = GameObject.FindWithTag("Player");
            animator = player.GetComponent<Animator>();
            originalPriority = winCam.Priority;
            goalAudio = GetComponent<AudioSource>();
            nextLevel = SceneHelpers.GetNextScene();
            menuInput = GoalCanvas.GetComponentInChildren<PlayerInput>(true);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject != player)
            {
                return;
            }

            StartCoroutine(SceneHelpers.LoadSceneInBackground(nextLevel, () => loadNextLevel));

            animator.SetTrigger((int)RobotAnimatorParameter.Win);
            winCam.Priority = camPriority;
            levelComplete = true;

            other.GetComponent<PlayerInput>().enabled = false;
            GoalCanvas.SetActive(true);
            StartCoroutine(AsyncHelpers.Defer(() =>
            {
                menuInput.enabled = true;
            }, 1.5f));
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
            menuInput.enabled = false;
        }

        public void OnSubmit(InputAction.CallbackContext context)
        {
            if (!levelComplete || !context.performed)
            {
                return;
            }

            loadNextLevel = true;
        }
    }
}