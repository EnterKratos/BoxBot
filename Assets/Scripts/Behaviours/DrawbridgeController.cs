using StateMachineBehaviours;
using UnityEngine;

namespace Behaviours
{
    public class DrawbridgeController : MonoBehaviour, IToggleable
    {
        private Collider collider;
        private Animator animator;

        private void Awake()
        {
            collider = gameObject.GetLinkedComponent<Collider>();
            collider.enabled = true;

            animator = GetComponent<Animator>();
        }

        public void ToggleBridgeAccess(int access)
        {
            collider.enabled = access == 0;
        }

        public void Toggle(bool state)
        {
            animator.SetBool((int)DrawbridgeAnimatorParameter.Lower, state);
        }
    }
}