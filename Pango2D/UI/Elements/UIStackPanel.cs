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
    public class UIStackPanel(GameWindow gameWindow, FontRegistry fontRegistry) : UIElement(gameWindow, fontRegistry), ILayoutContainer
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
            Size = new Point(Math.Max(Size.X, MinSize.X), Math.Max(Size.Y, MinSize.Y));
        }

        public override void Arrange(Point offset)
        {
            Position = CalculateAnchoredPosition(offset, Size, Anchor, new Point(gameWindow.ClientBounds.Width, gameWindow.ClientBounds.Height));
            Point currentPosition = Position + Padding + offset;
            foreach (var child in Children)
            {
                child.Arrange(currentPosition);
                if (orientation == Orientation.Horizontal)
                {
                    if (child.AlignSelf == ElementAlignment.Center)
                    {
                        var pos = child.Position;
                        pos.Y += (Size.Y - child.Size.Y) / 2;
                        child.Position = pos;
                    }
                    else if (child.AlignSelf == ElementAlignment.End)
                    {
                        var pos = child.Position;
                        pos.Y += Size.Y - child.Size.Y - Padding.Y * 2;
                        child.Position = pos;
                    }
                    else if (child.AlignSelf == ElementAlignment.Start)
                    {
                        var pos = child.Position;
                        pos.Y += 0;
                        child.Position = pos;
                    }
                    currentPosition.X += child.Size.X + gap;
                }
                else
                {
                    if (child.AlignSelf == ElementAlignment.Center)
                    {
                        var pos = child.Position;
                        pos.X += (Size.X - child.Size.X) / 2;
                        child.Position = pos;
                    }
                    else if (child.AlignSelf == ElementAlignment.End)
                    {
                        var pos = child.Position;
                        pos.X += Size.X - child.Size.X - Padding.X * 2;
                        child.Position = pos;
                    }
                    else if (child.AlignSelf == ElementAlignment.Start)
                    {
                        var pos = child.Position;
                        pos.X += 0;
                        child.Position = pos;
                    }
                    currentPosition.Y += child.Size.Y + gap;
                }
            }
            isLayoutDirty = false;
        }
    }
}
