using Demo.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.Core.Scenes;
using Pango2D.UI.Views;

namespace Demo.Views
{
    [ViewPath("Views/MainMenuView.xaml")]
    public partial class MainMenuView : UIView
    {
        private int counter = 0;
        public string Title { get; set; } = "Start";
        public string Counter => $"{counter}";
        public Point Padding { get; set; } = new Point(10, 20);
        public Texture2D BackgroundImage { get; set; } = null;
        public void OnClick(object sender)
        {
            Services.Get<SceneManager>().ChangeScene(new WorldScene());
        }
        public override void OnLoaded()
        {
            base.OnLoaded();
            BackgroundImage = Services.Get<ContentManager>().Load<Texture2D>("spinning-dagger");
        }
    }
}
