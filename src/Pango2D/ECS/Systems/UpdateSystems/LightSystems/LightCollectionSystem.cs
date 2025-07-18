using Microsoft.Xna.Framework;
using Pango2D.ECS.Components;
using Pango2D.ECS.Services;
using Pango2D.ECS.Systems.Contracts;

namespace Pango2D.ECS.Systems.UpdateSystems.LightSystems
{

    public class LightCollectionSystem : IPostUpdateSystem
    {
        private LightBufferService lightBuffer;

        public World World { get; set; }

        public void Initialize()
        {
            lightBuffer = World.Services.Get<LightBufferService>();
        }
        public void PostUpdate(GameTime gameTime)
        {
            lightBuffer.ActiveLights.Clear();
            AddPointLights();
        }
        private void AddPointLights()
        {
            foreach (var (_, light, transform) in World.Query<Light, Transform>((_, light, _) => light.Type == LightType.Point))
            {
                lightBuffer.ActiveLights.Add(new LightInstance(
                    transform.Position + light.Offset,
                    light.Color,
                    light.Radius,
                    light.Intensity,
                    light.Texture));
            }
        }
    }
}
