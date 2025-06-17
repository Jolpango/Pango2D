using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.Core.Contracts;
using Pango2D.Core.Graphics;
using Pango2D.ECS.Systems.Contracts;
using Pango2D.Extensions;
using Pango2D.Tiled;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pango2D.ECS.Systems.RenderSystems
{
    public class TileMapRenderer : IDrawSystem
    {
        public RenderPhase RenderPhase { get; set; } = RenderPhase.World;
        public World World { get; set; }
        private ICameraService cameraService;
        public void Initialize()
        {
            cameraService = World.Services.Get<ICameraService>();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var tileMaps = World.Query<TileMap>().ToList();
            if (tileMaps.Count == 0)
                return;

            var viewMatrix = cameraService.GetViewMatrix();
            foreach(var (entity, tileMap) in tileMaps)
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
                                Rectangle sourceRectangle = new Rectangle(
                                    tileX * (tileSet.TileWidth + tileSet.Spacing) + tileSet.Margin,
                                    tileY * (tileSet.TileHeight + tileSet.Spacing) + tileSet.Margin,
                                    tileSet.TileWidth,
                                    tileSet.TileHeight);
                                Vector2 position = new Vector2(j * tileMap.TileWidth, i * tileMap.TileHeight);
                                spriteBatch.Draw(
                                    tileSet.Texture,
                                    position,
                                    sourceRectangle,
                                    Color.White * layer.Opacity,
                                    0f,
                                    Vector2.Zero,// maybe center the tile?
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
