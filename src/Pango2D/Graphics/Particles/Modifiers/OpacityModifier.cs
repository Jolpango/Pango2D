using Pango2D.Graphics.Particles.Contracts;
using Pango2D.Graphics.Particles.Interpolations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Pango2D.Graphics.Particles.Modifiers
{
    public struct FloatKeyframe(float time, float scale)
    {
        [JsonInclude]
        public float Time = time;
        [JsonInclude]
        public float Value = scale;
    }
    
    public class OpacityModifier : IParticleModifier
    {
        [JsonIgnore]
        public IInterpelator Interpelator { get; set; } = new LinearInterpelator();
        public List<FloatKeyframe> KeyFrames { get; set; } = [];
        public OpacityModifier() { }
        public OpacityModifier(IEnumerable<FloatKeyframe> keyframes)
        {
            KeyFrames = keyframes.OrderBy(k => k.Time).ToList();
        }

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
            particle.Opacity = Interpelator.Interpolate(prev.Value, next.Value, Math.Clamp(localT, 0f, 1f));
        }
    }
}
