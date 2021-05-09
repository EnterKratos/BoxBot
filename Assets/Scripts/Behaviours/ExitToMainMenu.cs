using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Behaviours
{
    public class ExitToMainMenu : MonoBehaviour
    {
        public void OnExit(InputAction.CallbackContext context)
        {
            if (!context.action.triggered)
            {
                return;
            }

            SceneManager.LoadScene(0);
        }
    }
}