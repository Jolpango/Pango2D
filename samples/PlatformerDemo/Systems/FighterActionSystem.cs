using Microsoft.Xna.Framework;
using Pango2D.ECS;
using Pango2D.ECS.Components.Physics;
using Pango2D.ECS.Systems.Contracts;
using PlatformerDemo.Components;

namespace PlatformerDemo.Systems
{
    internal class FighterActionSystem : IPreUpdateSystem
    {
        public World World { get; set; }

        public void Initialize()
        {
        }

        public void PreUpdate(GameTime gameTime)
        {
            ProcessAttackIntents();
            ProcessHighJumpIntents();
            ProcessJumpIntents();
            ProcessDashIntents();
            UpdateAttackTimers(gameTime);
        }
        private void UpdateAttackTimers(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            foreach (var (entity, fighter) in World.Query<FighterComponent>())
            {
                if (fighter.ActionState == FighterActionState.Attacking)
                {
                    fighter.AttackTimer -= dt;
                    if (fighter.AttackTimer <= 0f)
                    {
                        fighter.ActionState = FighterActionState.Idle;
                        fighter.AttackTimer = 0f;
                        fighter.CurrentAttack = null;
                    }
                }
                if (fighter.MovementState == FighterMovementState.Jumping)
                {
                    fighter.JumpTimer -= dt;
                    if (fighter.JumpTimer <= 0f)
                    {
                        fighter.MovementState = FighterMovementState.Idle;
                        fighter.JumpTimer = 0f;
                    }
                }
                if (fighter.MovementState == FighterMovementState.Dashing)
                {
                    fighter.DashTimer -= dt;
                    if (fighter.DashTimer <= 0f)
                    {
                        fighter.MovementState = FighterMovementState.Idle;
                        fighter.DashTimer = 0f;
                    }
                }
                fighter.DashCooldownTimer -= dt;
            }
        }

        private void ProcessDashIntents()
        {
            foreach (var (_, fighter, dashIntent, acceleration) in World.Query<FighterComponent, WantsToDashComponent, Acceleration>())
            {
                if (CanDash(fighter))
                {
                    fighter.ActionState = FighterActionState.Idle;
                    fighter.MovementState = FighterMovementState.Dashing;
                    fighter.DashCooldownTimer = fighter.DashCooldown;
                    fighter.DashTimer = fighter.DashTime;
                    acceleration.Value += dashIntent.Direction * fighter.DashForce;
                }
            }
        }

        private void ProcessAttackIntents()
        {
            foreach (var (_, fighter, attackIntent) in World.Query<FighterComponent, WantsToAttackComponent>())
            {
                if (CanAttack(fighter))
                {
                    fighter.ActionState = FighterActionState.Attacking;
                    fighter.CurrentAttack = GetFighterAttack(fighter, attackIntent);
                    fighter.AttackTimer = fighter.CurrentAttack.Duration;
                }
            }
        }

        private void ProcessHighJumpIntents()
        {
            foreach (var (entity, fighter, intent, acceleration) in World.Query<FighterComponent, WantsToHighJumpComponent, Acceleration>())
            {
                if(fighter.MovementState == FighterMovementState.Jumping && fighter.ContactType == ContactType.None)
                    acceleration.Value.Y += -fighter.HighJumpForce;
            }
        }

        private void ProcessJumpIntents()
        {
            foreach (var (entity, fighter, intent, acceleration) in World.Query<FighterComponent, WantsToJumpComponent, Acceleration>())
            {
                if (CanJump(fighter))
                {
                    switch (fighter.ContactType)
                    {
                        case ContactType.Ground:
                            GroundJump(fighter, acceleration);
                            break;
                        case ContactType.Wall:
                            WallJump(fighter, acceleration);
                            break;
                        case ContactType.Ceiling:
                        case ContactType.None:
                            AirJump(fighter, acceleration);
                            break;
                    }
                    fighter.JumpTimer = fighter.JumpTime;
                    fighter.MovementState = FighterMovementState.Jumping;
                }
            }
        }

        private FighterAttack GetFighterAttack(FighterComponent fighter, WantsToAttackComponent attackIntent)
        {
            switch (attackIntent.Type)
            {
                case AttackType.Normal:
                    return GetNormalAttack(fighter);
                case AttackType.Signature:
                    return GetSignatureAttack(fighter);
                default:
                    return GetNormalAttack(fighter); 
            }
        }
        private FighterAttack GetNormalAttack(FighterComponent fighter)
        {
            switch (fighter.ContactType)
            {
                case ContactType.Ground:
                    return fighter.AttackList[0];
                case ContactType.Wall:
                case ContactType.Ceiling:
                case ContactType.None:
                    return fighter.AttackList[1];
                default:
                    return fighter.AttackList[1];
            }
        }
        private FighterAttack GetSignatureAttack(FighterComponent fighter)
        {
            switch (fighter.ContactType)
            {
                case ContactType.Ground:
                case ContactType.Wall:
                case ContactType.Ceiling:
                case ContactType.None:
                    return fighter.AttackList[2];
                default:
                    return fighter.AttackList[2];
            }
        }

        private void GroundJump(FighterComponent fighter, Acceleration acceleration)
        {
            acceleration.Value.Y += -fighter.GroundJumpForce;
            fighter.JumpsLeft--;
        }
        private void AirJump(FighterComponent fighter, Acceleration acceleration)
        {
            acceleration.Value.Y += -fighter.AirJumpForce;
            fighter.JumpsLeft--;
        }
        private void WallJump(FighterComponent fighter, Acceleration acceleration)
        {
            var direction = new Vector2(fighter.ContactNormal.X, -2);
            direction.Normalize();
            acceleration.Value += direction * fighter.WallJumpForce;
            fighter.JumpsLeft--;
        }

        private bool CanAttack(FighterComponent fighter)
        {
            if (fighter.ActionState == FighterActionState.Attacking)
                return false;
            return true;
        }

        private bool CanDash(FighterComponent fighter)
        {
            return fighter.DashCooldownTimer < 0;
        }

        private bool CanJump(FighterComponent fighter)
        {
            if (fighter.ActionState == FighterActionState.Attacking)
                return false;
            if(fighter.JumpsLeft <= 0)
                return false;
            return true;
        }
    }
}
