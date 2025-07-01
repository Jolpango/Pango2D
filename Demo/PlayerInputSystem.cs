using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pango2D.Core.Input.Contracts;
using Pango2D.ECS;
using Pango2D.ECS.Components;
using Pango2D.ECS.Components.Contracts;
using Pango2D.ECS.Systems;
using Pango2D.Graphics.Sprites;
using System;

namespace Demo
{
    public class DamageComponent : IComponent
    {
        public int Damage { get; set; } = 10; // Default damage value
    }   
    public class PlayerInputSystem(IInputProvider input) : PreUpdateComponentSystem<PlayerComponent, Velocity, Sprite, Transform>
    {
        private readonly IInputProvider input = input;
        private bool isAttacking = false;
        protected override void PreUpdate(GameTime gameTime, Entity entity, PlayerComponent player, Velocity velocity, Sprite sprite, Transform transform)
        {
            Vector2 direction = Vector2.Zero;
            if (input.IsKeyDown(Keys.W) || input.IsKeyDown(Keys.Up))
                direction.Y -= 1;
            if (input.IsKeyDown(Keys.S) || input.IsKeyDown(Keys.Down))
                direction.Y += 1;
            if (input.IsKeyDown(Keys.A) || input.IsKeyDown(Keys.Left))
                direction.X -= 1;
            if (input.IsKeyDown(Keys.D) || input.IsKeyDown(Keys.Right))
                direction.X += 1;

            direction = direction.LengthSquared() > 0 ? Vector2.Normalize(direction) : Vector2.Zero;
            Vector2 previousDirection = velocity.Value.LengthSquared() > 0 ? Vector2.Normalize(velocity.Value) : Vector2.Zero;
            velocity.Value = direction * (isAttacking ? 200f : 500f); // Speed can be adjusted as needed

            string animationName = null;
            if (!isAttacking)
            {
                if(direction.Length() > 0)
                {
                    animationName = "Walk";
                    if (direction.X < 0)
                    {
                        sprite.Effects = SpriteEffects.FlipHorizontally;
                    }
                    else if(direction.X > 0)
                    {
                        sprite.Effects = SpriteEffects.None;
                    }
                }
                if (animationName != null)
                {
                    World.AddComponent(entity, new AnimationCommand()
                    {
                        AnimationName = animationName,
                        OnFrameChanged = (index) =>
                        {
                            if (index == 7 || index == 3)
                            {
                                World.AddComponent(entity, new SoundEffectCommand() { SoundEffectName = "swing", Volume = 0.05f });
                            }
                        },
                        Loop = false
                    });
                }
                if (input.IsKeyPressed(Keys.Space))
                {
                    isAttacking = true;
                    World.AddComponent(entity, new SoundEffectCommand() { SoundEffectName = "swing", Pitch = Random.Shared.NextSingle() });
                    var hitboxPosition = sprite.Effects == SpriteEffects.FlipHorizontally
                        ? transform.Position + new Vector2(-100, 0) // Adjust hitbox position for left-facing sprite
                        : transform.Position + new Vector2(50, 0); // Adjust hitbox position for right-facing sprite

                    World.AddComponent(entity, new AnimationCommand()
                    {
                        AnimationName = "Attack01",
                        ForceRestart = true,
                        Loop = false,
                        OnFrameChanged = (index) =>
                        {
                            if (index != 2) return; // Only create hitbox on the 4th frame
                            var hitbox = new EntityBuilder(World)
                             .AddComponent(new Transform() { Position = hitboxPosition, Scale = Vector2.One * 4 })
                             .AddComponent(new DamageComponent())
                             .AddComponent(new Collider() { Bounds = new Rectangle(0, 0, 150, 100), IsTrigger = true, IsTransient = true })
                             .Build();
                        },
                        OnEnd = () =>
                        {
                            isAttacking = false;
                        }
                    });
                }
            }
        }
    }
}
