using Microsoft.Xna.Framework;
using Pango2D.Graphics.Particles.Contracts;
using Pango2D.Graphics.Particles.Interpolations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pango2D.Graphics.Particles.Modifiers
{
    public class ScaleModifier : IParticleModifier
    {
        public record ScaleKeyframe(float Time, float Scale);

        public readonly List<ScaleKeyframe> Keyframes = [];

        public IInterpelator Interpelator { get; set; } = new LinearInterpelator();

        public ScaleModifier(IEnumerable<ScaleKeyframe> keyframes)
        {
            Keyframes = keyframes.OrderBy(k => k.Time).ToList();
        }

        public void Apply(Particle particle, float deltaTime)
        {
            if (Keyframes.Count == 0) return;

            float t = particle.Lifetime / particle.MaxLifetime;

            ScaleKeyframe prev = Keyframes[0];
            ScaleKeyframe next = Keyframes[^1];

            for (int i = 1; i < Keyframes.Count; i++)
            {
                if (t < Keyframes[i].Time)
                {
                    next = Keyframes[i];
                    prev = Keyframes[i - 1];
                    break;
                }
            }

            float localT = (t - prev.Time) / (next.Time - prev.Time);
            particle.Scale = Interpelator.Interpolate(prev.Scale, next.Scale, Math.Clamp(localT, 0f, 1f));
        }
    }
}
