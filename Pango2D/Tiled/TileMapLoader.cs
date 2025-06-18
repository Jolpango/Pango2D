using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.ECS;
using Pango2D.ECS.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pango2D.Tiled
{
    public class TileMapLoader
    {
        public static TileMap LoadTileMap(string tileMapPath, ContentManager content)
        {
            var rawData = TiledData.FromJson(System.IO.File.ReadAllText(tileMapPath));
            var map = new TileMap()
            {
                Width = rawData.Width,
                Height = rawData.Height,
                TileWidth = rawData.Tilewidth,
                TileHeight = rawData.Tileheight,
            };

            foreach (var tilesetRef in rawData.Tilesets)
            {
                var tilesetPath = tilesetRef.Source;
                var tilesetFullPath = ResolveTilesetSource(tilesetPath, tileMapPath);
                var tilesetData = TilesetData.FromJson(System.IO.File.ReadAllText(tilesetFullPath));
                var texturePath = ResolveTilesetTexturePath(tilesetFullPath, tilesetData.Image);
                var tileset = new TileSet()
                {
                    Name = tilesetData.Name,
                    FirstGid = tilesetRef.Firstgid,
                    TileWidth = tilesetData.Tilewidth,
                    TileHeight = tilesetData.Tileheight,
                    TileCount = tilesetData.Tilecount,
                    Texture = content.Load<Texture2D>(texturePath),
                    Spacing = tilesetData.Spacing,
                    Margin = tilesetData.Margin
                };
                map.TileSets.Add(tileset);
            }

            foreach (var layerData in rawData.Layers.Where(l => l.Type == "tilelayer"))
            {
                var width = layerData.Width.Value;
                var height = layerData.Height.Value;

                var tiles = new int[height, width];
                for (int y = 0; y < height; y++)
                    for (int x = 0; x < width; x++)
                    {
                        int flatIndex = y * width + x;
                        tiles[y, x] = layerData.Data[flatIndex]; // handle flipping flags if needed
                    }
                var layer = new TileLayer(layerData.Name, width, height, tiles)
                {
                    Opacity = layerData.Opacity,
                    IsVisible = layerData.Visible
                };
                map.Layers.Add(layer);
            }

            return map;
        }

        private static string ResolveTilesetSource(string tileSetPath, string tileMapPath)
        {
            // Assuming the source is relative to the tile map path
            var directory = System.IO.Path.GetDirectoryName(tileMapPath);
            var path = System.IO.Path.Combine(directory, tileSetPath);
            return path;
        }

        private static string ResolveTilesetTexturePath(string tilesetPath, string texture)
        {
            var directory = System.IO.Path.GetDirectoryName(tilesetPath);
            // remove the root "Contnet" directory from the path
            if (directory.StartsWith("Content", StringComparison.OrdinalIgnoreCase))
            {
                directory = directory["Content/".Length..].TrimStart(System.IO.Path.DirectorySeparatorChar);
            }
            var path = System.IO.Path.Combine(directory, texture);
            var pathWithoutExtension = System.IO.Path.ChangeExtension(path, null);
            return pathWithoutExtension;
        }
    }
}
