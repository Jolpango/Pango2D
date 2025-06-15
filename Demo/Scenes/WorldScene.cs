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
            var view = UIView.Create<ViewTest>(Services);
            uiManager.AddView(view);
        }

        protected override World ConfigureWorld()
        {
            var content = Services.Get<ContentManager>();
            var world = new WorldBuilder(Services)
                .AddCoreSystems()
                .AddSystem(new MainCameraSystem())
                .AddSystem(new PlayerInputSystem(Services.Get<IInputProvider>()))
                .AddSystem(new LightingRenderSystem())
                .AddSystem(new LightingCompositeSystem())
                .AddSystem(new LightCollectionSystem())
                .Build();

            Entity background = new EntityBuilder(world)
                .AddComponent(new Transform())
                .AddComponent(new Sprite(content.Load<Texture2D>("background")))
                .Build();
            Entity player = new EntityBuilder(world)
                .AddComponent(new PlayerComponent())
                .AddComponent(new Transform())
                .AddComponent(new Velocity(Vector2.One * 100))
                .AddComponent(new Sprite(content.Load<Texture2D>("spinning-dagger")))
                .AddComponent(new SpriteAnimator(AsepriteLoader.Load("spinning-dagger.json")))
                .AddComponent(new Light() { Color = Color.Red, Radius = 1, Intensity = 1f })
                .AddComponent(new MainCameraTarget())
                .Build();
            Entity light = new EntityBuilder(world)
                .AddComponent(new Transform() { Position = new Vector2(100, 100)})
                .AddComponent(new Light() { Color = Color.White, Radius = 10f, Intensity = 1f })
                .Build();
            Entity light2 = new EntityBuilder(world)
                .AddComponent(new Transform() { Position = new Vector2(500, 0) })
                .AddComponent(new Light() { Color = Color.Red, Radius = 2f, Intensity = 0.5f })
                .Build();
            //TODO add systerm to auto check for food or drinks.

            return world;
        }
    }
}
