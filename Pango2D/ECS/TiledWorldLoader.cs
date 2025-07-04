using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Pango2D.ECS.Components;
using Pango2D.Tiled;
using System;

namespace Pango2D.ECS
{
    public static class TiledWorldLoader
    {
        public static Entity LoadMap(string path, World world, int scale = 1, Func<TiledObject, Entity?> prefabFactory = null)
        {

            var map = TileMapLoader.LoadTileMap(path, world.Services.Get<ContentManager>(), scale);

            foreach (var layer in map.ObjectLayers)
            {
                foreach (var obj in layer.Objects)
                {
                    obj.X *= scale;
                    obj.Y *= scale;
                    obj.Width *= scale;
                    obj.Height *= scale;
                    if (obj.Type == "collision")
                    {
                        new EntityBuilder(world)
                            .AddComponent(new Transform { Position = new Vector2(obj.X, obj.Y) })
                            .AddComponent(new Collider
                            {
                                Bounds = new Rectangle(0, 0, (int)obj.Width, (int)obj.Height),
                                IsStatic = true
                            })
                            .Build();
                    }
                    else
                    {
                        prefabFactory?.Invoke(obj);
                    }
                }
            }
            var mapEntity = new EntityBuilder(world)
                .AddComponent(map)
                .Build();
            return mapEntity;
        }
    }
}
