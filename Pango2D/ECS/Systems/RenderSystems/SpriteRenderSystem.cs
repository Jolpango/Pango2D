using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.Core.Contracts;
using Pango2D.Core.Graphics;
using Pango2D.ECS.Components;
using Pango2D.Extensions;
using Pango2D.Graphics.Sprites;

namespace Pango2D.ECS.Systems.RenderSystems
{
    /// <summary>
    /// A system responsible for rendering sprites in the game world.
    /// </summary>
    /// <remarks>This system is designed to render entities that have both a <see cref="Sprite"/> and a <see
    /// cref="Transform"/> component. It operates during the world rendering phase and applies the current camera's view
    /// matrix to the rendering process.</remarks>
    public class SpriteRenderSystem : DrawComponentSystem<Sprite, Transform>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteRenderSystem"/> class.
        /// </summary>
        /// <remarks>This constructor sets the default render phase to <see
        /// cref="RenderPhase.World"/>.</remarks>
        public SpriteRenderSystem()
        {
            RenderPhase = RenderPhase.World;
            renderPassSettings.BlendState = BlendState.AlphaBlend;
        }

        /// <summary>
        /// Prepares the rendering process by applying the current view matrix to the sprite batch.
        /// </summary>
        /// <remarks>This method retrieves the view matrix from the <see cref="ICameraService"/> in the
        /// world services. If no camera service is available, the identity matrix is used as the default. The view
        /// matrix is applied to the rendering pass settings before delegating to the base implementation.</remarks>
        /// <param name="spriteBatch">The <see cref="SpriteBatch"/> instance used for rendering sprites.</param>
        public override void BeginDraw(SpriteBatch spriteBatch)
        {
            var viewMatrix = World.Services.TryGet<ICameraService>()?.GetViewMatrix() ?? Matrix.Identity;
            spriteBatch.GraphicsDevice.SetRenderTarget(null); // Ensure rendering is set to the backbuffer
            spriteBatch.GraphicsDevice.Clear(Color.Black);
            renderPassSettings.TransformMatrix = viewMatrix;
            spriteBatch.Begin(renderPassSettings);
        }

        /// <summary>
        /// Renders the specified entity's sprite using the provided sprite batch and transform.
        /// </summary>
        /// <param name="gameTime">The current game time, used to track the timing of the draw operation.</param>
        /// <param name="spriteBatch">The <see cref="SpriteBatch"/> used to draw the sprite.</param>
        /// <param name="entity">The entity associated with the sprite being drawn.</param>
        /// <param name="sprite">The sprite to be rendered.</param>
        /// <param name="transform">The transform that defines the position, rotation, and scale of the sprite.</param>
        protected override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Entity entity, Sprite sprite, Transform transform)
        {
            sprite.Draw(spriteBatch, transform);
        }
    }
}
