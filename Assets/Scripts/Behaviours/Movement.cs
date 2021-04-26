using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Animator))]
public class Movement : MonoBehaviour
{
    public float duration = 1;

    private PlayerInput playerInput;
    private Animator animator;
    private Tweener tweener;
    private static readonly int Walk = Animator.StringToHash("Walk");
    private static readonly int Stop = Animator.StringToHash("Stop");

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

        tweener = transform.DOMove(targetPosition, duration)
            .OnStart(() => { animator.SetTrigger(Walk); })
            .OnComplete(() =>
            {
                animator.SetTrigger(Stop);
                tweener = null;
            });

        transform.DOLookAt(targetPosition, 1);
    }
}