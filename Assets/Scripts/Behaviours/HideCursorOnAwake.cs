using UnityEngine;

namespace Behaviours
{
    public class HideCursorOnAwake : MonoBehaviour
    {
        private void Awake()
        {
#if !UNITY_EDITOR
            Cursor.visible = false;
#endif
        }
    }
}
