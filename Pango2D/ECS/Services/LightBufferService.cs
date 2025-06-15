using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pango2D.ECS.Services
{
    public record LightInstance(
        Vector2 WorldPosition,
        Color Color,
        float Radius,
        float Intensity,
        Texture2D Texture
    );
    public class LightBufferService
    {
        public List<LightInstance> ActiveLights { get; } = new List<LightInstance>();
    }
}
