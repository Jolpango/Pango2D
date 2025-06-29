using Demo.Views;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.Core.Audio;
using Pango2D.Core.Graphics;
using Pango2D.Core.Scenes;
using Pango2D.ECS;
using Pango2D.ECS.Components;
using Pango2D.ECS.Systems.RenderSystems;
using Pango2D.ECS.Systems.UpdateSystems;
using Pango2D.Graphics.Sprites;
using Pango2D.Tiled;
using Pango2D.UI;
using Pango2D.UI.Views;
using Pango2D.Utilities;

namespace Demo.Scenes
{
    public class WorldScene : HybridScene
    {
        protected override void ConfigureUI(UIManager uiManager)
        {
            var view = UIView.Create<WorldView>(Services);
            uiManager.AddView(view);
        }

        protected override World ConfigureWorld()
        {
            var world = new WorldBuilder(Services)
                .AddCoreSystems()
                .AddLightingSystems()
                .AddSystem(new TileMapRenderer())
                .AddSystem(new SoundEffectCommandSystem(SoundEffectRegistry))
                .AddSystem(new MainCameraSystem())
                .AddSystem(new PlayerInputSystem(InputProvider))
                .AddSystem(new DebugColliderRenderSystem())
                .Build();
            var prefabFactory = new PrefabFactory(world);

            Entity map = TiledWorldLoader.LoadMap("Content/Tiled/Tilemaps/demo.tmj", world, prefabFactory.TiledFactory);
            CameraService.SetZoom(2f);
            SoundEffectRegistry.Add("bottle", Content.Load<SoundEffect>("Sounds/SoundEffects/bottle"));
            SoundEffectRegistry.Add("swing", Content.Load<SoundEffect>("Sounds/SoundEffects/swing"));

            return world;
        }

        public override void Draw(GameTime gameTime)
        {
            var spriteBatch = Services.Get<SpriteBatch>();
            Services.Get<RenderTargetRegistry>().ClearRenderTargets();
            World?.Draw(gameTime, spriteBatch);
            UIManager?.Draw(spriteBatch, UIRenderPassSettings);
        }
    }
}
