using Assets.Scripts.Maps.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Maps
{
    [Serializable]
    public class Map
    {

        public Dictionary<string, TileLayer> tileLayers;

        public Map()
        {

            this.tileLayers = new Dictionary<string, TileLayer>();
            this.tileLayers.Add("Floor", new TileLayer(10, 10));
            this.tileLayers.Add("Objects", new TileLayer(10, 10));
            this.tileLayers.Add("Characters", new TileLayer(10, 10));
            this.tileLayers.Add("Overlay", new TileLayer(10, 10));

        }

        public Map(int Width, int Height)
        {
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


    }
}
