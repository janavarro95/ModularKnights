using Assets.Scripts.Maps.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Maps
{
    [Serializable]
    public class Map
    {
        private int _width;
        private int _height;

        public int Width
        {
            get
            {
                return _width;
            }
        }
        public int Height
        {
            get
            {
                return _height;
            }
        }

        public Dictionary<string, TileLayer> tileLayers;

        public Map()
        {

            this.tileLayers = new Dictionary<string, TileLayer>();
            _height = 10;
            _width = 10;

        }

        public Map(int Width, int Height)
        {
            _height = Height;
            _width = Width;

            this.tileLayers = new Dictionary<string, TileLayer>();
            this.tileLayers.Add("Floor", new TileLayer(Width, Height));
            this.tileLayers.Add("Objects", new TileLayer(Width, Height));
            this.tileLayers.Add("Characters", new TileLayer(Width, Height));
            this.tileLayers.Add("Overlay", new TileLayer(Width, Height));
        }


        public virtual void resizeMap(int NewWidth, int NewHeight)
        {
            foreach(KeyValuePair<string,TileLayer> tileLayer in tileLayers)
            {
                tileLayer.Value.resizeLayer(NewWidth, NewHeight);
            }

        }

        public virtual bool isBattleMap()
        {
            return false;
        }

        public void centerCameraOnMap()
        {
            Camera.main.transform.position = new Vector3(this.Width / 2, this.Height / 2, -10);
        }


    }
}
