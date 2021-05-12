using StateMachineBehaviours;
using UnityEngine;

namespace Behaviours
{
    public class DrawbridgeController : MonoBehaviour, IToggleable
    {
        private Collider linkedCollider;
        private Animator animator;
        private Animator barrierAnimator;
        private AudioSource audioSource;

        private void Awake()
        {
            linkedCollider = gameObject.GetLinkedComponent<Collider>();
            linkedCollider.enabled = true;

            animator = GetComponent<Animator>();
            barrierAnimator = gameObject.GetLinkedComponent<Animator>();

            audioSource = GetComponent<AudioSource>();
        }

        public void ToggleBridgeAccess(int access)
        {
            linkedCollider.enabled = access == 0;
        }

        public void Toggle(bool state)
        {
            animator.SetBool((int)DrawbridgeAnimatorParameter.Lower, state);
            barrierAnimator.SetBool((int)BarrierAnimatorParameter.Lower, state);
            audioSource.Play();
        }
    }
}