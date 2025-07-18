using Microsoft.Xna.Framework;
using System;

namespace Pango2D.Core.Services
{
    public class ViewportService
    {
        public int VirtualWidth { get; private set; } = 1920;
        public int VirtualHeight { get; private set; } = 1080;

        public int WindowWidth { get; private set; }
        public int WindowHeight { get; private set; }

        public float Scale => Math.Min(
            (float)WindowWidth / VirtualWidth,
            (float)WindowHeight / VirtualHeight);

        public Rectangle DestinationRectangle => new Rectangle(
            (WindowWidth - (int)(VirtualWidth * Scale)) / 2,
            (WindowHeight - (int)(VirtualHeight * Scale)) / 2,
            (int)(VirtualWidth * Scale),
            (int)(VirtualHeight * Scale)
        );

        public void UpdateWindowSize(int width, int height)
        {
            WindowWidth = width;
            WindowHeight = height;
        }
    }

}
