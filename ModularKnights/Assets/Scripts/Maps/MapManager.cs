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

        public void Start()
        {
            Instance = this;
        }

        public void loadMap(Map m)
        {
            this.currentMap = m;
            SpriteRenderer r;

        }




    }
}
