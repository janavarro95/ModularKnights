using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Content
{

    /// <summary>
    /// 
    /// 
    /// Resources:
    ///     https://www.raywenderlich.com/479-using-streaming-assets-in-unity
    /// </summary>

    public class ContentManager:MonoBehaviour
    {
        public static ContentManager Instance;

        public virtual void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        public virtual void Start()
        {
            
        }

        public virtual void Update()
        {

        }





        public Sprite loadSprite(string RelativePath)
        {
            return loadSprite(RelativePath, new Rect(0, 0, 16, 16), new Vector2(0.5f, 0.5f), 16f);
        }

        
        public Sprite loadSprite(string RelativePath,Rect RectInfo, Vector2 Pivots, float PixelsPerUnit)
        {
            Sprite s= Sprite.Create(loadTexture2D(RelativePath), RectInfo, Pivots, PixelsPerUnit);
            if (s == null) throw new Exception("WTF???");
            s.texture.filterMode = FilterMode.Point; https://docs.unity3d.com/ScriptReference/FilterMode.html
            return s;
        }


        /// <summary>
        /// Loads a resource from a given path.
        /// </summary>
        /// <param name="AbsolutePath"></param>
        /// <returns></returns>
        public Texture2D loadTexture2D(string RelativePath)
        {
            string finalPath;
            WWW localFile;

            finalPath = Path.Combine(Application.streamingAssetsPath, RelativePath);

            if (!File.Exists(finalPath))
            {
                throw new Exception("File: "+ Path.GetFileName(finalPath)+" does not exist at the given path! " + finalPath);
            }

            localFile = new WWW(finalPath);

            if (localFile == null) throw new Exception("LOCAL FILE IS NULL!!!!");

            return localFile.texture;
        }


    }
}
