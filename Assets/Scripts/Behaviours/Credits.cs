using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Behaviours
{
    public class Credits : MonoBehaviour
    {
        public Transform EndPosition;
        public float Delay;
        public float Duration;
        public Ease Ease = Ease.Linear;

        public void ResetSave()
        {
            if (SaveGameHelpers.Load().GameComplete)
            {
                SaveGameHelpers.ResetSave();
            }
        }

        private void Awake()
        {
            StartCoroutine(ScrollCredits());
        }

        private IEnumerator ScrollCredits()
        {
            yield return new WaitForSeconds(Delay);

            var tweener = transform.DOMoveY(EndPosition.position.y, Duration);
            tweener.SetEase(Ease);
            tweener.OnComplete(() =>
            {
                ResetSave();
                SceneManager.LoadScene((int)Scene.MainMenu);
            });
        }
    }
}
