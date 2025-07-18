using Demo.Views;
using Pango2D.Core.Scenes;
using Pango2D.Core.Services;
using Pango2D.UI;
using Pango2D.UI.Views;

namespace Demo.Scenes
{
    public class MainMenu : UIScene
    {
        private MainMenuView mainMenuView;
        protected override void ConfigureUI(UIManager uIManager)
        {
            mainMenuView = UIView.Create<MainMenuView>(Services);
            uIManager.AddView(mainMenuView);
        }
        public override void Initialize(GameServices services)
        {
            base.Initialize(services);
        }
    }
}
