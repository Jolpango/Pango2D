using Demo.Views;
using Microsoft.Xna.Framework;
using Pango2D.Core.Audio;
using Pango2D.Core.Graphics;
using Pango2D.Core.Scenes;
using Pango2D.ECS;
using Pango2D.ECS.Components;
using Pango2D.ECS.Components.CameraComponents;
using Pango2D.ECS.Systems.RenderSystems;
using Pango2D.ECS.Systems.UpdateSystems.CameraSystems;
using Pango2D.ECS.Systems.UpdateSystems.MovementSystems;
using Pango2D.ECS.Systems.UpdateSystems.SoundSystems;
using Pango2D.UI;
using Pango2D.UI.Views;
using System.Linq;

namespace Demo.Scenes
{
    public class WorldScene : HybridScene
    {
        public override void LoadContent()
        {
            TextureRegistry.Load("Soldier", "Soldier");
            TextureRegistry.Load("Orc", "Orc");
            SoundEffectRegistry.Load("bottle", "Sounds/SoundEffects/bottle");
            SoundEffectRegistry.Load("swing", "Sounds/SoundEffects/swing");

            base.LoadContent();
        }
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
                .AddSystem(new CameraShakeSystem())
                .AddSystem(new PlayerInputSystem(InputProvider, Services.GamePadManager))
                .AddSystem(new LootPickupSystem())
                .AddSystem(new MeleeDamageSystem())
                .AddSystem(new HealthBarRenderer())
                .AddSystem(new DebugColliderRenderSystem())
                .Build();
            var prefabFactory = new PrefabFactory(world);

            Entity map = TiledWorldLoader.LoadMap("Content/Tiled/Tilemaps/island.tmj", world, scale: 4, prefabFactory: prefabFactory.TiledFactory);
            Entity camera = new EntityBuilder(world)
                .AddComponent(new Camera()
                {
                    Offset = new Vector2(32, 32),
                    Zoom = 1f,
                    ViewportWidth = Services.ViewportService.VirtualWidth,
                    ViewportHeight = Services.ViewportService.VirtualHeight
                })
                .AddComponent(new Light()
                {
                    Color = Color.White * 0.4f,
                    Type = LightType.Ambient,
                })
                .AddComponent(new CameraAcceleration())
                .AddComponent(new CameraShake())
                .Build();
            UILoader loader = new UILoader(Services);
            var entitypos = loader.LoadWithContext("Views/EntityPos.xaml", world.Query<Transform, MainCameraTarget>().FirstOrDefault().Item2);
            var goldview = loader.LoadWithContext("Views/GoldView.xaml", world.Query<PlayerComponent>().FirstOrDefault().Item2);
            var healthView = loader.LoadWithContext("Views/HealthView.xaml", world.Query<PlayerComponent>().FirstOrDefault().Item2);
            healthView.AddChild(goldview);
            UIManager.AddRootElement(healthView);
            return world;
        }

        public override void Draw(GameTime gameTime)
        {
            var spriteBatch = Services.SpriteBatch;
            World?.Draw(gameTime, spriteBatch);
            UIManager?.Draw(spriteBatch, UIRenderPassSettings);
        }
    }
}
