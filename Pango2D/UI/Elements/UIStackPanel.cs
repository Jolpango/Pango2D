using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.Core.Graphics;
using Pango2D.UI.Elements.Contracts;
using System;

namespace Pango2D.UI.Elements
{
    public enum Orientation
    {
        Horizontal,
        Vertical
    }
    public class UIStackPanel(FontRegistry fontRegistry) : UIElement(fontRegistry), ILayoutContainer
    {
        private Orientation orientation = Orientation.Vertical;
        private int gap = 0;
        public int Gap
        {
            get => gap;
            set
            {
                if (gap != value)
                {
                    gap = value;
                    InvalidateLayout();
                }
            }
        }
        public Orientation Orientation
        {
            get => orientation;
            set
            {
                if (orientation != value)
                {
                    orientation = value;
                    InvalidateLayout();
                }
            }
        }
        public override void Measure()
        {
            int width = 0;
            int height = 0;

            foreach (var child in Children)
            {
                child.Measure();
                if (orientation == Orientation.Horizontal)
                {
                    width += child.Size.X + gap;
                    height = Math.Max(height, child.Size.Y);
                }
                else
                {
                    height += child.Size.Y + gap;
                    width = Math.Max(width, child.Size.X);
                }
            }
            Size = new Point(width + Padding.X * 2, height + Padding.Y * 2);
        }

        public override void Arrange(Point offset)
        {
            Position = CalculateAnchoredPosition(offset, Size, Anchor, new Point(1920, 1080));
            Point currentPosition = Position + Padding + offset;
            foreach (var child in Children)
            {
                child.Arrange(currentPosition);
                if (orientation == Orientation.Horizontal)
                {
                    currentPosition.X += child.Size.X + gap;
                }
                else
                {
                    currentPosition.Y += child.Size.Y + gap;
                }
            }
            isLayoutDirty = false;
        }
    }
}
