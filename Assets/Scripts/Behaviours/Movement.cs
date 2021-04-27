using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Animator))]
public class Movement : MonoBehaviour
{
    public float duration = 1;
    public LayerMask obstructionsLayer;

    private PlayerInput playerInput;
    private Animator animator;
    private Tweener tweener;
    private static readonly int Walk = Animator.StringToHash("Walk");
    private static readonly int Stop = Animator.StringToHash("Stop");
    private static readonly int No = Animator.StringToHash("No");

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerInput.SwitchCurrentControlScheme("Keyboard&Mouse", Keyboard.current);

        animator = GetComponent<Animator>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!context.action.triggered || tweener != null)
        {
            return;
        }

        var input = context.ReadValue<Vector2>();

        var position = transform.position;

        var targetPosition = new Vector3(
            position.x + input.x,
            position.y,
            position.z + input.y);

        var heading = targetPosition - position;
        var direction = heading / heading.magnitude;

        var rayCastOrigin = new Vector3(position.x, position.y + 0.5f, position.z);

        const int rayDistance = 1;
        Debug.DrawRay(rayCastOrigin, direction, Color.red, 0.2f);
        var hit = Physics.Raycast(rayCastOrigin, direction, rayDistance, obstructionsLayer);

        var animInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (hit)
        {
            if (animInfo.tagHash != No)
            {
                animator.SetTrigger(No);
            }

            return;
        }

        Move(targetPosition);
    }

    private void Move(Vector3 targetPosition)
    {
        tweener = transform.DOMove(targetPosition, duration)
            .OnStart(() => { animator.SetTrigger(Walk); })
            .OnComplete(() =>
            {
                animator.SetTrigger(Stop);
                tweener = null;
            });

        transform.DOLookAt(targetPosition, duration);
    }
}