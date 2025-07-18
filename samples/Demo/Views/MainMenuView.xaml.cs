using Demo.Scenes;
using Pango2D.Core.Scenes;
using Pango2D.UI.Views;

namespace Demo.Views
{
    [ViewPath("Views/MainMenuView.xaml")]
    public partial class MainMenuView : UIView
    {
        public void OnClick(object sender)
        {
            Services.Get<SceneManager>().ChangeScene(new WorldScene());
        }
        public override void OnLoaded()
        {
            base.OnLoaded();
        }
    }
}
