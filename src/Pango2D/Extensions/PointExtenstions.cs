using Microsoft.Xna.Framework;
namespace Pango2D.Extensions
{
    public static class PointExtenstions
    {
        public static Point Multiply(this Point point, int scalar)
        {
            return new Point(point.X * scalar, point.Y * scalar);
        }
    }
}
