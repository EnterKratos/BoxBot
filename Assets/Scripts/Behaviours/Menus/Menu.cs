using System.Collections.Generic;
using System.Linq;
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
        public AudioClip MenuSubmit;
        public AudioClip MenuReturn;

        [HideInInspector]
        public Menu previousMenu;

        private MenuItem[] allMenuItems;
        private LinkedList<MenuItem> selectableMenuItems;
        private LinkedListNode<MenuItem> selectedMenuItem;
        private AudioController audioController;

        private void Awake()
        {
            allMenuItems = GetComponentsInChildren<MenuItem>(true);

            foreach (var menuItem in allMenuItems)
            {
                menuItem.StoreMaterials(DefaultMaterial, SelectedItemMaterial);
            }

            selectableMenuItems = new LinkedList<MenuItem>(allMenuItems.Where(menu => !menu.NoSelection));
            selectedMenuItem = selectableMenuItems.First;
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
            audioController.Play(MenuSubmit);

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
            foreach (var menuItem in allMenuItems)
            {
                menuItem.Enable();
            }
        }

        public void Disable()
        {
            foreach (var menuItem in allMenuItems)
            {
                menuItem.Disable();
            }
        }
    }
}