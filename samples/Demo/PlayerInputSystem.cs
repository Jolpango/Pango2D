using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pango2D.Core.Input;
using Pango2D.Core.Input.Contracts;
using Pango2D.ECS;
using Pango2D.ECS.Components;
using Pango2D.ECS.Components.Commands;
using Pango2D.ECS.Components.Contracts;
using Pango2D.ECS.Components.Physics;
using Pango2D.ECS.Systems;
using Pango2D.Graphics.Sprites;
using System;

namespace Demo
{
    public class DamageComponent : IComponent
    {
        public int Damage { get; set; } = 10;
    }   
    public class PlayerInputSystem(IInputProvider input, GamePadManager gamepad) : PreUpdateComponentSystem<PlayerComponent, Acceleration, Sprite, Transform, Velocity>
    {
        private readonly IInputProvider input = input;
        private readonly GamePadManager gamepad = gamepad;
        private bool isAttacking = false;
        protected override void PreUpdate(GameTime gameTime, Entity entity, PlayerComponent player, Acceleration acceleration, Sprite sprite, Transform transform, Velocity velocity)
        {
            Vector2 direction = gamepad.GetLeftThumbstick(PlayerIndex.One);

            Vector2 previousDirection = acceleration.Value.LengthSquared() > 0 ? Vector2.Normalize(acceleration.Value) : Vector2.Zero;

            if(input.IsKeyDown(Keys.Left) || gamepad.IsButtonDown(PlayerIndex.One, Buttons.DPadLeft))
            {
                direction.X -= 1;
            }
            if (input.IsKeyDown(Keys.Right) || gamepad.IsButtonDown(PlayerIndex.One, Buttons.DPadRight))
            {
                direction.X += 1;
            }
            if (input.IsKeyDown(Keys.Up) || gamepad.IsButtonDown(PlayerIndex.One, Buttons.DPadUp))
            {
                direction.Y -= 1;
            }
            if (input.IsKeyDown(Keys.Down) || gamepad.IsButtonDown(PlayerIndex.One, Buttons.DPadDown))
            {
                direction.Y += 1;
            }
            acceleration.Value += direction * (isAttacking ? 500f : 3000f);

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
                if(gamepad.IsButtonPressed(PlayerIndex.One, Buttons.A))
                {
                    acceleration.Value += -Vector2.UnitY * 100000;
                }
                if (input.IsKeyPressed(Keys.Space))
                {
                    isAttacking = true;
                    acceleration.Value += direction * 25000;
                    World.AddComponent(entity, new SoundEffectCommand() { SoundEffectName = "swing", Pitch = Random.Shared.NextSingle() });

                    World.AddComponent(entity, new AnimationCommand()
                    {
                        AnimationName = "Attack02",
                        ForceRestart = true,
                        Loop = false,
                        OnFrameChanged = (index) =>
                        {
                            if (index != 3) return;
                            var hitboxPosition = sprite.Effects == SpriteEffects.FlipHorizontally
                                ? transform.Position + new Vector2(-100, 0)
                                : transform.Position + new Vector2(50, 0);
                            var hitbox = new EntityBuilder(World)
                             .AddComponent(new Transform() { Position = hitboxPosition, Scale = Vector2.One * 4 })
                             .AddComponent(new DamageComponent() { Damage = 40 })
                             .AddComponent(new Collider() { Bounds = new Rectangle(0, 0, 150, 100), Behavior = ColliderBehavior.Transient })
                             .Build();
                        },
                        OnEnd = () =>
                        {
                            isAttacking = false;
                        }
                    });
                }
                else if (gamepad.IsButtonPressed(PlayerIndex.One, Buttons.X))
                {
                    isAttacking = true;
                    World.AddComponent(entity, new SoundEffectCommand() { SoundEffectName = "swing", Pitch = Random.Shared.NextSingle() });
                    var hitboxPosition = sprite.Effects == SpriteEffects.FlipHorizontally
                        ? transform.Position + new Vector2(-100, 0)
                        : transform.Position + new Vector2(50, 0);

                    World.AddComponent(entity, new AnimationCommand()
                    {
                        AnimationName = "Attack01",
                        ForceRestart = true,
                        Loop = false,
                        OnFrameChanged = (index) =>
                        {
                            if (index != 2) return;
                            var hitbox = new EntityBuilder(World)
                             .AddComponent(new Transform() { Position = hitboxPosition, Scale = Vector2.One * 4 })
                             .AddComponent(new DamageComponent())
                             .AddComponent(new Collider() { Bounds = new Rectangle(0, 0, 150, 100), Behavior = ColliderBehavior.Transient })
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
