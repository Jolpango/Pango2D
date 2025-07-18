using Demo.Content;
using Microsoft.Xna.Framework;
using Pango2D.ECS;
using Pango2D.ECS.Components;
using Pango2D.ECS.Components.CameraComponents;
using Pango2D.ECS.Components.Commands;
using Pango2D.ECS.Systems.Contracts;
using Pango2D.Graphics.Particles;
using System;
using System.Linq;

namespace Demo
{
    public class MeleeDamageSystem : IPostUpdateSystem
    {
        public World World { get; set; }

        public void Initialize() { }

        public void PostUpdate(GameTime gameTime)
        {
            foreach (var (entity, enemy, collisionEvent) in World.Query<EnemyComponent, CollisionEvent>())
            {
                if (World.TryGetComponent<DamageComponent>(collisionEvent.Other, out var damageComponent))
                {
                    var (camera, cameraShake) = World.Query<CameraShake>().FirstOrDefault();
                    if(cameraShake is not null)
                    {
                        cameraShake.Intensity = 20f;
                        cameraShake.TimeLeft = 0.08f;
                        cameraShake.RotationalIntensity = 0.02f;
                    }
                    if(World.TryGetComponent<ParticleEffect>(entity, out var particleEffect) &&
                        World.TryGetComponent<Transform>(entity, out var transform))
                    {
                        particleEffect.Emit(transform.Position + new Vector2(40, 40));
                    }
                    if (enemy.Health <= 0)
                        continue;
                    enemy.Health -= damageComponent.Damage;
                    World.AddComponent(entity, new SoundEffectCommand()
                    {
                        SoundEffectName = "bottle",
                        Volume = 0.2f
                    });
                    World.AddComponent(entity, new AnimationCommand()
                    {
                        AnimationName = "hurt",
                        Loop = false,
                        OnEnd = () =>
                        {
                            if (enemy.Health <= 0)
                            {
                                World.AddComponent(entity, new AnimationCommand()
                                {
                                    AnimationName = "Death",
                                    Loop = false,
                                    OnEnd = () =>
                                    {
                                        World.DestroyEntity(entity);
                                    }
                                });
                            }
                        }
                    });
                }
            }
        }
    }
}
