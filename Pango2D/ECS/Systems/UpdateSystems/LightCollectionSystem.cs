using Microsoft.Xna.Framework;
using Pango2D.ECS.Components;
using Pango2D.ECS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pango2D.ECS.Systems.UpdateSystems
{

    public class LightCollectionSystem : PostUpdateComponentSystem<Light, Transform>
    {
        private LightBufferService lightBuffer;
        public override void Initialize()
        {
            lightBuffer = World.Services.Get<LightBufferService>();
        }
        public override void PostUpdate(GameTime gameTime)
        {
            lightBuffer.ActiveLights.Clear();
            base.PostUpdate(gameTime);
        }
        protected override void PostUpdate(GameTime gameTime, Entity entity, Light light, Transform transform)
        {
            lightBuffer.ActiveLights.Add(new LightInstance(transform.Position + light.Offset, light.Color, light.Radius, light.Intensity));
        }
    }
}
