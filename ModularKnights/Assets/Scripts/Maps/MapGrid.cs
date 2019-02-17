using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Maps
{
    [Serializable]
    public class MapGrid
    {

        public Texture2D texture;
        private int _cellDimension;
        public Sprite sprite;

        public IEnumerator ok;

        /// <summary>
        /// Wraps the cell dimension
        /// </summary>
        public int CellDimension
        {
            get
            {
                return _cellDimension;
            }
            set
            {
                cleanGrid();
                _cellDimension = value;
                stitchGrid(this._gridColor);
                loadSprite();
            }
        }

        private Color _gridColor;
        public Color GridColor
        {
            get
            {
                return _gridColor;
            }
            set
            {
                cleanGrid();
                _gridColor = value;
                stitchGrid(this._gridColor);
                loadSprite();
            }
        }


        public MapGrid()
        {

        }

        public void initialize(Map m, int dimensionalMultiplier)
        {
            this.texture = new Texture2D(m.Width * dimensionalMultiplier, m.Height * dimensionalMultiplier);
            cleanTexture();
            _gridColor = Color.black;
            _cellDimension = dimensionalMultiplier;
            stitchGrid(Color.black);
            loadSprite();
        }

        public void initialize(Map m, int dimensionalMultiplier,Color c)
        {
            this.texture = new Texture2D(m.Width * dimensionalMultiplier, m.Height * dimensionalMultiplier);
            cleanTexture();
            _gridColor = c;
            _cellDimension = dimensionalMultiplier;
            stitchGrid(Color.black);
            loadSprite();
        }

        /// <summary>
        /// Cleans the texture by setting the alpha of all pixels to 0.
        /// </summary>
        private void cleanTexture()
        {
            for (int i = 0; i < this.texture.width; i += 1)
            {
                for (int j = 0; j < this.texture.height; j += 1)
                {
                    this.texture.SetPixel(i, j, new Color(0, 0, 0, 0));
                    //Debug.Log("CLEAN: " + new Vector2(i, j));
                }
            }
        }

        /// <summary>
        /// Erases the old marks the grid left behind.
        /// </summary>
        private void cleanGrid()
        {
            for (int i = _cellDimension; i < this.texture.width; i += _cellDimension)
            {
                for (int j = 0; j < this.texture.height; j += 1)
                {
                    this.texture.SetPixel(i, j, new Color(0,0,0,0));
                    //Debug.Log("CLEAN: " + new Vector2(i, j));
                }
            }

            for (int j = _cellDimension; j < this.texture.height; j += _cellDimension)
            {
                for (int i = 0; i < this.texture.width; i += 1)
                {
                    this.texture.SetPixel(i, j, new Color(0, 0, 0, 0));
                    //Debug.Log("Stitch: " + new Vector2(i, j));
                }
            }

            this.texture.Apply(false);
        }

        /// <summary>
        /// Creates the new grid texture.
        /// </summary>
        /// <param name="gridColor"></param>
        private void stitchGrid(Color gridColor)
        {
            
            for(int i=_cellDimension; i < this.texture.width; i += _cellDimension)
            {
                for(int j = 0; j < this.texture.height; j += 1)
                {
                    this.texture.SetPixel(i, j, gridColor);
                    //Debug.Log("Stitch: " + new Vector2(i, j));
                }
            }
            

            
            for (int j = _cellDimension; j < this.texture.height; j += _cellDimension)
            {
                for (int i = 0; i < this.texture.width; i += 1)
                {
                    this.texture.SetPixel(i, j, gridColor);
                    //Debug.Log("Stitch: " + new Vector2(i, j));
                }
            }
            
            this.texture.Apply(false);
        }

        private void loadSprite()
        {
            loadSprite(new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0), 16f);
        }


        public void loadSprite( Rect RectInfo, Vector2 Pivots, float PixelsPerUnit)
        {
            if (this.texture == null)
            {
                Debug.Log("TEXTURE IS NULL???");
            }

            Sprite s = Sprite.Create(this.texture, RectInfo, Pivots, PixelsPerUnit);
            s.texture.filterMode = FilterMode.Point; https://docs.unity3d.com/ScriptReference/FilterMode.html
            this.sprite = s;
        }

    }
}
