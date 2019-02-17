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

        Button newMapButton;
        Button loadMapButton;

        public override void Start()
        {
            Menu.ActiveMenu = this;
            GameObject canvas = this.gameObject.transform.Find("Canvas").gameObject;
            newMapButton = canvas.transform.Find("NewMapButton").gameObject.GetComponent<Button>();
            loadMapButton = canvas.transform.Find("LoadMapButton").gameObject.GetComponent<Button>();
            this.menuCursor = GameCursor.Instance;
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
            if (GameCursor.SimulateMousePress(newMapButton))
            {

            }
            if (GameCursor.SimulateMousePress(loadMapButton))
            {
                loadMap();
            }
        }

        private void loadMap()
        {
            var extensions = new[] {
            new ExtensionFilter("MapFile","json"),
            new ExtensionFilter("All Files", "*" ),
            };
            var paths = StandaloneFileBrowser.OpenFilePanel("Open File", "", extensions, true);
        }

        public override void exitMenu()
        {
            base.exitMenu();
        }
    }
}
