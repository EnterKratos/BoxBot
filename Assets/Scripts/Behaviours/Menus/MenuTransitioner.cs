using UnityEngine;

namespace Behaviours.Menus
{
    [RequireComponent(typeof(MenuController))]
    public class MenuTransitioner : MonoBehaviour
    {
        private MenuController controller;

        private void Awake()
        {
            controller = GetComponent<MenuController>();
        }

        public void TransitionTo(Menu targetMenu)
        {
            targetMenu.previousMenu = controller.currentMenu;
            controller.currentMenu.Disable();
            controller.currentMenu = targetMenu;
            targetMenu.Enable();
        }

        public void TransitionBack()
        {
            var current = controller.currentMenu;
            var previous = current.previousMenu;

            current.Disable();
            controller.currentMenu = previous;
            previous.Enable();
        }
    }
}