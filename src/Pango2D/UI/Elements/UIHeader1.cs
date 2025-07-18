using Microsoft.Xna.Framework;
using Pango2D.Core.Graphics;

namespace Pango2D.UI.Elements
{
    public class UIHeader1 : UIElement
    {
        public UIHeader1(GameWindow gameWindow, FontRegistry fontRegistry) : base(gameWindow, fontRegistry)
        {
            FontSize = 36; // Default font size
            FontColor = Color.Black;
        }
    }
}
