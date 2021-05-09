using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Behaviours
{
    [RequireComponent(typeof(PlayerInput))]
    public class DeathUI : MonoBehaviour
    {
        public GameObject ui;

        private PlayerInput playerInput;

        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
        }

        public void DisplayUI()
        {
            StartCoroutine(DisplayUICoroutine());
        }

        private IEnumerator DisplayUICoroutine()
        {
            // Waiting because enabling the TMP component seems to cause a stutter
            yield return new WaitForSeconds(1f);

            ui.SetActive(true);
            playerInput.SwitchCurrentActionMap("UI");
        }
    }
}