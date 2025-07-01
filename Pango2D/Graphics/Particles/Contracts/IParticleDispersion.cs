using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pango2D.Graphics.Particles.Contracts
{
    public interface IParticleDispersion
    {
        void Apply(Particle particle, ParticleEmitter emitter);
    }
}
