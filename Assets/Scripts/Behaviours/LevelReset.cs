using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Behaviours
{
    public class LevelReset : MonoBehaviour
    {
        private void Update()
        {
            if (!Keyboard.current.rKey.wasReleasedThisFrame)
            {
                return;
            }

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}