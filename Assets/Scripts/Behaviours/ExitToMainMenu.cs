using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Behaviours
{
    [RequireComponent(typeof(PlayerInput))]
    public class ExitToMainMenu : MonoBehaviour
    {
        private PlayerInput playerInput;
        private const string Exit = "Exit";

        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
        }

        private void OnEnable()
        {
            playerInput.onActionTriggered += OnActionTriggered;
        }

        private void OnDisable()
        {
            playerInput.onActionTriggered -= OnActionTriggered;
        }

        private void OnActionTriggered(InputAction.CallbackContext context)
        {
            if (!context.action.triggered)
            {
                return;
            }

            switch (context.action.name)
            {
                case Exit:
                    SceneManager.LoadScene(0);
                    break;
            }
        }
    }
}