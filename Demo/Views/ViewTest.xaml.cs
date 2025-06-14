using Microsoft.Xna.Framework;
using Pango2D.Core.Graphics;
using Pango2D.UI;
using Pango2D.UI.Views;
using System;

namespace Demo.Content.UI
{
    [ViewPath("Views/ViewTest.xaml")]
    public partial class ViewTest : UIView
    {
        private int counter = 0;
        public string Title { get; set; } = "Tjena";
        public string Counter => $"{counter}";
        public Point Padding { get; set; } = new Point(10, 20);
        public void OnClick()
        {
            System.Diagnostics.Debug.WriteLine("Test");
            Padding = new Point(Padding.X + 1, Padding.Y);
            counter++;
        }
        public void TestMethod()
        {
            Title = "Ny titel";
        }
        public override void OnLoaded()
        {
            base.OnLoaded();
        }
    }
}
