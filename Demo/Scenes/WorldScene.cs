using Demo.Content.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.Core.Audio;
using Pango2D.Core.Contracts;
using Pango2D.Core.Graphics;
using Pango2D.Core.Input.Contracts;
using Pango2D.Core.Scenes;
using Pango2D.ECS;
using Pango2D.ECS.Components;
using Pango2D.ECS.Services;
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
        private TileMap tileMap;
        protected override void ConfigureUI(UIManager uiManager)
        {
            var view = UIView.Create<ViewTest>(Services);
            uiManager.AddView(view);
        }

        protected override World ConfigureWorld()
        {
            var world = new WorldBuilder(Services)
                .AddCoreSystems()
                .AddLightingSystems()
                .AddSystem(new SoundEffectCommandSystem(SoundEffectRegistry))
                .AddSystem(new MainCameraSystem())
                .AddSystem(new PlayerInputSystem(InputProvider))
                .Build();

            Entity player = new EntityBuilder(world)
                .AddComponent(new PlayerComponent())
                .AddComponent(new Transform())
                .AddComponent(new Velocity(Vector2.One * 100))
                .AddComponent(new Sprite(Content.Load<Texture2D>("spinning-dagger")))
                .AddComponent(new SpriteAnimator(AsepriteLoader.Load("spinning-dagger.json")))
                .AddComponent(new Light() { Color = Color.White, Radius = 100, Intensity = 1f })
                .AddComponent(new MainCameraTarget())
                .Build();

            Entity light = new EntityBuilder(world)
                .AddComponent(new Transform() { Position = new Vector2(100, 200)})
                .AddComponent(new Light() { Color = Color.Orange, Radius = 100f, Intensity = 1f })
                .Build();

            Entity light2 = new EntityBuilder(world)
                .AddComponent(new Transform() { Position = new Vector2(100, 0) })
                .AddComponent(new Light() { Color = Color.Orange, Radius = 100f, Intensity = 0.5f })
                .Build();

            CameraService.SetZoom(1.5f);
            SoundEffectRegistry.Add("bottle", Content.Load<SoundEffect>("Sounds/SoundEffects/bottle"));
            SoundEffectRegistry.Add("swing", Content.Load<SoundEffect>("Sounds/SoundEffects/swing"));
            var loader = new TileMapLoader();
            tileMap = loader.LoadTileMap("Content/Tiled/Tilemaps/demo.tmj", Content);

            return world;
        }

        public override void Draw(GameTime gameTime)
        {
            var spriteBatch = Services.Get<SpriteBatch>();
            spriteBatch.GraphicsDevice.SetRenderTarget(Services.Get<RenderTargetRegistry>().GetOrCreate("World"));
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone, null, CameraService.GetViewMatrix());
            tileMap?.Draw(spriteBatch);
            spriteBatch.End();
            World?.Draw(gameTime, spriteBatch);
            UIManager?.Draw(spriteBatch, UIRenderPassSettings);
        }
    }
}
