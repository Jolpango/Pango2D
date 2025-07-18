using Microsoft.Xna.Framework;
using Pango2D.Core.Graphics;

namespace Pango2D.UI.Elements
{
    public class UIParagraph : UIElement
    {
        public UIParagraph(GameWindow gameWindow, FontRegistry fontRegistry) : base(gameWindow, fontRegistry)
        {
            FontSize = 12; // Default font size
            FontColor = Color.Black;
        }
    }
}
