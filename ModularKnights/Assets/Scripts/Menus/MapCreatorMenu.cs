using Assets.Scripts.GameInput;
using Assets.Scripts.Maps;
using Assets.Scripts.Menus.Components;
using Assets.Scripts.Utilities.Math;
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
    ///     -paint tiles
    ///     -delete tiles
    ///     -edit layers
    ///     
    ///     -undo
    ///     -redo
    ///     
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

        GameObject mapEditorUI;
        DropDownComponent layerSelectionComponent;
        Button selectTileButton;
        Button saveButton;
        Button loadButton;
        Button goBackButton;

        GameObject mapEditor;

        public float cameraScrollSpeedModifier=10f;

        public bool editsHaveBeenMade;

        private enum MenuMode
        {
            NewLoadSelect,
            NewMapCreationPrompt,
            MapEditorUI,
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

            mapEditorUI = canvas.transform.Find("MapEditorUI").gameObject;
            layerSelectionComponent =new DropDownComponent(mapEditorUI.transform.Find("LayerSelect").gameObject.GetComponent<Dropdown>());
            selectTileButton = mapEditorUI.transform.Find("SelectTileButton").gameObject.GetComponent<Button>();
            saveButton = mapEditorUI.transform.Find("SaveMap").gameObject.GetComponent<Button>();
            loadMapButton= mapEditorUI.transform.Find("LoadMap").gameObject.GetComponent<Button>();
            goBackButton= mapEditorUI.transform.Find("GoBack").gameObject.GetComponent<Button>();


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
                mapEditorUI.SetActive(false);
                mapEditor.SetActive(false);
            }
            if(menuMode == MenuMode.NewMapCreationPrompt)
            {
                newLoadState.SetActive(false);
                newMapState.SetActive(true);
                mapEditorUI.SetActive(false);
                mapEditor.SetActive(false);
            }
            if(menuMode== MenuMode.MapEditorUI)
            {
                newLoadState.SetActive(false);
                newMapState.SetActive(false);
                mapEditorUI.SetActive(true);
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

            checkForInput();

            checkForDisablingEventSystem();
        }

        private void checkForInput()
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
            if (menuMode == MenuMode.NewMapCreationPrompt)
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
                    menuMode = MenuMode.MapEditorUI;
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
                    inputWidth.select();
                    return;
                }
                if (GameCursor.SimulateMousePress(inputHeight))
                {
                    inputHeight.select();
                    return;
                }
            }

            if (menuMode == MenuMode.MapEditorUI)
            {
                if (cursorClickIntersectsUI() == false)
                {
                    if (InputControls.APressed)
                    {
                        Debug.Log(GameCursor.Instance.WorldPosition);
                    }
                }
                checkForCameraMovement();

            }
        }


        /// <summary>
        /// Checks for moving the camera around the grid in a freeform roaming fasion fashion.
        /// </summary>
        private void checkForCameraMovement()
        {
            if (InputControls.LeftJoystickMoved)
            {
                if (Camera.main.transform.position.x >= 0 && Camera.main.transform.position.x <= currentMap.Width) Camera.main.transform.position += new Vector3(InputControls.LeftJoystickHorizontal, 0, 0)*Time.deltaTime*cameraScrollSpeedModifier;
                if (Camera.main.transform.position.y >= 0 && Camera.main.transform.position.y <= currentMap.Height) Camera.main.transform.position += new Vector3(0, InputControls.LeftJoystickVertical, 0)*Time.deltaTime*cameraScrollSpeedModifier;
                Vector3 pos = Camera.main.transform.position;
                pos.x = pos.x.Clamp(0, currentMap.Width);
                pos.y = pos.y.Clamp(0, currentMap.Height);
                Camera.main.transform.position = pos;
            }
        }

        /// <summary>
        /// Checks if the UI has been clicked to intersect the map from being clicked.
        /// </summary>
        /// <returns></returns>
        private bool cursorClickIntersectsUI()
        {
            if(GameCursor.SimulateMousePress(layerSelectionComponent)){
                layerSelectionComponent.select();
                return true;
            }
            if (GameCursor.SimulateMousePress(selectTileButton))
            {
                Debug.Log("Select a tile!");
                return true;
            }
            if (GameCursor.SimulateMousePress(saveButton))
            {
                Debug.Log("Save map!");
                return true;
            }
            if (GameCursor.SimulateMousePress(loadMapButton))
            {
                if (editsHaveBeenMade)
                {
                    Debug.Log("Trying to load a map but edits have been made! Do something about it!");
                    return true;
                }
                loadMap();
                return true;
            }
            if (GameCursor.SimulateMousePress(goBackButton))
            {
                if (editsHaveBeenMade)
                {
                    Debug.Log("Trying to exit map editor but edits have been made! Do something about it!");
                    return true;
                }
                menuMode = MenuMode.NewLoadSelect;
                setUpMenuVisibility();
                return true;
            }


            return false;
        }

        /// <summary>
        /// Used to prevent Unity's event system happening at the same time my system is working. I.E selecting components when I don't want it to.
        /// </summary>
        private void checkForDisablingEventSystem()
        {


            if (EventSystem.current.currentSelectedGameObject != null && (InputControls.LeftJoystickMoved == true) && this.snapCompatible() == false)
            {
                if (InputControls.GetControllerType() == InputControls.ControllerType.Keyboard)
                {
                    return;
                }
                EventSystem.current.SetSelectedGameObject(null);
            }
        }

        /// <summary>
        /// Open the prompt for creating a new map.
        /// </summary>
        private void createNewMapPromptButtonClick()
        {
            menuMode = MenuMode.NewMapCreationPrompt;
            setUpMenuVisibility();
        }

        /// <summary>
        /// Load a map from disk.
        /// </summary>
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

        /// <summary>
        /// Create a new map and show all of the components on the screen.
        /// </summary>
        private void createMap()
        {
            Map m = new Map(Convert.ToInt32(this.inputWidth.value),Convert.ToInt32(this.inputHeight.value));
            this.currentMap = m;

            grid = new MapGrid();
            grid.initialize(m, 16);
            //StartCoroutine(grid.initialize(m,16));

            this.layerSelectionComponent.clearOptions();
            List<string> options = new List<string>();
            foreach (var layer in m.tileLayers.Keys)
            {
                options.Add(layer);   
            }
            this.layerSelectionComponent.addOptions(options);

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
