using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pango2D.Tiled
{
    public class TileLayer(string name, int width, int height, int[,] tileIds)
    {
        public string Name { get; } = name;
        public int Width { get; } = width;
        public int Height { get; } = height;
        public bool IsVisible { get; set; } = true;
        public float Opacity { get; set; } = 1.0f;
        public int[,] TileIds { get; } = tileIds;
    }
}
