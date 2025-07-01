using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.Core.Graphics;
using Pango2D.ECS.Components;
using Pango2D.ECS.Systems.Contracts;
using Pango2D.Graphics.Sprites;

namespace Pango2D.ECS.Systems.RenderSystems
{
    /// <summary>
    /// A system responsible for rendering sprites in the game world.
    /// </summary>
    /// <remarks>This system is designed to render entities that have both a <see cref="Sprite"/> and a <see
    /// cref="Transform"/> component. It operates during the world rendering phase and applies the current camera's view
    /// matrix to the rendering process.</remarks>
    public class SpriteRenderSystem : IDrawSystem
    {
        public RenderPhase RenderPhase { get; set; } = RenderPhase.World;
        public World World { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteRenderSystem"/> class.
        /// </summary>
        /// <remarks>This constructor sets the default render phase to <see
        /// cref="RenderPhase.World"/>.</remarks>
        public SpriteRenderSystem()
        {
            RenderPhase = RenderPhase.World;
        }

        /// <summary>
        /// Initializes the sprite render system.
        /// </summary>
        public void Initialize() { }

        /// <summary>
        /// Draws all sprites in the world using the provided <see cref="SpriteBatch"/>.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            const float minY = -10000f;
            const float maxY = 10000f;

            foreach (var (_, transform, sprite) in World.Query<Transform, Sprite>())
            {
                float bottomY = transform.Position.Y + sprite.SourceRectangle.Height;
                // Clamp t between 0 and 1 to avoid out-of-bounds values
                float t = MathHelper.Clamp((bottomY - minY) / (maxY - minY), 0f, 1f);
                sprite.LayerDepth = MathHelper.Lerp(LayerDepths.EntityBase, LayerDepths.EntityMax, t);
                sprite.Draw(spriteBatch, transform);
            }
        }
    }
}
