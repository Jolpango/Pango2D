using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.Core.Services;
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
        public string FPSText { get => "FPS: " + MathF.Round(Services?.Get<DebugService>().FPS ?? 0, 2); }
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
    }
}
