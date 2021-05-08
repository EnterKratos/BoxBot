using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Behaviours.Menus
{
    public class MenuController : MonoBehaviour
    {
        public Menu currentMenu;

        private Menu[] menus;
        private IEnumerable<Menu> OtherMenus => menus.Except(new[] {currentMenu});

        private void Awake()
        {
            menus = GetComponentsInChildren<Menu>();
        }

        private void Start()
        {
            currentMenu.Enable();

            foreach (var menu in OtherMenus)
            {
                menu.Disable();
            }
        }

        public void Navigate(InputAction.CallbackContext context)
        {
            if (!context.performed)
            {
                return;
            }

            var input = context.ReadValue<Vector2>();
            if (input.y < 0)
            {
                currentMenu.Up();
            }
            else
            {
                currentMenu.Down();
            }
        }

        public void Submit(InputAction.CallbackContext context)
        {
            if (!context.performed)
            {
                return;
            }

            currentMenu.Submit();
        }
    }
}