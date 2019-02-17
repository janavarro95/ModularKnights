using Assets.Scripts.GameInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Maps
{
    public class MapManager:MonoBehaviour
    {
        public static MapManager Instance;

        public Map currentMap;


        MapGrid grid;
        SpriteRenderer gridRenderer;

        [SerializeField]
        bool showGrid = true;

        public void Awake()
        {
            loadMap(new Map(16, 16));
        }

        public void Start()
        {
           
            Instance = this;

        }

        public void loadMap(Map m)
        {
            this.currentMap = m;
            grid = new MapGrid();
            grid.initialize(m, 16);
            //StartCoroutine(grid.initialize(m,16));
            gridRenderer = this.gameObject.transform.Find("GridRenderer").GetComponent<SpriteRenderer>();
            gridRenderer.sprite = grid.sprite;
            if (grid.sprite == null) Debug.Log("SPRITE IS NULL");
            centerCameraOnMap();
        }

        private void centerCameraOnMap()
        {
            Camera.main.transform.position = new Vector3(currentMap.Width / 2, currentMap.Height / 2, -10);
        }


        public void Update()
        {
            if (InputControls.APressed)
            {
                Debug.Log("AAAHHH???");
                Debug.Log(GameCursor.Instance.WorldPosition);
            }
        }



    }
}
