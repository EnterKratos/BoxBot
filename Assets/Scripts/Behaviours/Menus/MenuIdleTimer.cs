using System.Collections;
using StateMachineBehaviours;
using UnityEngine;

namespace Behaviours.Menus
{
    public class MenuIdleTimer : MonoBehaviour
    {
        public float waveDelay = 5f;
        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            StartCoroutine(WaveLoop());
        }

        private IEnumerator WaveLoop()
        {
            while (true)
            {
                yield return new WaitForSeconds(waveDelay);

                animator.SetTrigger((int)MenuRobotAnimatorParameter.Wave);
            }
        }
    }
}