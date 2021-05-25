using System.Collections.Generic;
using System.Linq;
using StateMachineBehaviours;
using UnityEngine;

namespace Behaviours
{
    public class ButtonTrigger : MonoBehaviour, IPressable
    {
        public GameObject[] triggerObjects;

        private Animator animator;
        private List<IToggleable> toggleables = new List<IToggleable>();
        private AudioSource audioSource;
        private Dictionary<int, GameObject> objectsInContact = new Dictionary<int, GameObject>();

        private void Awake()
        {
            animator = GetComponent<Animator>();
            foreach (var triggerObject in triggerObjects)
            {
                var toggleable = triggerObject.GetComponent<IToggleable>();
                if (toggleable != null)
                {
                    toggleables.Add(toggleable);
                }
            }
            
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
            foreach (var toggleable in toggleables)
            {
                toggleable.Toggle(state);
            }
            audioSource.Play();
        }
    }
}