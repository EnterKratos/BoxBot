using StateMachineBehaviours;
using UnityEngine;

namespace Behaviours
{
	[RequireComponent(typeof(Animator))]
	public class Killable : MonoBehaviour
	{
		private Animator animator;

		private void Awake()
		{
			animator = GetComponent<Animator>();
		}

		public void Kill()
		{
			animator.SetBool((int)RobotAnimatorParameter.Dead, true);
		}
	}
}