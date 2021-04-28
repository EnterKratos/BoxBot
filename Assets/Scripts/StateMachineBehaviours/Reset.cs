using UnityEngine;

namespace StateMachineBehaviours
{
    public class Reset : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.WriteDefaultValues();
        }
    }
}