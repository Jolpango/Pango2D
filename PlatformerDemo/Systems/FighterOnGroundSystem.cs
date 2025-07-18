using Microsoft.Xna.Framework;
using Pango2D.ECS;
using Pango2D.ECS.Components;
using Pango2D.ECS.Components.Physics;
using Pango2D.ECS.Systems.Contracts;
using PlatformerDemo.Components;
using System;

namespace PlatformerDemo.Systems
{
    public class FighterOnGroundSystem : IPostUpdateSystem
    {
        public World World { get; set; }

        public void Initialize() { }

        public void PostUpdate(GameTime gameTime)
        {
            foreach (var (entity, fighter) in World.Query<FighterComponent>())
            {
                if (!World.TryGetComponent<CollisionEvents>(entity, out var collisionEvents)
                    || collisionEvents.Events.Count <= 0)
                {
                    World.TryGetComponent<Friction>(entity, out var friction);
                    SetAirbourneFighterData(fighter, friction);
                    continue;
                }
                foreach (var collision in collisionEvents.Events)
                {
                    if (!World.TryGetComponent<Collider>(collision.Other, out var collider) ||
                        collider.Behavior != ColliderBehavior.Static)
                        continue;
                    World.TryGetComponent<Friction>(entity, out var friction);
                    if (collision.Normal.Y == -1)
                    {
                        if(fighter.ContactType == ContactType.None)
                        {
                            OnFighterLand(entity, fighter);
                        }
                        SetGroundedFighterData(fighter, collision.Normal, friction);
                    }
                    else if (MathF.Abs(collision.Normal.X) == 1)
                    {
                        SetWallFighterData(fighter, collision.Normal, friction);
                    }
                }
            }
        }

        private void OnFighterLand(Entity entity, FighterComponent fighter)
        {
            if(fighter.ActionState == FighterActionState.Attacking)
                return;
            fighter.MovementState = FighterMovementState.Landing;
        }

        private static void SetAirbourneFighterData(FighterComponent fighter, Friction friction)
        {
            friction.Value = 0.85f;
            fighter.ContactType = ContactType.None;
            fighter.ContactNormal = Vector2.Zero;
        }

        private static void SetGroundedFighterData(FighterComponent fighter, Vector2 contactNormal, Friction friction)
        {
            friction.Value = 0.9999f;
            fighter.JumpsLeft = fighter.MaxJumps;
            fighter.ContactType = ContactType.Ground;
            fighter.ContactNormal = contactNormal;
        }

        private static void SetWallFighterData(FighterComponent fighter, Vector2 contactNormal, Friction friction)
        {
            friction.Value = 0.9999f;
            fighter.JumpsLeft = 2;
            fighter.ContactType = ContactType.Wall;
            fighter.ContactNormal = contactNormal;
        }
    }
}
