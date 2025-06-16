using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pango2D.Tiled
{
    public class TileLayer
    {
        public string Name { get; }
        public int Width { get; }
        public int Height { get; }
        public int[,] TileIds { get; }

        public TileLayer(string name, int width, int height, int[,] tileIds)
        {
            Name = name;
            Width = width;
            Height = height;
            TileIds = tileIds;
        }
    }
}
