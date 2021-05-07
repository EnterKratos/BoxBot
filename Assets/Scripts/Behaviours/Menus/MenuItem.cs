using TMPro;
using UnityEngine;

namespace Behaviours.Menus
{
    public class MenuItem : MonoBehaviour
    {
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
            text.fontSharedMaterial = selectedItemMaterial;
        }

        public void Deselect()
        {
            text.fontSharedMaterial = defaultMaterial;
        }
    }
}