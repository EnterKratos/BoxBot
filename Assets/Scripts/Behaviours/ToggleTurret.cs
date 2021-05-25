using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Behaviours
{
    public class ToggleTurret : MonoBehaviour, IToggleable
    {
        public float DisableDelay;

        private IToggleable[] toggleables;
        private Turret turret;
        private bool currentState;

        private void Awake()
        {
            var selfToggleable = GetComponent<IToggleable>();
            turret = GetComponent<Turret>();

            var allToggleables = new HashSet<IToggleable>(GetComponentsInChildren<IToggleable>());
            allToggleables.Remove(selfToggleable);
            toggleables = allToggleables.ToArray();
        }

        public void Toggle(bool state)
        {
            if (currentState == state)
            {
                return;
            }

            foreach (var toggleable in toggleables)
            {
                toggleable.Toggle(state);
            }

            switch (state)
            {
                case true:
                    turret.Disable(DisableDelay);
                    break;
                case false:
                    turret.Enable();
                    break;
            }

            currentState = state;
        }
    }
}
