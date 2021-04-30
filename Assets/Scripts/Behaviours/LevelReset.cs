using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Behaviours
{
    public class LevelReset : MonoBehaviour
    {
#if UNITY_EDITOR
        private void Update()
        {
            if (!Keyboard.current.rKey.wasReleasedThisFrame)
            {
                return;
            }

            Debug.Log("Level Reset");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
#endif
    }
}