using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.Core.Graphics;
using Pango2D.ECS.Systems.Contracts;
using Pango2D.Tiled;
using System.Linq;

namespace Pango2D.ECS.Systems.RenderSystems
{
    public class TileMapRenderer : IDrawSystem
    {
        public RenderPhase RenderPhase { get; set; } = RenderPhase.World;
        public World World { get; set; }
        public void Initialize()
        {
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach(var (entity, tileMap) in World.Query<TileMap>())
            {
                foreach (var layer in tileMap.Layers)
                {
                    if (layer.IsVisible == false) continue; // Skip invisible layers
                    float layerDepth = 1;
                    switch (layer.Name)
                    {
                        case "Ground":
                            layerDepth = LayerDepths.Ground;
                            break;
                        case "Decoration":
                            layerDepth = LayerDepths.Decoration;
                            break;
                        case "Foreground":
                            layerDepth = LayerDepths.Foreground;
                            break;
                    }
                    for (int i = 0; i < layer.Height; i++)
                    {
                        for (int j = 0; j < layer.Width; j++)
                        {
                            int tileId = layer.TileIds[i, j];
                            if (tileId == 0) continue; // Skip empty tiles
                            var tileSet = tileMap.TileSets.Find(ts => ts.FirstGid <= tileId && ts.FirstGid + ts.TileCount > tileId);
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
                                Vector2 position = new(j * tileMap.TileWidth, i * tileMap.TileHeight);
                                Vector2 origin = new Vector2(tileSet.TileWidth / 2f, tileSet.TileHeight / 2f);
                                spriteBatch.Draw(
                                    tileSet.Texture,
                                    position + origin,
                                    sourceRectangle,
                                    Color.White * layer.Opacity,
                                    0f,
                                    origin,
                                    Vector2.One,
                                    SpriteEffects.None,
                                    layerDepth);
                            }
                        }
                    }
                }
            }
        }
    }
}
