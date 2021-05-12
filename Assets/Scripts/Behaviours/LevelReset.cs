using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Behaviours
{
    public class LevelReset : MonoBehaviour
    {
        public new AudioClip audio;
        private AudioController audioController;

        private void Start()
        {
            audioController = FindObjectOfType<AudioController>();
        }

        public void OnReset(InputAction.CallbackContext context)
        {
            if (!context.performed)
            {
                return;
            }

            audioController.Play(audio);

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}