using UnityEngine;
using UnityEngine.Events;

namespace Behaviours
{
	public class Killable : MonoBehaviour
	{
		public UnityEvent OnKill;
		public void Kill()
		{
			OnKill?.Invoke();
		}
	}
}