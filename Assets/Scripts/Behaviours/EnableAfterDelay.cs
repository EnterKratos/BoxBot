using UnityEngine;

namespace Behaviours
{
    public class EnableAfterDelay : MonoBehaviour
    {
        public float Delay;
        public Behaviour[] BehaviourToEnable;

        private void Awake()
        {
            StartCoroutine(AsyncHelpers.Defer(() =>
            {
                foreach (var behaviour in BehaviourToEnable)
                {
                    behaviour.enabled = true;
                }
            }, Delay));
        }
    }
}
