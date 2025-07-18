using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.Core.Graphics;
using Pango2D.ECS.Components;
using Pango2D.ECS.Systems.Contracts;


namespace Pango2D.ECS.Systems.RenderSystems
{
    public class DebugColliderRenderSystem : IDrawSystem
    {
        public RenderPhase RenderPhase { get => RenderPhase.Debug; }
        public World World { get; set; }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var (_, collider, transform) in World.Query<Collider, Transform>())
            {
                var color = Color.Blue;
                if (collider.Behavior == ColliderBehavior.Dynamic)
                    color = Color.Red;
                else if (collider.Behavior == ColliderBehavior.Trigger)
                    color = Color.Green;
                spriteBatch.Draw(TextureCache.White,
                    new Rectangle((int)transform.Position.X + collider.Bounds.X,
                                  (int)transform.Position.Y + collider.Bounds.Y,
                                  collider.Bounds.Width,
                                  collider.Bounds.Height),
                    new Rectangle(0,
                                  0,
                                  collider.Bounds.Width,
                                  collider.Bounds.Height),
                    color * 0.2f,
                    0f,
                    //new Vector2(collider.Bounds.Width / 2, collider.Bounds.Height / 2),
                    Vector2.Zero,
                    SpriteEffects.None,
                    1f);
            }
        }

        public void Initialize()
        {

        }
    }
}
