using StateMachineBehaviours;
using UnityEngine;

namespace Behaviours
{
    public class DrawbridgeController : MonoBehaviour, IPressable
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

        public void Press()
        {
            Lower();
        }

        public void Release()
        {
            Raise();
        }

        private void Raise()
        {
            animator.SetBool((int)DrawbridgeAnimatorParameter.Lower, false);
        }

        private void Lower()
        {
            animator.SetBool((int)DrawbridgeAnimatorParameter.Lower, true);
        }
    }
}