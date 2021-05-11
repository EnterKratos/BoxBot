using UnityEngine;

namespace Behaviours
{
    public class InstantiateMissingByTag : MonoBehaviour
    {
        public string tagToFind;
        public GameObject prefab;

        private void Awake()
        {
            if (!GameObject.FindWithTag(tagToFind))
            {
                Instantiate(prefab);
            }
        }
    }
}