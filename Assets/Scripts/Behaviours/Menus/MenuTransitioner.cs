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
            controller.currentMenu.gameObject.SetActive(false);
            controller.currentMenu = targetMenu;
            targetMenu.gameObject.SetActive(true);
        }
    }
}