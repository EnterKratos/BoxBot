using StateMachineBehaviours;
using UnityEngine;

namespace Behaviours
{
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(IMovable))]
	public class Killable : MonoBehaviour
	{
		private Animator animator;
		private IMovable movable;

		private void Awake()
		{
			animator = GetComponent<Animator>();
			movable = GetComponent<IMovable>();
		}

		public void Kill()
		{
			movable.Stop();
			animator.SetBool((int)RobotAnimatorParameter.Dead, true);
		}
	}
}