using Microsoft.Xna.Framework;
using Pango2D.Graphics.Particles.Contracts;
using Pango2D.Graphics.Particles.Interpolations;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Pango2D.Graphics.Particles.Modifiers
{
    public struct ColorKeyframe(float time, Color color)
    {
        [JsonInclude]
        public Color Color = color;
        [JsonInclude]
        public float Time = time;
    }
    public class ColorModifier : IParticleModifier
    {
        [JsonIgnore]
        public IInterpelator Interpelator { get; set; } = new LinearInterpelator();
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

            float localT = (t - prev.Time) / (next.Time - prev.Time);
            float r = Interpelator.Interpolate(prev.Color.R, next.Color.R, Math.Clamp(localT, 0f, 1f));
            float g = Interpelator.Interpolate(prev.Color.G, next.Color.G, Math.Clamp(localT, 0f, 1f));
            float b = Interpelator.Interpolate(prev.Color.B, next.Color.B, Math.Clamp(localT, 0f, 1f));
            float a = Interpelator.Interpolate(prev.Color.A, next.Color.A, Math.Clamp(localT, 0f, 1f));
            particle.Color = new Color((int)r, (int)g, (int)b, (int)a);
        }
    }
}
