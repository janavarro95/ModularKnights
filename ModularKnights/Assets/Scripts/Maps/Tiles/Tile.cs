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

namespace Assets.Scripts.Maps.Tiles
{
    [Serializable]
    public class Tile
    {
        [JsonIgnore]
        public Sprite sprite;

        [JsonIgnore]
        public Texture2D texture;

        /// <summary>
        /// The path to the texture in the streaming assets path.
        /// </summary>
        public string pathToTexture;

        public Tile()
        {
            
        }

        public Tile(string TexturePath)
        {
            texture = (Texture2D)ContentManager.Instance.loadTexture2D(TexturePath);
            initialize();
        }

        public void initialize()
        {
            this.sprite = Sprite.Create(this.texture, new Rect(0, 0, 16, 16), new Vector2(0.5f, 0.5f), 16f);
        }
    }
}
