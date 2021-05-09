using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Behaviours
{
    public class LevelReset : MonoBehaviour
    {
        public void OnReset(InputAction.CallbackContext context)
        {
            if (!context.performed)
            {
                return;
            }

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}