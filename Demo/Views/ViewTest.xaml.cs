using Microsoft.Xna.Framework;
using Pango2D.UI.Views;

namespace Demo.Content.UI
{
    [ViewPath("Views/ViewTest.xaml")]
    public partial class ViewTest : UIView
    {
        private int counter = 0;
        public string Title { get; set; } = "UI With binds: 0";
        public string Counter => $"{counter}";
        public Point Padding { get; set; } = new Point(10, 20);
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
