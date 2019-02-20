using Assets.Scripts.Maps.Tiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Content
{
    public class ContentPack
    {
        string id;
        string folderName;
        string author;
        string version;

        public Dictionary<string, Tile> mapTiles;

        public ContentPack()
        {
            mapTiles = new Dictionary<string, Tile>();
        }

        public ContentPack(string ID, string FolderName,string Author,string Version)
        {
            this.id = ID;
            this.folderName = FolderName;
            this.author = Author;
            this.version = Version;
        }

        public virtual void serialzePack()
        {
            GameInformation.GameManager.Manager.serializer.Serialize(Path.Combine(Application.streamingAssetsPath, folderName, "ContentPackInfo.json"), this);
        }

        public virtual void deserialzePack(string PathToPack)
        {
            ContentPack pack = DeserializePack(PathToPack);

            this.folderName = pack.folderName;
            this.id = pack.id;
            this.author = pack.author;
            this.version = pack.version;

            this.mapTiles = pack.mapTiles;

        }

        /// <summary>
        /// Deserializes a generic content pack. Create a new content pack and use the instanced content pack.deserialize functionailty for overloading and custom serialization/deserialization.
        /// </summary>
        /// <param name="PathToPack"></param>
        /// <returns></returns>
        public static ContentPack DeserializePack(string PathToPack)
        {
            return GameInformation.GameManager.Manager.serializer.Deserialize<ContentPack>(PathToPack);
        }

    }
}
