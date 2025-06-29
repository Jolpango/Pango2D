using Microsoft.Xna.Framework;
using Pango2D.Core.Graphics;

namespace Pango2D.UI.Elements
{
    public class UIHeader3 : UIElement
    {
        public UIHeader3(FontRegistry fontRegistry) : base(fontRegistry)
        {
            FontSize = 16; // Default font size
            FontColor = Color.Black;
        }
    }
}
