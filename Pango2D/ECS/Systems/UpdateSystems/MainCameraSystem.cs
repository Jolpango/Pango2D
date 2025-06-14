
using Microsoft.Xna.Framework;
using Pango2D.Core.Contracts;
using Pango2D.ECS.Components;

namespace Pango2D.ECS.Systems.UpdateSystems
{
    public class MainCameraSystem : PostUpdateComponentSystem<MainCameraTarget, Transform>
    {
        protected override void PostUpdate(GameTime gameTime, Entity entity, MainCameraTarget target, Transform transform)
        {

            var camera = World.Services.TryGet<ICameraService>();
            if (camera is null)
                return;

            camera.SetPosition(transform.Position);
        }
    }
}
