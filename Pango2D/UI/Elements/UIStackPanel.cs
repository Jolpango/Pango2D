using Microsoft.Xna.Framework;
using System;

namespace Pango2D.UI.Elements
{
    public enum Orientation
    {
        Horizontal,
        Vertical
    }
    public class UIStackPanel : UIElement
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
                    width += child.DesiredSize.X + gap;
                    height = Math.Max(height, child.DesiredSize.Y);
                }
                else
                {
                    height += child.DesiredSize.Y + gap;
                    width = Math.Max(width, child.DesiredSize.X);
                }
            }
            DesiredSize = new Point(width + Padding.X * 2, height + Padding.Y * 2);
        }

        public override void Arrange(Point offset)
        {
            Point currentPosition = new Point(offset.X + Padding.X, offset.Y + Padding.Y);
            foreach (var child in Children)
            {
                child.Arrange(currentPosition);
                if (orientation == Orientation.Horizontal)
                {
                    currentPosition.X += child.DesiredSize.X + gap;
                }
                else
                {
                    currentPosition.Y += child.DesiredSize.Y + gap;
                }
            }
            isLayoutDirty = false;
        }
    }
}
