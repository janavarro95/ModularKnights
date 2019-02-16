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
        }






    }
}
