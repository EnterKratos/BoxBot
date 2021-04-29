﻿using System;
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

        private PlayerInput playerInput;
        private Animator animator;
        private Tweener translationTweener;
        private Tweener rotationTweener;
        private const string MoveAction = "Move";

        public void Stop()
        {
            translationTweener?.Kill();
            rotationTweener?.Kill();
        }

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
            if (context.action.triggered && context.action.name == MoveAction)
            {
                OnMove(context);
            }
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            if (animator.GetBool((int)RobotAnimatorParameter.Dead) ||
                animator.GetBool((int)RobotAnimatorParameter.Walk))
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
                if (animator.GetBool((int)RobotAnimatorParameter.Unable))
                {
                    return;
                }

                animator.SetBool((int)RobotAnimatorParameter.Unable, true);
                StartCoroutine(Defer(() => animator.SetBool((int)RobotAnimatorParameter.Unable, false)));

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
            var direction = new Vector3(0, -5, 0);
            var rayCastOrigin = new Vector3(targetPosition.x, targetPosition.y + 2f, targetPosition.z);

            Debug.DrawRay(rayCastOrigin, direction, Color.red, 1f);
            return !Physics.Raycast(rayCastOrigin, direction, direction.magnitude, obstructionsLayer);
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