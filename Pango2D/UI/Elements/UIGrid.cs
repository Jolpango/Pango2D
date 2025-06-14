using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pango2D.UI.Elements
{
    public class UIGrid : UIElement
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        public float? CellWidth { get; set; } = null;
        public float? CellHeight { get; set; } = null;
        public UIGrid()
        {
        }
        public override void Arrange(Point offset)
        {
            base.Arrange(offset);
        }
    }
}
