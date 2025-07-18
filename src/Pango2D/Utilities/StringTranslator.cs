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

            if (!rgb.StartsWith("rgb(") && !rgb.StartsWith("rgba(") || !rgb.EndsWith(")"))
            {
                throw new FormatException("RGB(A) string must be in the format 'rgb(r, g, b)' or 'rgba(r, g, b, a)' or with percentages.");
            }

            string content = rgb.Substring(rgb.IndexOf('(') + 1, rgb.Length - rgb.IndexOf('(') - 2);
            string[] parts = content.Split(',');

            if (parts.Length < 3 || parts.Length > 4)
            {
                throw new FormatException("RGB(A) string must contain three or four components.");
            }

            int[] values = new int[4];
            values[3] = 255; // Default alpha value  

            for (int i = 0; i < parts.Length; i++)
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

            return new Color(values[0], values[1], values[2], values[3]);
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
