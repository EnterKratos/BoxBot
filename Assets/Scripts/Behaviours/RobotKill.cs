using StateMachineBehaviours;
using UnityEngine;

namespace Behaviours
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(IMovable))]
    public class RobotKill : MonoBehaviour
    {
        public AudioSource audioSource;

        private Animator animator;
        private IMovable movable;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            movable = GetComponent<IMovable>();
        }

        public void OnKill()
        {
            if (animator.GetBool((int)RobotAnimatorParameter.Dead))
            {
                return;
            }

            movable.Stop();
            animator.SetBool((int)RobotAnimatorParameter.Dead, true);
            audioSource.Play();
        }
    }
}