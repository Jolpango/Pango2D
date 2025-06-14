using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pango2D.Utilities
{
    public static class StringTranslator
    {
        /// <summary>  
        /// Converts RGB color string to XNA Color. Format is like "rgb(255, 0, 0)" or "rgb(100%, 0%, 0%)".  
        /// </summary>  
        /// <param name="rgb"></param>  
        /// <returns></returns>  
        public static Color ColorFromRGB(string rgb)
        {
            if (string.IsNullOrEmpty(rgb))
            {
                throw new ArgumentNullException(nameof(rgb), "RGB string cannot be null or empty.");
            }

            rgb = rgb.Trim().ToLowerInvariant();

            if (!rgb.StartsWith("rgb(") || !rgb.EndsWith(")"))
            {
                throw new FormatException("RGB string must be in the format 'rgb(r, g, b)' or 'rgb(r%, g%, b%)'.");
            }

            string content = rgb.Substring(4, rgb.Length - 5);
            string[] parts = content.Split(',');

            if (parts.Length != 3)
            {
                throw new FormatException("RGB string must contain exactly three components.");
            }

            int[] values = new int[3];

            for (int i = 0; i < 3; i++)
            {
                string part = parts[i].Trim();

                if (part.EndsWith("%"))
                {
                    part = part.Substring(0, part.Length - 1);
                    if (!float.TryParse(part, NumberStyles.Float, CultureInfo.InvariantCulture, out float percentage) || percentage < 0 || percentage > 100)
                    {
                        throw new FormatException($"Invalid percentage value: {parts[i]}");
                    }
                    values[i] = (int)(percentage * 255 / 100);
                }
                else
                {
                    if (!int.TryParse(part, NumberStyles.Integer, CultureInfo.InvariantCulture, out int value) || value < 0 || value > 255)
                    {
                        throw new FormatException($"Invalid integer value: {parts[i]}");
                    }
                    values[i] = value;
                }
            }

            return new Color(values[0], values[1], values[2]);
        }

        /// <summary>
        /// Converts a string representation of a Point to a Point object.
        /// </summary>
        /// <param name="pointStr"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        public static Point PointFromString(string pointStr)
        {
            if (string.IsNullOrEmpty(pointStr))
            {
                throw new ArgumentNullException(nameof(pointStr), "Point string cannot be null or empty.");
            }
            string[] parts = pointStr.Split(',');
            if (parts.Length != 2)
            {
                throw new FormatException("Point string must be in the format 'x, y'.");
            }
            if (!int.TryParse(parts[0].Trim(), out int x) || !int.TryParse(parts[1].Trim(), out int y))
            {
                throw new FormatException("Invalid integer values for Point.");
            }
            return new Point(x, y);
        }
    }
}
