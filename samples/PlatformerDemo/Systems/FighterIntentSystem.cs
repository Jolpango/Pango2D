using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Pango2D.Core.Input;
using Pango2D.ECS;
using Pango2D.ECS.Components.Contracts;
using Pango2D.ECS.Systems.Contracts;
using PlatformerDemo.Components;

namespace PlatformerDemo.Systems
{
    public enum AttackType
    {
        Normal,
        Signature
    }
    public class WantsToAttackComponent : IComponent
    {
        public AttackType Type { get; set; } = AttackType.Normal;
    }

    public class WantsToJumpComponent : IComponent { }
    public class WantsToHighJumpComponent : IComponent { }

    public class WantsToMoveComponent : IComponent
    {
        public Vector2 Direction { get; set; } = Vector2.Zero;
    }
    public class WantsToDashComponent : IComponent
    {
        public Vector2 Direction { get; set; } = Vector2.Zero;
    }

    public class FighterIntentSystem : IPreUpdateSystem
    {
        public World World { get; set; }

        private GamePadManager gamePad;

        public void Initialize()
        {
            gamePad = World.Services.GamePadManager;
        }

        public void PreUpdate(GameTime gameTime)
        {
            foreach(var(entity, _, controls) in World.Query<FighterComponent, FighterControls>())
            {
                ClearIntent(entity);
                CheckAttackIntent(entity, controls);
                CheckJumpIntent(entity, controls);
                CheckLongJumpIntent(entity, controls);
                CheckMoveIntent(entity, controls);
                CheckDashIntent(entity, controls);
            }
        }

        private void CheckDashIntent(Entity entity, FighterControls controls)
        {
            if (gamePad.IsButtonPressed(controls.PlayerIndex, controls.DashLeftButton))
            {
                World.AddComponent(entity, new WantsToDashComponent { Direction = new Vector2(-1, 0) });
            }
            else if (gamePad.IsButtonPressed(controls.PlayerIndex, controls.DashRightButton))
            {
                World.AddComponent(entity, new WantsToDashComponent { Direction = new Vector2(1, 0) });
            }
        }

        private void CheckMoveIntent(Entity entity, FighterControls controls)
        {
            var direction = gamePad.GetLeftThumbstick(controls.PlayerIndex);
            direction.X += gamePad.IsButtonDown(controls.PlayerIndex, Buttons.DPadLeft) ? -1 : 0;
            direction.X += gamePad.IsButtonDown(controls.PlayerIndex, Buttons.DPadRight) ? 1 : 0;
            direction.Y += gamePad.IsButtonDown(controls.PlayerIndex, Buttons.DPadUp) ? -1 : 0;
            direction.Y += gamePad.IsButtonDown(controls.PlayerIndex, Buttons.DPadDown) ? 1 : 0;

            if (direction.LengthSquared() > 0)
            {
                if(direction.Length() > 1)
                {
                    direction.Normalize();
                }
                World.AddComponent(entity, new WantsToMoveComponent { Direction = direction });
            }
        }
        private void CheckLongJumpIntent(Entity entity, FighterControls controls)
        {
            if (gamePad.IsButtonDown(controls.PlayerIndex, controls.JumpButton))
            {
                World.AddComponent(entity, new WantsToHighJumpComponent());
            }
        }
        private void CheckJumpIntent(Entity entity, FighterControls controls)
        {
            if (gamePad.IsButtonPressed(controls.PlayerIndex, controls.JumpButton))
            {
                World.AddComponent(entity, new WantsToJumpComponent());
            }
        }

        private void CheckAttackIntent(Entity entity, FighterControls controls)
        {
            if (gamePad.IsButtonPressed(controls.PlayerIndex, controls.AttackButton))
            {
                World.AddComponent(entity, new WantsToAttackComponent() { Type = AttackType.Normal});
            }
            else if (gamePad.IsButtonPressed(controls.PlayerIndex, controls.SignatureButton))
            {
                World.AddComponent(entity, new WantsToAttackComponent() { Type = AttackType.Signature });
            }
        }

        private void ClearIntent(Entity entity)
        {
            World.RemoveComponent<WantsToAttackComponent>(entity);
            World.RemoveComponent<WantsToJumpComponent>(entity);
            World.RemoveComponent<WantsToHighJumpComponent>(entity);
            World.RemoveComponent<WantsToMoveComponent>(entity);
            World.RemoveComponent<WantsToDashComponent>(entity);
        }
    }
}
