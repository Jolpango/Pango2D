using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.Core.Graphics;
using Pango2D.Core.Input.Contracts;
using Pango2D.Core.Services;
using Pango2D.UI;

namespace Pango2D.Core.Scenes
{
    public abstract class UIScene : SceneBase
    {
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
            UIManager = new UIManager();
            ConfigureUI(UIManager);
        }
        protected abstract void ConfigureUI(UIManager uIManager);
        public override void UnloadContent()
        {
            base.UnloadContent();
            UIManager?.Dispose();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            UIManager?.Update(gameTime, Services.Get<IInputProvider>());
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            UIManager?.Draw(Services.Get<SpriteBatch>(), UIRenderPassSettings);
        }
    }
}
