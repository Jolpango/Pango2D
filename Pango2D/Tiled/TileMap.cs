using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.ECS.Components.Contracts;
using System.Collections.Generic;

namespace Pango2D.Tiled
{
    public class TileMap : IComponent
    {
        public int Width { get; init; }
        public int Height { get; init; }
        public int TileWidth { get; init; }
        public int TileHeight { get; init; }
        public List<TileLayer> Layers { get; } = [];
        public List<ObjectLayer> ObjectLayers { get; } = [];
        public List<TileSet> TileSets { get; } = [];
        public int Scale { get; set; }
        public int TileWidthScaled { get; set; }
        public int TileHeightScaled { get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var layer in Layers)
            {
                for(int i = 0; i < layer.Height; i++)
                {
                    for(int j = 0; j < layer.Width; j++)
                    {
                        int tileId = layer.TileIds[i, j];
                        if (tileId == 0) continue; // Skip empty tiles
                        var tileSet = TileSets.Find(ts => ts.FirstGid <= tileId && ts.FirstGid + ts.TileCount > tileId);
                        if (tileSet != null)
                        {
                            int localTileId = tileId - tileSet.FirstGid;
                            int tileX = localTileId % (tileSet.Texture.Width / (tileSet.TileWidth + tileSet.Spacing));
                            int tileY = localTileId / (tileSet.Texture.Width / (tileSet.TileWidth + tileSet.Spacing));
                            Rectangle sourceRectangle = new(
                                tileX * (tileSet.TileWidth + tileSet.Spacing) + tileSet.Margin,
                                tileY * (tileSet.TileHeight + tileSet.Spacing) + tileSet.Margin,
                                tileSet.TileWidth,
                                tileSet.TileHeight);
                            Vector2 position = new(j * TileWidth, i * TileHeight);
                            Vector2 origin = new Vector2(tileSet.TileWidth / 2f, tileSet.TileHeight / 2f);
                            spriteBatch.Draw(tileSet.Texture, position, sourceRectangle, Color.White, 0f, origin, 1f, SpriteEffects.None, 1f);
                        }
                    }
                }
            }
        }
    }
}
