using Microsoft.Xna.Framework;
using Pango2D.ECS.Components.Contracts;
using System.Collections.Generic;

namespace PlatformerDemo.Components
{
    public enum ContactType
    {
        None,
        Ground,
        Wall,
        Ceiling,
    }

    public enum FighterMovementState
    {
        Idle,
        Moving,
        Jumping,
        Landing,
        Dashing,
    }
    public enum FighterActionState
    {
        Idle,
        Attacking
    }
    public enum FighterDirection
    {
        Right,
        Left
    }
    public class HitboxSettings
    {
        public float SpawnTime { get; set; } = 0.0f;
        public Rectangle Hitbox { get; set; }
        public float DamageMultiplier { get; set; } = 1.0f;
        public float KnockbackMultiplier { get; set; } = 1.0f;
    }
    public class FighterAttack
    {
        public string Name { get; set; }
        public int Damage { get; set; }
        public float Duration { get; set; }
        public string AnimationName { get; set; }
        public List<HitboxSettings> HitboxSequence { get; set; } = [];
    }
    public class FighterComponent : IComponent
    {
        public string Name { get; set; } = "FighterComponent";
        public FighterMovementState MovementState { get; set; } = FighterMovementState.Idle;
        public FighterActionState ActionState { get; set; } = FighterActionState.Idle;

        public ContactType ContactType { get; set; } = ContactType.None;
        public Vector2 ContactNormal { get; set; } = Vector2.Zero;


        public float AttackTimer { get; set; } = 0f;
        public FighterAttack CurrentAttack { get; set; } = null;
        public FighterAttack[] AttackList = [
            new FighterAttack
            {
                Name = "NormalGround",
                Damage = 10,
                Duration = 0.5f,
                AnimationName = "GroundAttack",
                HitboxSequence = [
                    new HitboxSettings
                    {
                        SpawnTime = 0.3f,
                        Hitbox = new Rectangle(50, 30, 50, 40),
                        DamageMultiplier = 1.0f,
                        KnockbackMultiplier = 1.0f
                    }
                ]
            },
            new FighterAttack
            {
                Name = "NormalAir",
                Damage = 10,
                Duration = 0.5f,
                AnimationName = "AirAttack",
                HitboxSequence = [
                    new HitboxSettings
                    {
                        SpawnTime = 0.1f,
                        Hitbox = new Rectangle(50, 30, 50, 40),
                        DamageMultiplier = 1.0f,
                        KnockbackMultiplier = 1.0f
                    }
                ]
            },
            new FighterAttack
            {
                Name = "SignatureGround",
                Damage = 10,
                Duration = 0.5f,
                AnimationName = "SignatureGround",
                HitboxSequence = [
                    new HitboxSettings
                    {
                        SpawnTime = 0.1f,
                        Hitbox = new Rectangle(-10, -30, 50, 50),
                        DamageMultiplier = 1.0f,
                        KnockbackMultiplier = 1.0f
                    }
                ]
            }
        ];


        public float GroundSpeed { get; set; } = 3000f;
        public float AirSpeed { get; set; } = 1000f;


        public float DashTime { get; set; } = 0.3f;
        public float DashTimer { get; set; } = 0f;
        public float DashCooldown { get; set; } = 5.0f;
        public float DashCooldownTimer { get; set; } = 0f;
        public float DashForce { get; set; } = 10000.0f;


        public float GroundJumpForce { get; set; } = 30000.0f;
        public float AirJumpForce { get; set; } = 20000.0f;
        public float WallJumpForce { get; set; } = 30000.0f;
        public float HighJumpForce { get; set; } = 1000.0f;
        public float JumpTime { get; set; } = 0.3f;
        public int JumpsLeft { get; set; } = 3;
        public int MaxJumps { get; set; } = 3;
        public float JumpTimer { get; set; }

        public FighterDirection Direction { get; set; } = FighterDirection.Right;
    }
}
