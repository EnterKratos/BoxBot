using System;
using System.Collections;
using DG.Tweening;
using StateMachineBehaviours;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Behaviours
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(Animator))]
    public class Movable : MonoBehaviour, IMovable
    {
        public float duration = 1;
        public LayerMask obstructionsLayer;

        private Animator animator;
        private Tweener translationTweener;
        private Tweener rotationTweener;

        public void Stop()
        {
            translationTweener?.Kill();
            rotationTweener?.Kill();
        }

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (animator.GetBool((int)RobotAnimatorParameter.Dead) ||
                animator.GetBool((int)RobotAnimatorParameter.Walk))
            {
                return;
            }

            var input = context.ReadValue<Vector2>();

            var position = transform.position;

            var movement = new Movement
            {
                BasePosition = position,
                Direction = input
            };

            var targetPosition = movement.GetDestination(1);

            var canMove = CanMove(targetPosition);
            var canPush = CanPush(movement, out var pushable);

            if (canMove && pushable == null)
            {
                Move(targetPosition);
                return;
            }

            if (canMove && canPush && pushable != null)
            {
                Move(targetPosition);
                pushable.Push(movement.GetDestination(2), duration);
                return;
            }

            Unable();
        }

        private void Unable()
        {
            if (animator.GetBool((int)RobotAnimatorParameter.Unable))
            {
                return;
            }

            animator.SetBool((int)RobotAnimatorParameter.Unable, true);
            StartCoroutine(Defer(() => animator.SetBool((int)RobotAnimatorParameter.Unable, false)));
        }

        private IEnumerator Defer(Action action, float delay = 0.1f)
        {
            yield return new WaitForSeconds(delay);
            action();
        }

        private bool CanMove(Vector3 targetPosition)
        {
            var rayCastDetails = new RayCastDetails
            {
                Origin = targetPosition.AddY(2f),
                Direction = new Vector3(0, -5, 0)
            };

            Debug.DrawRay(rayCastDetails.Origin, rayCastDetails.Direction, Color.red, 1f);
            return !Physics.Raycast(rayCastDetails.Origin, rayCastDetails.Direction, rayCastDetails.Direction.magnitude, obstructionsLayer);
        }

        private bool CanPush(Movement movement, out Pushable pushable)
        {
            var oneSpace = movement.GetDestination(1).AddY(2f);
            var twoSpaces = movement.GetDestination(2).AddY(2f);
            var direction = new Vector3(0, -5, 0);

            var rayCastDetails = new RayCastDetails
            {
                Origin = oneSpace,
                Direction = direction
            };

            Debug.DrawRay(rayCastDetails.Origin, rayCastDetails.Direction, Color.green, 1f);
            Physics.Raycast(rayCastDetails.Origin, rayCastDetails.Direction, out var hitInfo, rayCastDetails.Direction.magnitude);

            pushable = hitInfo.collider != null ?
                hitInfo.collider.gameObject.GetComponent<Pushable>() :
                null;

            return pushable && pushable.CanPush(new RayCastDetails
            {
                Origin = twoSpaces,
                Direction = direction
            });
        }

        private void Move(Vector3 targetPosition)
        {
            translationTweener = transform.DOMove(targetPosition, duration)
                .OnStart(() => animator.SetBool((int)RobotAnimatorParameter.Walk, true))
                .OnComplete(() => animator.SetBool((int)RobotAnimatorParameter.Walk, false));

            rotationTweener = transform.DOLookAt(targetPosition, duration);
        }
    }
}