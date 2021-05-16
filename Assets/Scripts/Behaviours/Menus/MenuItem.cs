using TMPro;
using UnityEngine;

namespace Behaviours.Menus
{
    public class MenuItem : MonoBehaviour
    {
        public bool NoSelection;
        public bool ToggleSelf;
        private TMP_Text text;
        private Material defaultMaterial;
        private Material selectedItemMaterial;

        public void StoreMaterials(Material defaultMaterial, Material selectedItemMaterial)
        {
            this.defaultMaterial = defaultMaterial;
            this.selectedItemMaterial = selectedItemMaterial;
        }

        private void Awake()
        {
            text = GetComponent<TMP_Text>();
        }

        public void Select()
        {
            if (text)
            {
                text.fontSharedMaterial = selectedItemMaterial;
            }
        }

        public void Deselect()
        {
            if (text)
            {
                text.fontSharedMaterial = defaultMaterial;
            }
        }

        public void Enable()
        {
            ToggleEnabledState(true);
        }

        public void Disable()
        {
            ToggleEnabledState(false);
        }

        private void ToggleEnabledState(bool state)
        {
            if (ToggleSelf)
            {
                gameObject.SetActive(state);
                return;
            }

            if (text)
            {
                text.enabled = state;
            }
        }
    }
}