using Assets.Scripts.GameInput;
using Assets.Scripts.Maps;
using Assets.Scripts.Menus.Components;
using SFB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Menus
{

    /// <summary>
    /// Todo:
    /// make ui for editor mode
    ///     -go back
    ///     -paint tiles
    ///     -delete tiles
    ///     -edit layers
    ///     -save map
    ///     -load map
    /// -make controls to move camera (wasd)
    ///     -ensure it can't scroll too far from the map grid.
    /// </summary>
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
        InputFieldComponent inputWidth;
        InputFieldComponent inputHeight;

        GameObject mapEditor;

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
            inputWidth =new InputFieldComponent(newMapState.transform.Find("InputWidth").gameObject.GetComponent<InputField>());
            inputHeight =new InputFieldComponent(newMapState.transform.Find("InputHeight").gameObject.GetComponent<InputField>());


            mapEditor = this.gameObject.transform.Find("MapEditor").gameObject;

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
                mapEditor.SetActive(false);
            }
            if(menuMode == MenuMode.NewMapCreationPrompt)
            {
                newLoadState.SetActive(false);
                newMapState.SetActive(true);
                mapEditor.SetActive(false);
            }
            if(menuMode== MenuMode.MapEditor)
            {
                newLoadState.SetActive(false);
                newMapState.SetActive(false);
                mapEditor.SetActive(true);
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
                    if (String.IsNullOrEmpty(inputWidth.value))
                    {
                        throw new Exception("Input width field is empty!");
                    }
                    if (String.IsNullOrEmpty(inputHeight.value))
                    {
                        throw new Exception("Input width field is empty!");
                    }

                    if (Convert.ToInt32(inputWidth.value) <= 0)
                    {
                        throw new Exception("Input width must be a positive number greater than 0!");
                    }
                    if (Convert.ToInt32(inputHeight.value) <= 0)
                    {
                        throw new Exception("Input height must be a positive number greater than 0!");
                    }

                    Debug.Log("CREATE THE MAP");
                    createMap();
                    menuMode = MenuMode.MapEditor;
                    setUpMenuVisibility();
                    return;
                }
                if (GameCursor.SimulateMousePress(createBackToPrompt))
                {
                    menuMode = MenuMode.NewLoadSelect;
                    setUpMenuVisibility();
                    return;
                }
                if (GameCursor.SimulateMousePress(inputWidth))
                {
                    inputWidth.Select();
                    return;
                }
                if (GameCursor.SimulateMousePress(inputHeight))
                {
                    inputHeight.Select();
                    return;
                }
            }

            checkForDisablingEventSystem();
        }

        private void checkForDisablingEventSystem()
        {


            if (EventSystem.current.currentSelectedGameObject != null && (InputControls.LeftJoystickMoved == true) && this.snapCompatible() == false)
            {
                if (InputControls.GetControllerType() == InputControls.ControllerType.Keyboard)
                {
                    Debug.Log("Am I keyboard???");
                    return;
                }
                Debug.Log("DESELECT");
                EventSystem.current.SetSelectedGameObject(null, new BaseEventData(EventSystem.current));
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

        private void createMap()
        {
            Map m = new Map(Convert.ToInt32(this.inputWidth.value),Convert.ToInt32(this.inputHeight.value));
            this.currentMap = m;

            grid = new MapGrid();
            grid.initialize(m, 16);
            //StartCoroutine(grid.initialize(m,16));



            gridRenderer = mapEditor.transform.Find("GridRenderer").GetComponent<SpriteRenderer>();
            gridRenderer.sprite = grid.sprite;
            if (grid.sprite == null) Debug.Log("SPRITE IS NULL");
            //centerCameraOnMap();
            this.currentMap.centerCameraOnMap();

        }

        public override void exitMenu()
        {
            base.exitMenu();
        }
    }
}
