using UnityEngine;

namespace Behaviours
{
    public class ButtonTrigger : MonoBehaviour
    {
        public GameObject triggerObject;
        private IPressable pressable;

        private void Awake()
        {
            pressable = triggerObject.GetComponent<IPressable>();
        }

        private void OnTriggerEnter(Collider other)
        {
            pressable.Press();
        }

        private void OnTriggerExit(Collider other)
        {
            pressable.Release();
        }
    }
}