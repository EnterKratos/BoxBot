using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StateMachineBehaviours
{
    public class ValidateParameters<TEnum> : StateMachineBehaviour
        where TEnum : struct, Enum, IConvertible
    {
        private void OnValidate()
        {
            var animator = FindObjectOfType<Animator>();
            if (animator == null)
            {
                return;
            }

            var controllerName = animator.runtimeAnimatorController.name;

            foreach (var parameter in animator.parameters)
            {
                if (Enum.IsDefined(typeof(TEnum), parameter.nameHash))
                {
                    continue;
                }

                Debug.LogError(
                    $"'{parameter.name}' is not defined in the Enum '{typeof(TEnum)}'.\n" +
                    $"Either add '{parameter.name}' to '{typeof(TEnum)}' " +
                    $"(e.g {parameter.name} = {Animator.StringToHash(parameter.name)})\n" +
                    $"or remove the '{parameter.name}' parameter from '{controllerName}'.\n" +
                    "Please ensure that your parameter names are valid C# identifiers!",
                    this);
            }

            foreach (var val in
                Enum.GetValues(typeof(TEnum))
                    .Cast<TEnum>()
                    .Where(v => !v.ToInt32(null).Equals(default)))
            {
                var parameterExists = animator.parameters
                    .Any(p => p.name == val.ToString());

                if (parameterExists)
                {
                    continue;
                }

                Debug.LogError(
                    $"The Enum value '{val}' is not defined in the parameter list of '{controllerName}'.\n" +
                    $"Either add a '{val}' parameter to '{controllerName}' or remove '{val}' from '{typeof(TEnum)}'.\n" +
                    "Please ensure that your parameter names are valid C# identifiers!",
                    this);
            }
        }
    }
}