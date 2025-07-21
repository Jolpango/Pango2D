using Pango2D.Graphics.Particles.Contracts;
using Pango2D.Graphics.Particles.Interpolations;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Pango2D.Graphics.Particles.Modifiers
{
    internal class AngularVelocityModifier : IParticleModifier
    {
        [JsonIgnore]
        public IInterpelator Interpelator { get; set; } = new LinearInterpelator();
        public List<FloatKeyframe> KeyFrames { get; set; } = [];

        public void Apply(Particle particle, float deltaTime)
        {
            if (KeyFrames.Count == 0) return;

            float t = particle.Lifetime / particle.MaxLifetime;

            FloatKeyframe prev = KeyFrames[0];
            FloatKeyframe next = KeyFrames[^1];

            for (int i = 1; i < KeyFrames.Count; i++)
            {
                if (t < KeyFrames[i].Time)
                {
                    next = KeyFrames[i];
                    prev = KeyFrames[i - 1];
                    break;
                }
            }

            float localT = (t - prev.Time) / (next.Time - prev.Time);
            particle.AngularVelocity = Interpelator.Interpolate(prev.Scale, next.Scale, Math.Clamp(localT, 0f, 1f));
        }
    }
}
