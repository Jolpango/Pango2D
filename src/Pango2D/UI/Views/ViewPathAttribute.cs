using System;

namespace Pango2D.UI.Views
{
    public class ViewPathAttribute : Attribute
    {
        public string Path { get; }
        public ViewPathAttribute(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("Path cannot be null or empty.", nameof(path));
            Path = path;
        }
        public override string ToString()
        {
            return $"ViewPath: {Path}";
        }
    }
}