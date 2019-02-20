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



        public string tileID;
        public string packID;

        

        [JsonIgnore]
        public string ContentPackID
        {
            get
            {
                return packID;
            }
        }

        [JsonIgnore]
        public string ID
        {
            get
            {
                return tileID;
            }
        }

        public TileType tileType;
        public string customName;

        /// <summary>
        /// The path to the texture in the streaming assets path.
        /// </summary>
        public string textureName;

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

        public Tile(string FullPathToFileMeta)
        {
            Tile t= GameInformation.GameManager.Manager.serializer.Deserialize<Tile>(FullPathToFileMeta);
            this.tileType = t.tileType;
            this.packID = t.packID;
            this.tileID = t.tileID;
            this.customName = t.customName;
            this.textureName = t.textureName;

            texture = (Texture2D)ContentManager.Instance.loadTexture2D(Path.Combine(Path.GetDirectoryName(FullPathToFileMeta),this.textureName));

            initialize();


        }

        public Tile(TileType type,string PackID, string TileID,string customName,string textureName,string pathToImage="")
        {
            this.tileType = type;
            this.packID = PackID;
            this.tileID = TileID;
            this.customName = customName;
            this.textureName = textureName;

            if (String.IsNullOrEmpty(pathToImage))
            {
                return;
            }
            else
            {
                texture = (Texture2D)ContentManager.Instance.loadTexture2D(Path.Combine(pathToImage,textureName));
                initialize();
            }
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
