using Assets.Scripts.GameInput;
using Assets.Scripts.Maps;
using SFB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menus
{
    public class MapCreatorMenu:Menu
    {

        public Map currentMap;

        MapGrid grid;
        SpriteRenderer gridRenderer;

        [SerializeField]
        bool showGrid = true;


        GameObject newLoadState;
        Button newMapButton;
        Button loadMapButton;

        GameObject newMapState;
        Button createMapButton;
        Button createBackToPrompt;

        private enum MenuMode
        {
            NewLoadSelect,
            NewMapCreationPrompt,
            MapEditor
        }

        private MenuMode menuMode;

        public override void Start()
        {
            Menu.ActiveMenu = this;
            GameObject canvas = this.gameObject.transform.Find("Canvas").gameObject;

            newLoadState = canvas.transform.Find("NewLoadState").gameObject;
            newMapButton = newLoadState.transform.Find("NewMapButton").gameObject.GetComponent<Button>();
            loadMapButton = newLoadState.transform.Find("LoadMapButton").gameObject.GetComponent<Button>();


            newMapState = canvas.transform.Find("NewMapCreationPrompt").gameObject;
            createMapButton = newMapState.transform.Find("Create").gameObject.GetComponent<Button>();
            createBackToPrompt = newMapState.transform.Find("Back").gameObject.GetComponent<Button>();


            this.menuCursor = GameCursor.Instance;
            menuMode = MenuMode.NewLoadSelect;

            setUpForSnapping();
            setUpMenuVisibility();
        }

        private void setUpMenuVisibility()
        {
            if(menuMode== MenuMode.NewLoadSelect)
            {
                newLoadState.SetActive(true);
                newMapState.SetActive(false);
            }
            if(menuMode == MenuMode.NewMapCreationPrompt)
            {
                newLoadState.SetActive(false);
                newMapState.SetActive(true);
            }
        }

        public override void setUpForSnapping()
        {
            
        }

        public override bool snapCompatible()
        {
            return false;
        }

        public override void Update()
        {
            if (menuMode == MenuMode.NewLoadSelect)
            {

                if (GameCursor.SimulateMousePress(newMapButton))
                {
                    createNewMapPromptButtonClick();
                }
                if (GameCursor.SimulateMousePress(loadMapButton))
                {
                    loadMap();
                }
            }
            if(menuMode == MenuMode.NewMapCreationPrompt)
            {
                if (GameCursor.SimulateMousePress(createMapButton))
                {
                    Debug.Log("CREATE THE MAP");
                    return;
                }
                if (GameCursor.SimulateMousePress(createBackToPrompt))
                {
                    menuMode = MenuMode.NewLoadSelect;
                    setUpMenuVisibility();
                    return;
                }
            }

        }

        private void createNewMapPromptButtonClick()
        {
            menuMode = MenuMode.NewMapCreationPrompt;
            setUpMenuVisibility();
        }

        private void loadMap()
        {
            var extensions = new[] {
            new ExtensionFilter("MapFile","json"),
            new ExtensionFilter("All Files", "*" ),
            };
            var paths = StandaloneFileBrowser.OpenFilePanel("Open File", "", extensions, true);
            if (paths.Length == 0) return;
            Debug.Log(paths[0]);
        }

        public override void exitMenu()
        {
            base.exitMenu();
        }
    }
}
