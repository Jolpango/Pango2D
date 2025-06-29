using Microsoft.Xna.Framework;
using Pango2D.Core.Graphics;

namespace Pango2D.UI.Elements
{
    public class UIHeader1 : UIElement
    {
        public UIHeader1(FontRegistry fontRegistry) : base(fontRegistry)
        {
            FontSize = 36; // Default font size
            FontColor = Color.Black;
        }
    }
}
