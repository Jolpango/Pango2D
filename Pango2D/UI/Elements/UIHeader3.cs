using Microsoft.Xna.Framework;
using Pango2D.Core.Graphics;

namespace Pango2D.UI.Elements
{
    public class UIHeader3 : UIElement
    {
        public UIHeader3(GameWindow gameWindow, FontRegistry fontRegistry) : base(gameWindow, fontRegistry)
        {
            FontSize = 16; // Default font size
            FontColor = Color.Black;
        }
    }
}
