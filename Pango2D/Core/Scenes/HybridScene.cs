using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.Core.Graphics;
using Pango2D.Core.Input.Contracts;
using Pango2D.Core.Services;
using Pango2D.ECS;
using Pango2D.UI;

namespace Pango2D.Core.Scenes
{
    public abstract class HybridScene : SceneBase
    {
        protected World World { get; private set; }
        protected UIManager UIManager { get; private set; }
        protected RenderPassSettings UIRenderPassSettings { get; private set; } = new RenderPassSettings
        {
            TransformMatrix = Matrix.Identity,
            BlendState = BlendState.AlphaBlend,
            SamplerState = SamplerState.PointClamp,
            RasterizerState = RasterizerState.CullNone
        };
        public override void Initialize(GameServices services)
        {
            base.Initialize(services);
        }
        public override void LoadContent()
        {
            base.LoadContent();
            World = ConfigureWorld();
            UIManager = new UIManager();
            ConfigureUI(UIManager);
        }
        protected abstract World ConfigureWorld();
        protected abstract void ConfigureUI(UIManager uiManager);
        public override void UnloadContent()
        {
            base.UnloadContent();
            World?.Dispose();
            UIManager?.Dispose();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            UIManager?.Update(gameTime, Services.Get<IInputProvider>());
            World?.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            var spriteBatch = Services.Get<SpriteBatch>();
            World?.Draw(gameTime, spriteBatch);
            UIManager?.Draw(spriteBatch, UIRenderPassSettings);
        }
    }
}
