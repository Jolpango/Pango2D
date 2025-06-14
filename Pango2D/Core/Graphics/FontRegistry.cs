using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pango2D.Core.Graphics
{
    public class FontRegistry : AssetRegistry<SpriteFont>
    {
        public FontRegistry(ContentManager content, string defaultFontPath) : base(content)
        {
            if (string.IsNullOrEmpty(defaultFontPath))
                throw new System.ArgumentException("Default font path cannot be null or empty.", nameof(defaultFontPath));
            // Preload default font
            if (!entries.ContainsKey("DefaultFont"))
            {
                entries.Add("DefaultFont", content.Load<SpriteFont>(defaultFontPath));
            }
        }
    }
}
