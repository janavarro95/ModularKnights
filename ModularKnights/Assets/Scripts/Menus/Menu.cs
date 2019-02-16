using Assets.Scripts.GameInformation;
using Assets.Scripts.GameInput;
using Assets.Scripts.Menus.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menus
{
    public class Menu : MonoBehaviour
    {
        public static Menu ActiveMenu;
        public static bool IsMenuUp
        {
            get
            {
                return ActiveMenu != null;
            }
        }


        public GameCursor menuCursor;
        public MenuComponent selectedComponent;

        public virtual void Start()
        {
           
            
        }

        public virtual void Update()
        {

        }

        public virtual void exitMenu()
        {
            Destroy(this.gameObject);
        }

        public virtual bool snapCompatible()
        {
            return false;
        }

        public virtual void setUpForSnapping()
        {

        }

        public static void Instantiate()
        {
            Instantiate<Menu>();
        }

        public static void Instantiate<T>(bool OverrideMenu=false)
        {
            if(typeof(T) == typeof(Menu))
            {
                Instantiate("Menu",OverrideMenu);
            }
            else if (typeof(T) == typeof(MainMenu))
            {
                Instantiate("MainMenu",OverrideMenu);
            }
            else
            {
                throw new Exception("Hmm trying to call on a type of menu that doesn't exist.");
            }
        }

        public static void Instantiate(string Name,bool OverrideCurrentMenu=false)
        {
            if (OverrideCurrentMenu == false)
            {
                if (Menu.IsMenuUp) return;
                Menu.ActiveMenu = LoadMenuFromPrefab(Name).GetComponent<Menu>();
            }
            else
            {
                Menu.ActiveMenu.exitMenu();
                Menu.ActiveMenu = LoadMenuFromPrefab(Name).GetComponent<Menu>();
            }
            
        }

        protected static GameObject LoadMenuFromPrefab(string ItemName)
        {
            string path = Path.Combine(Path.Combine("Prefabs", "Menus"), ItemName);
            GameObject menuObj=Instantiate((GameObject)Resources.Load(path, typeof(GameObject)));
            return menuObj;
        }
    }
}
