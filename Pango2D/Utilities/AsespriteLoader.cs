using System;
using System.Collections.Generic;

namespace Pango2D.Utilities
{
    using Microsoft.Xna.Framework;
    using Pango2D.Graphics.Sprites;
    using System.IO;
    using System.Text.Json;

    public static class AsepriteLoader
    {
        public static string RootDirectory { get; set; } = "Content";
        public static Dictionary<string, SpriteAnimation> Load(string jsonPath)
        {
            var json = File.ReadAllText($"{RootDirectory}/{jsonPath}");
            var doc = JsonDocument.Parse(json);

            var root = doc.RootElement;
            var frameArray = root.GetProperty("frames");
            var meta = root.GetProperty("meta");
            var tags = meta.GetProperty("frameTags");

            // Step 1: Parse frames
            var allFrames = new List<SpriteAnimationFrame>();
            foreach (var frameEl in frameArray.EnumerateArray())
            {
                var frameRect = frameEl.GetProperty("frame");
                var x = frameRect.GetProperty("x").GetInt32();
                var y = frameRect.GetProperty("y").GetInt32();
                var w = frameRect.GetProperty("w").GetInt32();
                var h = frameRect.GetProperty("h").GetInt32();
                var duration = frameEl.GetProperty("duration").GetInt32() / 1000f;

                allFrames.Add(new SpriteAnimationFrame(new Rectangle(x, y, w, h), duration));
            }

            // Step 2: Parse animations (by tag)
            var animations = new Dictionary<string, SpriteAnimation>();
            foreach (var tag in tags.EnumerateArray())
            {
                string name = tag.GetProperty("name").GetString();
                int from = tag.GetProperty("from").GetInt32();
                int to = tag.GetProperty("to").GetInt32();
                string direction = tag.GetProperty("direction").GetString();

                var animFrames = allFrames.GetRange(from, to - from + 1).ToArray();

                if (direction == "reverse")
                    Array.Reverse(animFrames);

                animations[name] = new SpriteAnimation(animFrames, looping: true);
            }

            return animations;
        }
    }

}
