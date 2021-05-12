using System.Collections.Generic;
using System.Linq;
using StateMachineBehaviours;
using UnityEngine;

namespace Behaviours
{
    public class ButtonTrigger : MonoBehaviour, IPressable
    {
        public GameObject triggerObject;

        private Animator animator;
        private IToggleable toggleable;
        private AudioSource audioSource;
        private Dictionary<int, GameObject> objectsInContact = new Dictionary<int, GameObject>();

        private void Awake()
        {
            animator = GetComponent<Animator>();
            toggleable = triggerObject.GetComponent<IToggleable>();
            audioSource = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter(Collider other)
        {
            objectsInContact[GetOtherObjectId(other)] = other.gameObject;

            Press();
        }

        private void OnTriggerExit(Collider other)
        {
            objectsInContact.Remove(GetOtherObjectId(other));

            if (objectsInContact.Any())
            {
                return;
            }

            Release();
        }

        private int GetOtherObjectId(Collider other)
        {
            return other.gameObject.GetInstanceID();
        }

        public void Press()
        {
            Toggle(true);
        }

        public void Release()
        {
            Toggle(false);
        }

        private void Toggle(bool state)
        {
            if (state == animator.GetBool((int)ButtonAnimatorParameter.Press))
            {
                return;
            }

            animator.SetBool((int)ButtonAnimatorParameter.Press, state);
            toggleable.Toggle(state);
            audioSource.Play();
        }
    }
}