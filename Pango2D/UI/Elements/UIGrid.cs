using Microsoft.Xna.Framework;
using Pango2D.Core.Graphics;
using Pango2D.UI.Elements.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Pango2D.UI.Elements
{
    public class UIGrid(GameWindow gameWindow, FontRegistry fontRegistry) : UIElement(gameWindow, fontRegistry), ILayoutContainer
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        public Point? CellSize { get; set; }
        public override void Measure()
        {
            foreach (var child in Children)
                child.Measure();

            int cellWidth = CellSize?.X ?? Children.Max(c => c.Size.X);
            int cellHeight = CellSize?.Y ?? Children.Max(c => c.Size.Y);

            Size = new Point(
                Columns * cellWidth + Padding.X * 2,
                Rows * cellHeight + Padding.Y * 2
            );
        }
        public override void Arrange(Point offset)
        {
            Position = offset;

            int cellWidth = CellSize?.X ?? Children.Max(c => c.Size.X);
            int cellHeight = CellSize?.Y ?? Children.Max(c => c.Size.Y);

            for (int i = 0; i < Children.Count; i++)
            {
                int row = i / Columns;
                int col = i % Columns;

                var child = Children[i];
                var childOffset = new Point(
                    Position.X + Padding.X + col * cellWidth,
                    Position.Y + Padding.Y + row * cellHeight
                );

                child.Arrange(childOffset);
            }

            isLayoutDirty = false;
        }
    }
}
