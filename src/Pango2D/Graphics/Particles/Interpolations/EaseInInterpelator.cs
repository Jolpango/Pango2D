using Microsoft.Xna.Framework;
using Pango2D.Graphics.Particles.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pango2D.Graphics.Particles.Interpolations
{
    public class EaseInInterpelator : IInterpelator
    {
        public float Interpolate(float start, float end, float t) => MathHelper.Lerp(start, end, t * t);
    }
}
