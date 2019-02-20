using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.Collections;
using Assets.Scripts.Content;
using Newtonsoft.Json.Converters;

namespace Assets.Scripts.Maps.Tiles
{
    [Serializable]
    public class Tile
    {
        [JsonIgnore]
        public Sprite sprite;

        [JsonIgnore]
        public Texture2D texture;

        public enum TileType
        {
            Plain,
            Mountain,
            Custom
        }

        public string id;

        [JsonIgnore]
        public string ID
        {
            get
            {
                return id;
            }
        }

        public TileType tileType;
        public string customName;

        /// <summary>
        /// The path to the texture in the streaming assets path.
        /// </summary>
        public string pathToTexture;

        [JsonIgnore]
        public string TileName
        {
            get
            {
                return this.getTileName();
            }
        }

        public Tile()
        {
            
        }

        public Tile(TileType type,string PathToTileMeta,string TextureName)
        {
            this.tileType = type;
            texture = (Texture2D)ContentManager.Instance.loadTexture2D(TextureName);
            initialize();
        }

        public Tile(string name,string PathToTileMeta,string TextureName)
        {
            this.tileType = TileType.Custom;
            this.customName = name;
            texture = (Texture2D)ContentManager.Instance.loadTexture2D(TextureName);
            initialize();
        }

        public virtual string getTileName()
        {
            if (this.tileType == TileType.Plain) return "Plain";
            else if (tileType == TileType.Mountain) return "Mountain";
            else if (tileType == TileType.Custom)
            {
                if (String.IsNullOrEmpty(this.customName)) return "CustomTile";
                else return this.customName;
            }
            return "Tile";
        }

        public void initialize()
        {
            this.sprite = Sprite.Create(this.texture, new Rect(0, 0, 16, 16), new Vector2(0.5f, 0.5f), 16f);
        }
    }
}
