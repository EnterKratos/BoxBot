using UnityEngine;

namespace Behaviours
{
	[RequireComponent(typeof(Animator))]
	public class Killable : MonoBehaviour
	{
		private Animator animator;
		private static readonly int Dead = Animator.StringToHash("Dead");

		private void Awake()
		{
			animator = GetComponent<Animator>();
		}

		public void Kill()
		{
			animator.SetBool(Dead, true);
		}
	}
}