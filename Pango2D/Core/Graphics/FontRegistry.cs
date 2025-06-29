using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.UI.Elements;

namespace Pango2D.Core.Graphics
{
    public class FontRegistry : AssetRegistry<SpriteFont>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FontRegistry"/> class with a default font family path.
        /// Requires fonts to be named in the format "Default-<size>" where <size> is the font size (e.g., "Default-8", "Default-12", etc.).
        /// Sizes required: 8, 12, 16, 24, and 36.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="defaultFontFamilyPath"></param>
        /// <exception cref="System.ArgumentException"></exception>
        public FontRegistry(ContentManager content, string defaultFontFolder) : base(content)
        {
            if (string.IsNullOrEmpty(defaultFontFolder))
                throw new System.ArgumentException("Default font path cannot be null or empty.", nameof(defaultFontFolder));

            AddFontCollection("Default", defaultFontFolder);
        }

        /// <summary>
        /// Adds a font family to the registry.
        /// </summary>
        /// <param name="familyName"></param>
        /// <param name="path"></param>
        /// <exception cref="System.ArgumentException"></exception>
        public void AddFontCollection(string fontName, string folderPath)
        {
            if (string.IsNullOrEmpty(fontName))
                throw new System.ArgumentException("Family name cannot be null or empty.", nameof(fontName));
            if (string.IsNullOrEmpty(folderPath))
                throw new System.ArgumentException("Path cannot be null or empty.", nameof(folderPath));
            int[] sizes = [8, 12, 14, 16, 24, 36, 48, 72];
            FontStyle[] styles = [FontStyle.Regular, FontStyle.Bold, FontStyle.Italic];
            foreach (var size in sizes)
            {
                foreach (var style in styles)
                {
                    var styleString = style.ToString();
                    try
                    {
                        Add($"{fontName}-{styleString}-{size}", content.Load<SpriteFont>($"{folderPath}/{styleString}/{fontName}-{style.ToString()}-{size}"));
                    }
                    catch { }
                }
            }
        }

        public SpriteFont Get(string name, int size, FontStyle style)
        {
            return Get($"{name}-{style}-{size}");
        }
    }
}
