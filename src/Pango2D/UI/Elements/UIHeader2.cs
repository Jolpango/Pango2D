using Microsoft.Xna.Framework;
using Pango2D.Core.Graphics;

namespace Pango2D.UI.Elements
{
    public class UIHeader2 : UIElement
    {
        public UIHeader2(GameWindow gameWindow, FontRegistry fontRegistry) : base(gameWindow, fontRegistry)
        {
            FontSize = 24; // Default font size
            FontColor = Color.Black;
        }
    }
}
