using Pango2D.Graphics.Particles.Contracts;
using Pango2D.Graphics.Particles.Interpolations;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Pango2D.Graphics.Particles.Modifiers
{
    public class AngularVelocityModifier : IParticleModifier
    {
        [JsonIgnore]
        public IInterpolator Interpolator { get; set; } = new LinearInterpolator();
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

            float localT = (next.Time - prev.Time) != 0f ? (t - prev.Time) / (next.Time - prev.Time) : 0f;
            particle.AngularVelocity = Interpolator.Interpolate(prev.Value, next.Value, Math.Clamp(localT, 0f, 1f));
        }
    }
}
