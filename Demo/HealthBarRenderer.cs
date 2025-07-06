using Demo.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.Core.Graphics;
using Pango2D.ECS;
using Pango2D.ECS.Components;
using Pango2D.ECS.Systems.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    public class HealthBarRenderer : IDrawSystem
    {
        public RenderPhase RenderPhase => RenderPhase.World;

        public World World { get; set; }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach(var (entity, enemy, transform) in World.Query<EnemyComponent, Transform>())
            {
                var fullBarDestination = new Rectangle(
                    (int)(transform.Position.X),
                    (int)(transform.Position.Y),
                    enemy.HealthBarWidth,
                    enemy.HealthBarHeight
                );
                var currentHealthPercentage = MathF.Max((float)enemy.Health / enemy.MaxHealth, 0);
                var healthDestination = new Rectangle(
                    (int)(transform.Position.X),
                    (int)(transform.Position.Y),
                    (int)(enemy.HealthBarWidth * currentHealthPercentage),
                    enemy.HealthBarHeight
                );
                spriteBatch.Draw(TextureCache.White, fullBarDestination, null, Color.Red, 0f, Vector2.Zero, SpriteEffects.None, 0.9f);
                spriteBatch.Draw(TextureCache.White, healthDestination, null, Color.Green, 0f, Vector2.Zero, SpriteEffects.None, 0.9f);
            }
        }

        public void Initialize() { }
    }
}
