using Microsoft.Xna.Framework;
using Pango2D.ECS;
using Pango2D.ECS.Components;
using Pango2D.ECS.Components.Physics;
using Pango2D.ECS.Systems.Contracts;
using PlatformerDemo.Components;
using System;

namespace PlatformerDemo.Systems
{
    public class FighterAttackSystem : IPostUpdateSystem
    {
        public World World { get; set; }

        public void Initialize()
        {
        }

        public void PostUpdate(GameTime gameTime)
        {
            foreach(var(entity, fighter) in World.Query<FighterComponent>((_, f) => f.CurrentAttack != null))
            {
                var currentAttack = fighter.CurrentAttack;
                var elapsedTime = currentAttack.Duration - fighter.AttackTimer;
                var previousElapsedTime = currentAttack.Duration - (fighter.AttackTimer + (float)gameTime.ElapsedGameTime.TotalSeconds);
                foreach (var hitboxSettings in fighter.CurrentAttack.HitboxSequence)
                {
                    if(ShouldSpawnHitbox(hitboxSettings, elapsedTime, previousElapsedTime))
                    {
                        SpawnHitbox(entity, hitboxSettings, fighter);
                    }
                }
            }
            foreach(var (entity, fighter, acceleration, collisionEvent) in World.Query<FighterComponent, Acceleration, CollisionEvents>())
            {
                foreach(var collision in collisionEvent.Events)
                {
                    if(World.TryGetComponent<DamageComponent>(collision.Other, out var damageComponent))
                    {
                        acceleration.Value += damageComponent.KnockbackDirection * damageComponent.Knockback;
                    }
                }
            }
        }

        private void SpawnHitbox(Entity entity, HitboxSettings hitboxSettings, FighterComponent fighter)
        {
            if (!World.TryGetComponent<Transform>(entity, out var transform))
                return;

            var hitbox = fighter.Direction == FighterDirection.Right
                ? hitboxSettings.Hitbox
                : new Rectangle(
                    hitboxSettings.Hitbox.X * -1,
                    hitboxSettings.Hitbox.Y,
                    hitboxSettings.Hitbox.Width,
                    hitboxSettings.Hitbox.Height);
            var hitboxEntity = new EntityBuilder(World)
                .AddComponent(new Collider()
                {
                    Type = ColliderType.AABB,
                    Behavior = ColliderBehavior.Transient,
                    Bounds = hitbox
                })
                .AddComponent(new Transform() { Position = transform.Position })
                .AddComponent(new DamageComponent()
                {
                    Damage = hitboxSettings.DamageMultiplier,
                    KnockbackDirection = fighter.Direction == FighterDirection.Left
                        ? new Vector2(-1, -1)
                        : new Vector2(1, -1),
                    Knockback = 50000
                })
                .Build();
        }

        private bool ShouldSpawnHitbox(HitboxSettings hitboxSettings, float elapsedTime, float previousElapsedTime)
        {
            return hitboxSettings.SpawnTime <= elapsedTime && hitboxSettings.SpawnTime > previousElapsedTime;
        }
    }
}
