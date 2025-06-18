using Microsoft.Xna.Framework;
using Pango2D.ECS;
using Pango2D.UI.Views;
using System;

namespace Demo.Views
{
    [ViewPath("Views/WorldView.xaml")]
    public partial class WorldView : UIView
    {
        private int counter = 0;
        public string Title { get; set; } = "UI With binds: 0";
        public string Counter => $"{counter}";
        public Point Padding { get; set; } = new Point(10, 20);
        private World world;
        public void Button1_Click()
        {
            Padding = new Point(Padding.X + 1, Padding.Y);
            Title = $"UI With binds: {counter}";
            counter++;
        }
        public override void OnLoaded()
        {
            base.OnLoaded();
        }

        public void SetWorld(World world)
        {
            this.world = world;
        }
    }
}
