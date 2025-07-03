using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pango2D.Graphics.Particles.Contracts
{
    public interface IParticleModifier
    {
        public IInterpelator Interpelator { get; set; }
        public void Apply(Particle particle, float deltaTime);
    }
}
