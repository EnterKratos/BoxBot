using UnityEngine;

namespace Behaviours
{
    public class HideCursorOnAwake : MonoBehaviour
    {
        private void Awake()
        {
            Cursor.visible = false;
        }
    }
}
