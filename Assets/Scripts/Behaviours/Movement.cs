using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Behaviours
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(Animator))]
    public class Movement : MonoBehaviour
    {
        public float duration = 1;
        public LayerMask obstructionsLayer;

        private PlayerInput playerInput;
        private Animator animator;
        private static readonly int Walk = Animator.StringToHash("Walk");
        private static readonly int Unable = Animator.StringToHash("Unable");
        private const string MoveAction = "Move";

        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
            playerInput.SwitchCurrentControlScheme("Keyboard&Mouse", Keyboard.current);

            animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            playerInput.onActionTriggered += OnActionTriggered;
        }

        private void OnDisable()
        {
            playerInput.onActionTriggered -= OnActionTriggered;
        }

        private void OnActionTriggered(InputAction.CallbackContext context)
        {
            if (context.action.name == MoveAction)
            {
                OnMove(context);
            }
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            if (!context.action.triggered || animator.GetBool(Walk))
            {
                return;
            }

            var input = context.ReadValue<Vector2>();

            var position = transform.position;

            var targetPosition = new Vector3(
                position.x + input.x,
                position.y,
                position.z + input.y);

            if (!CanMove(targetPosition))
            {
                if (animator.GetBool(Unable))
                {
                    return;
                }

                animator.SetBool(Unable, true);
                StartCoroutine(Defer(() => animator.SetBool(Unable, false)));

                return;
            }

            Move(targetPosition);
        }

        private IEnumerator Defer(Action action, float delay = 0.1f)
        {
            yield return new WaitForSeconds(delay);
            action();
        }

        private bool CanMove(Vector3 targetPosition)
        {
            var position = transform.position;
            var heading = targetPosition - position;
            var direction = heading / heading.magnitude;

            var rayCastOrigin = new Vector3(position.x, position.y + 0.5f, position.z);

            const int rayDistance = 1;
            Debug.DrawRay(rayCastOrigin, direction, Color.red, 0.2f);
            return !Physics.Raycast(rayCastOrigin, direction, rayDistance, obstructionsLayer);
        }

        private void Move(Vector3 targetPosition)
        {
            transform.DOMove(targetPosition, duration)
                .OnStart(() => animator.SetBool(Walk, true))
                .OnComplete(() => animator.SetBool(Walk, false));

            transform.DOLookAt(targetPosition, duration);
        }
    }
}