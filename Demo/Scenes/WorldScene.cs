using Demo.Content.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.Core.Input.Contracts;
using Pango2D.Core.Scenes;
using Pango2D.ECS;
using Pango2D.ECS.Components;
using Pango2D.ECS.Systems.RenderSystems;
using Pango2D.ECS.Systems.UpdateSystems;
using Pango2D.Graphics.Sprites;
using Pango2D.UI;
using Pango2D.UI.Views;
using Pango2D.Utilities;

namespace Demo.Scenes
{
    public class WorldScene : HybridScene
    {
        protected override void ConfigureUI(UIManager uiManager)
        {
            //var view = UIView.Create<ViewTest>(Services);
            //uiManager.AddView(view);
        }

        protected override World ConfigureWorld()
        {
            var content = Services.Get<ContentManager>();
            var world = new WorldBuilder(Services)
                .AddCoreSystems()
                .AddSystem(new MainCameraSystem())
                .AddSystem(new PlayerInputSystem(Services.Get<IInputProvider>()))
                .AddSystem(new LightingSystem())
                .Build();

            Entity player = new EntityBuilder(world)
                .AddComponent(new PlayerComponent())
                .AddComponent(new Transform())
                .AddComponent(new Velocity(Vector2.One * 100))
                .AddComponent(new Sprite(content.Load<Texture2D>("spinning-dagger")))
                .AddComponent(new SpriteAnimator(AsepriteLoader.Load("spinning-dagger.json")))
                .AddComponent(new Light() { Color = Color.White, Radius = 100f, Intensity = 1f })
                .AddComponent(new MainCameraTarget())
                .Build();
            //TODO add systerm to auto check for food or drinks.

            return world;
        }
    }
}
