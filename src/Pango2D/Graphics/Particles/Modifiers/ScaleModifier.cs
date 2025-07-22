using Microsoft.Xna.Framework;
using Pango2D.Graphics.Particles.Contracts;
using Pango2D.Graphics.Particles.Interpolations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Pango2D.Graphics.Particles.Modifiers
{
    public class ScaleModifier : IParticleModifier
    {
        [JsonIgnore]
        public IInterpolator Interpolator { get; set; } = new LinearInterpolator();
        public List<FloatKeyframe> Keyframes { get; set; } = [];
        public ScaleModifier() { }
        public ScaleModifier(IEnumerable<FloatKeyframe> keyframes)
        {
            Keyframes = keyframes.OrderBy(k => k.Time).ToList();
        }

        public void Apply(Particle particle, float deltaTime)
        {
            if (Keyframes.Count == 0) return;

            float t = particle.Lifetime / particle.MaxLifetime;

            FloatKeyframe prev = Keyframes[0];
            FloatKeyframe next = Keyframes[^1];

            for (int i = 1; i < Keyframes.Count; i++)
            {
                if (t < Keyframes[i].Time)
                {
                    next = Keyframes[i];
                    prev = Keyframes[i - 1];
                    break;
                }
            }

            float localT = (next.Time - prev.Time) != 0f ? (t - prev.Time) / (next.Time - prev.Time) : 0f;
            particle.Scale = Interpolator.Interpolate(prev.Value, next.Value, Math.Clamp(localT, 0f, 1f));
        }
    }
}
