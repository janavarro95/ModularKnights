using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Maps.Tiles
{
    public class TileLayer
    {
        public Tile[,] tiles;

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

        public TileLayer()
        {
            _width = 10;
            _height = 10;
        }

        public TileLayer(int Width, int Height)
        {
            this._width = Width;
            this._height = Height;

            this.tiles = new Tile[_width, _height];
        }

        public void resizeLayer(int NewWidth, int NewHeight)
        {
            Tile[,] newArray = new Tile[NewWidth, NewHeight];

            for (int j = 0; j < NewHeight; j++)
            {
                for (int i = 0; i < NewWidth; i++)
                {
                    if (j > _height || i > _width) break;
                    else
                    {
                        newArray[i, j] = tiles[i, j];
                    }
                }
            }
            this.tiles = newArray;

        }
    }
}
