using Microsoft.Xna.Framework;
using Pango2D.ECS;
using Pango2D.ECS.Components.Commands;
using Pango2D.ECS.Systems.Contracts;
using PlatformerDemo.Components;

namespace PlatformerDemo.Systems
{
    public class FighterAnimationSystem : IPreUpdateSystem
    {
        public World World { get; set; }

        public void Initialize() { }

        public void PreUpdate(GameTime gameTime)
        {
            foreach (var (entity, fighter) in World.Query<FighterComponent>())
            {
                string animation = GetAnimationForState(fighter);
                if (!string.IsNullOrEmpty(animation))
                {
                    World.AddComponent(entity, new AnimationCommand { AnimationName = animation });
                }
            }
        }

        private static string GetAnimationForState(FighterComponent fighter)
        {
            if (fighter.ActionState == FighterActionState.Attacking)
            {
                return fighter.CurrentAttack?.AnimationName ?? "Idle";
            }

            return fighter.MovementState switch
            {
                FighterMovementState.Idle => "Idle",
                FighterMovementState.Moving => GetMovingAnimation(fighter),
                FighterMovementState.Jumping => "Jump",
                _ => "Idle"
            };
        }

        private static string GetMovingAnimation(FighterComponent fighter)
        {
            if (fighter.ContactType == ContactType.None)
            {
                return "Fall";
            }
            else if (fighter.ContactType == ContactType.Ground)
            {
                return "Run";
            }
            else if (fighter.ContactType == ContactType.Wall || fighter.ContactType == ContactType.Ceiling)
            {
                return "WallSlide";
            }
            return "Run";
        }
    }
}
