using Microsoft.Xna.Framework;
using Pango2D.Graphics.Particles.Contracts;
using Pango2D.Graphics.Particles.Interpolations;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Pango2D.Graphics.Particles.Modifiers
{
    public record ColorKeyframe(float Time, Color Color);
    public class ColorModifier : IParticleModifier
    {
        [JsonIgnore]
        public IInterpolator Interpolator { get; set; } = new LinearInterpolator();
        public List<ColorKeyframe> KeyFrames { get; set; } = [];
        public void Apply(Particle particle, float deltaTime)
        {
            if (KeyFrames.Count == 0) return;

            float t = particle.Lifetime / particle.MaxLifetime;

            ColorKeyframe prev = KeyFrames[0];
            ColorKeyframe next = KeyFrames[^1];

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
            float r = Interpolator.Interpolate(prev.Color.R, next.Color.R, Math.Clamp(localT, 0f, 1f));
            float g = Interpolator.Interpolate(prev.Color.G, next.Color.G, Math.Clamp(localT, 0f, 1f));
            float b = Interpolator.Interpolate(prev.Color.B, next.Color.B, Math.Clamp(localT, 0f, 1f));
            float a = Interpolator.Interpolate(prev.Color.A, next.Color.A, Math.Clamp(localT, 0f, 1f));
            particle.Color = new Color((int)r, (int)g, (int)b, (int)a);
        }
    }
}
