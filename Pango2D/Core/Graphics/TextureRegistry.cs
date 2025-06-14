using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pango2D.Core.Graphics
{
    public class TextureRegistry : AssetRegistry<Texture2D>
    {
        public TextureRegistry(ContentManager content) : base(content)
        {
        }
    }
}
