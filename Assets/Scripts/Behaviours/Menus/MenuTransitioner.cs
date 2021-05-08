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
            controller.currentMenu.Disable();
            controller.currentMenu = targetMenu;
            targetMenu.Enable();
        }
    }
}