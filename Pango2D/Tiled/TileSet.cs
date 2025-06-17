using Microsoft.Xna.Framework.Graphics;


namespace Pango2D.Tiled
{
    public class TileSet
    {
        public string Name { get; set; }
        public Texture2D Texture { get; set; }
        public int FirstGid { get; set; }
        public int TileWidth { get; set; }
        public int TileHeight { get; set; }
        public int TileCount { get; set; }
        public int Spacing { get; set; }
        public int Margin { get; set; }
    }
}
