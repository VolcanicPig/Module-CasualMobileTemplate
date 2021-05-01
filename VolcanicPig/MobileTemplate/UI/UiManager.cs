using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VolcanicPig.Mobile.Ui
{
    public class UiManager : SingletonBehaviour<UiManager>
    {
        public Menu[] menus;
        private Menu _currentMenu;
        public Menu CurrentMenu => _currentMenu; 

        public override void Awake()
        {
            base.Awake();
            GetMenus();
        }

        private void GetMenus()
        {
            menus = GetComponentsInChildren<Menu>();
        }

        public void OpenMenu(string id)
        {
            if (_currentMenu)
            {
                _currentMenu.Close();
            }

            Menu newMenu = GetMenuById(id);
            if (newMenu)
            {
                newMenu.Open();
                _currentMenu = newMenu;
            }
        }

        public bool IsMenuOpen(string id)
        {
            return _currentMenu.MenuId == id; 
        }

        private Menu GetMenuById(string id)
        {
            foreach (Menu menu in menus)
            {
                if (menu.MenuId.Equals(id))
                {
                    return menu;
                }
            }

            Debug.LogError($"{this} No Menu found with id {id}");
            return null;
        }
    }
}
