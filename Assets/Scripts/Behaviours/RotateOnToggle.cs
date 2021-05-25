using DG.Tweening;
using UnityEngine;

namespace Behaviours
{
    public class RotateOnToggle : MonoBehaviour, IToggleable
    {
        public Vector3 Rotation;
        public float Duration;
        public RotateMode Mode;

        public void Toggle(bool state)
        {
            switch (state)
            {
                case true:
                    transform.DORotate(Rotation, Duration, Mode);
                    break;
                case false:
                    transform.DORotate(-Rotation, Duration, Mode);
                    break;
            }
        }
    }
}
