using Demo.Content;
using Demo.Views;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Pango2D.Core.Audio;
using Pango2D.Core.Graphics;
using Pango2D.Core.Scenes;
using Pango2D.ECS;
using Pango2D.ECS.Components;
using Pango2D.ECS.Systems.RenderSystems;
using Pango2D.ECS.Systems.UpdateSystems;
using Pango2D.UI;
using Pango2D.UI.Views;
using System.Linq;

namespace Demo.Scenes
{
    public class WorldScene : HybridScene
    {

        protected override World ConfigureScene(UIManager uiManager)
        {
            var view = UIView.Create<WorldView>(Services);
            UIManager.AddView(view);
            var world = new WorldBuilder(Services)
                .AddCoreSystems()
                .AddLightingSystems()
                .AddSystem(new TileMapRenderer())
                .AddSystem(new SoundEffectCommandSystem(SoundEffectRegistry))
                .AddSystem(new MainCameraSystem())
                .AddSystem(new PlayerInputSystem(InputProvider))
                .AddSystem(new LootPickupSystem())
                .AddSystem(new MeleeDamageSystem())
                .AddSystem(new HealthBarRenderer())
                .AddSystem(new DebugColliderRenderSystem())
                .Build();
            var prefabFactory = new PrefabFactory(world);

            Entity map = TiledWorldLoader.LoadMap("Content/Tiled/Tilemaps/overworld.tmj", world, prefabFactory.TiledFactory);
            Entity camera = new EntityBuilder(world)
                .AddComponent(new Camera()
                {
                    Offset = new Vector2(32, 32),
                    MaxSpeed = 1500f,
                    Zoom = 1f,
                    ViewportWidth = Services.ViewportService.VirtualWidth,
                    ViewportHeight = Services.ViewportService.VirtualHeight
                })
                .Build();
            SoundEffectRegistry.Add("bottle", Content.Load<SoundEffect>("Sounds/SoundEffects/bottle"));
            SoundEffectRegistry.Add("swing", Content.Load<SoundEffect>("Sounds/SoundEffects/swing"));
            UILoader loader = new UILoader(Services.GameWindow, FontRegistry);
            var entitypos = loader.LoadWithContext("Views/EntityPos.xaml", world.Query<Transform, MainCameraTarget>().FirstOrDefault().Item2);
            var goldview = loader.LoadWithContext("Views/GoldView.xaml", world.Query<PlayerComponent>().FirstOrDefault().Item2);
            var healthView = loader.LoadWithContext("Views/HealthView.xaml", world.Query<PlayerComponent>().FirstOrDefault().Item2);
            var enemyHealthView = loader.LoadWithContext("Views/HealthView.xaml", world.Query<EnemyComponent>().FirstOrDefault().Item2);
            enemyHealthView.Padding = new Point(10, 135);
            UIManager.AddRootElement(goldview);
            UIManager.AddRootElement(healthView);
            UIManager.AddRootElement(enemyHealthView);
            return world;
        }

        public override void Draw(GameTime gameTime)
        {
            var spriteBatch = Services.SpriteBatch;
            Services.Get<RenderTargetRegistry>().ClearRenderTargets();
            World?.Draw(gameTime, spriteBatch);
            UIManager?.Draw(spriteBatch, UIRenderPassSettings);
        }
    }
}
