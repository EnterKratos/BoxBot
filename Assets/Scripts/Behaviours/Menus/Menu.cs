using System.Collections.Generic;
using UnityEngine;

namespace Behaviours.Menus
{
    public class Menu : MonoBehaviour
    {
        public bool NoSelection;
        public MenuAction GlobalAction;
        public Material DefaultMaterial;
        public Material SelectedItemMaterial;
        public AudioClip MenuSelection;

        [HideInInspector]
        public Menu previousMenu;

        private LinkedList<MenuItem> menuItems;
        private LinkedListNode<MenuItem> selectedMenuItem;
        private AudioController audioController;

        private void Awake()
        {
            var menuItems = GetComponentsInChildren<MenuItem>(true);
            foreach (var menuItem in menuItems)
            {
                menuItem.StoreMaterials(DefaultMaterial, SelectedItemMaterial);
            }

            this.menuItems = new LinkedList<MenuItem>(menuItems);
            selectedMenuItem = this.menuItems.First;
        }

        private void Start()
        {
            audioController = FindObjectOfType<AudioController>();

            if (NoSelection || selectedMenuItem == null || selectedMenuItem.Value == null)
            {
                return;
            }

            selectedMenuItem.Value.Select();
        }

        public void Up()
        {
            if (NoSelection)
            {
                return;
            }

            selectedMenuItem.Value.Deselect();
            selectedMenuItem = selectedMenuItem.Next ?? selectedMenuItem.List.First;
            selectedMenuItem.Value.Select();

            audioController.Play(MenuSelection);
        }

        public void Down()
        {
            if (NoSelection)
            {
                return;
            }

            selectedMenuItem.Value.Deselect();
            selectedMenuItem = selectedMenuItem.Previous ?? selectedMenuItem.List.Last;
            selectedMenuItem.Value.Select();

            audioController.Play(MenuSelection);
        }

        public void Submit()
        {
            if (NoSelection && GlobalAction != null)
            {
                GlobalAction.Perform();
                return;
            }

            selectedMenuItem.Value.GetComponent<MenuAction>()
                ?.Perform();
        }

        public void Enable()
        {
            foreach (var menuItem in menuItems)
            {
                menuItem.Enable();
            }
        }

        public void Disable()
        {
            foreach (var menuItem in menuItems)
            {
                menuItem.Disable();
            }
        }
    }
}