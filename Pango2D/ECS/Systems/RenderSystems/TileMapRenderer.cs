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
            foreach (var (entity, tileMap) in World.Query<TileMap>())
            {
                foreach (var layer in tileMap.Layers)
                {
                    if (!layer.IsVisible) continue;
                    float layerDepth = 1;
                    switch (layer.Name)
                    {
                        case "Ground": layerDepth = LayerDepths.Ground; break;
                        case "Decoration": layerDepth = LayerDepths.Decoration; break;
                        case "Foreground": layerDepth = LayerDepths.Foreground; break;
                    }
                    foreach(var tile in layer.Tiles)
                    {
                        tile.Update(gameTime);
                        spriteBatch.Draw(
                            tile.TileSet.Texture,
                            tile.DestinationRectangle,
                            tile.GetSourceRectangle(),
                            Color.White * layer.Opacity,
                            0f,
                            Vector2.Zero,
                            SpriteEffects.None,
                            layerDepth);
                    }
                    //for (int i = 0; i < layer.Height; i++)
                    //{
                    //    for (int j = 0; j < layer.Width; j++)
                    //    {
                    //        int tileId = layer.TileIds[i, j];
                    //        if (tileId == 0) continue;
                    //        var tileSet = tileMap.TileSets.Find(ts => ts.FirstGid <= tileId && ts.FirstGid + ts.TileCount > tileId);
                    //        if (tileSet != null)
                    //        {
                    //            int localTileId = tileId - tileSet.FirstGid;
                    //            int tilesPerRow = tileSet.Texture.Width / (tileSet.TileWidth + tileSet.Spacing);
                    //            int tileX = localTileId % tilesPerRow;
                    //            int tileY = localTileId / tilesPerRow;

                    //            Rectangle sourceRectangle = new(
                    //                tileX * (tileSet.TileWidth + tileSet.Spacing) + tileSet.Margin,
                    //                tileY * (tileSet.TileHeight + tileSet.Spacing) + tileSet.Margin,
                    //                tileSet.TileWidth,
                    //                tileSet.TileHeight);

                    //            int destX = j * tileMap.TileWidthScaled;
                    //            int destY = i * tileMap.TileHeightScaled;
                    //            Rectangle destRectangle = new(destX, destY, tileMap.TileWidthScaled, tileMap.TileHeightScaled);

                    //            spriteBatch.Draw(
                    //                tileSet.Texture,
                    //                destRectangle,
                    //                sourceRectangle,
                    //                Color.White * layer.Opacity,
                    //                0f,
                    //                Vector2.Zero,
                    //                SpriteEffects.None,
                    //                layerDepth);
                    //        }
                    //    }
                    //}
                }
            }
        }
    }
}
