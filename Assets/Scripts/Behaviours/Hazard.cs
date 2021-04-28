using UnityEngine;

namespace Behaviours
{
	[RequireComponent(typeof(Collider))]
	public class Hazard : MonoBehaviour
	{
		private void OnTriggerEnter(Collider other)
		{
			var killable = other.GetComponent<Killable>();
			if (killable)
			{
				killable.Kill();
			}
		}
	}
}