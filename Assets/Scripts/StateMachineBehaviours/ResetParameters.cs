using System;
using System.Linq;
using UnityEngine;

namespace StateMachineBehaviours
{
    public class ResetParameters<TEnum> : StateMachineBehaviour
        where TEnum : struct, Enum, IConvertible
    {
        public TEnum parameterToReset;

        private int parameterInt;

        private void Awake()
        {
            parameterInt = parameterToReset.ToInt32(null);
        }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (parameterInt == default)
            {
                animator.WriteDefaultValues();
                return;
            }

            var parameter = animator.parameters.Single(p => p.nameHash == parameterInt);

            ResetParameter(animator, parameter);
        }

        private void ResetParameter(Animator animator, AnimatorControllerParameter parameter)
        {
            switch (parameter.type)
            {
                case AnimatorControllerParameterType.Float:
                    animator.SetFloat(parameter.nameHash, parameter.defaultFloat);
                    break;
                case AnimatorControllerParameterType.Int:
                    animator.SetInteger(parameter.nameHash, parameter.defaultInt);
                    break;
                case AnimatorControllerParameterType.Bool:
                    animator.SetBool(parameter.nameHash, parameter.defaultBool);
                    break;
                case AnimatorControllerParameterType.Trigger:
                    animator.ResetTrigger(parameter.nameHash);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}