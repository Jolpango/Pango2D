using Demo.Content.UI;
using Pango2D.Core.Scenes;
using Pango2D.UI;
using Pango2D.UI.Views;
using System;
namespace Demo.Scenes
{
    public class MainMenu : UIScene
    {
        protected override void ConfigureUI(UIManager uIManager)
        {
            ViewTest view = UIView.Create<ViewTest>(Services);
            uIManager.AddView(view);
        }
    }
}
