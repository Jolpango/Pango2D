using Microsoft.Xna.Framework;
using Pango2D.Graphics.Particles.Contracts;
using System;

namespace Pango2D.Graphics.Particles.Dispersion
{
    public class RandomDispersion : IParticleDispersion
    {
        private readonly Random random = new();
        public float MinSpeed { get; set; }
        public float MaxSpeed { get; set; }
        public float MinAngle { get; set; } // In radians
        public float MaxAngle { get; set; } // In radians
        public RandomDispersion(float minSpeed, float maxSpeed, float minAngle = 0f, float maxAngle = MathF.Tau)
        {
            MinSpeed = minSpeed;
            MaxSpeed = maxSpeed;
            MinAngle = minAngle;
            MaxAngle = maxAngle;
        }

        public void Apply(Particle particle, ParticleEmitter emitter)
        {
            float angle = (float)(MinAngle + random.NextDouble() * (MaxAngle - MinAngle));
            float speed = (float)(MinSpeed + random.NextDouble() * (MaxSpeed - MinSpeed));
            Vector2 direction = new Vector2(MathF.Cos(angle), MathF.Sin(angle));
            particle.Velocity = direction * speed;
        }
    }
}
