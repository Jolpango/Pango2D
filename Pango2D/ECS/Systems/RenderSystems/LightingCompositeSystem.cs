using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.Core.Graphics;
using Pango2D.ECS.Services;
using Pango2D.ECS.Systems.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pango2D.ECS.Systems.RenderSystems
{
    public class LightingCompositeSystem : IDrawSystem
    {
        public RenderPhase RenderPhase { get; set; } = RenderPhase.PostProcess;
        public World World { get; set; }
        private LightBufferService lightBuffer;
        public void Initialize()
        {
            lightBuffer = World.Services.Get<LightBufferService>();
        }

        public void BeginDraw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public void EndDraw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }
    }
}
