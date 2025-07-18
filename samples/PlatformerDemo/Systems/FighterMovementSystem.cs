using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.ECS;
using Pango2D.ECS.Components.Physics;
using Pango2D.ECS.Systems.Contracts;
using Pango2D.Graphics.Sprites;
using PlatformerDemo.Components;
using System;

namespace PlatformerDemo.Systems
{
    internal class FighterMovementSystem : IPreUpdateSystem
    {
        public World World { get; set; }

        public void Initialize() { }

        public void PreUpdate(GameTime gameTime)
        {
            foreach (var (entity, fighter, acceleration) in World.Query<FighterComponent, Acceleration>())
            {
                World.TryGetComponent<WantsToMoveComponent>(entity, out var moveIntent);
                if (moveIntent is not null && CanMove(fighter))
                {
                    if(fighter.MovementState != FighterMovementState.Jumping)
                        fighter.MovementState = FighterMovementState.Moving;
                    acceleration.Value += GetMovementDirection(fighter, moveIntent) * GetMovementSpeed(fighter);
                    if (World.TryGetComponent<Sprite>(entity, out var sprite))
                    {
                        SetFighterDirection(moveIntent.Direction, sprite, fighter);
                    }
                }
                SetMovementState(fighter, acceleration);
            }
        }

        private static void SetMovementState(FighterComponent fighter, Acceleration acceleration)
        {
            if (Math.Abs(acceleration.Value.X) <= 1 && fighter.ContactType == ContactType.Ground)
            {
                fighter.MovementState = FighterMovementState.Idle;
            }
        }

        private Vector2 GetMovementDirection(FighterComponent fighter, WantsToMoveComponent moveIntent)
        {
            if(fighter.ContactType != ContactType.Wall)
                return new Vector2(moveIntent.Direction.X, MathF.Max(moveIntent.Direction.Y, 0f));
            return moveIntent.Direction;
        }

        private void SetFighterDirection(Vector2 direction, Sprite sprite, FighterComponent fighter)
        {
            if (direction.X < 0)
            {
                sprite.Effects = SpriteEffects.FlipHorizontally;
                fighter.Direction = FighterDirection.Left;
            }
            else if (direction.X > 0)
            {
                sprite.Effects = SpriteEffects.None;
                fighter.Direction = FighterDirection.Right;
            } 
        }

        private bool CanMove(FighterComponent fighter)
        {
            if (fighter.ActionState == FighterActionState.Attacking)
                return false;
            return true;
        }

        private float GetMovementSpeed(FighterComponent fighter)
        {
            switch(fighter.ContactType)
            {
                case ContactType.Ground:
                    return fighter.GroundSpeed;
                case ContactType.Wall:
                case ContactType.Ceiling:
                case ContactType.None:
                    return fighter.AirSpeed;
                default:
                    return fighter.AirSpeed;
            }
        }
    }
}
