using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.Core.Graphics;
using Pango2D.ECS.Components.Contracts;
using Pango2D.Graphics.Particles.Contracts;
using Pango2D.Graphics.Particles.Interpolations;
using System.Collections.Generic;
using System.Linq;

namespace Pango2D.Graphics.Particles
{
    public class ParticleEmitter : IComponent
    {
        private float emissionAccumulator = 0f;
        public string Name { get; set; }
        public int MaxParticles { get; set; } = 1000;
        public float EmissionRate { get; set; } = 10f;
        public float Lifetime { get; set; } = 5f;
        public bool IsActive { get; set; } = true;
        public Vector2 Position { get; set; } = Vector2.Zero;
        public IParticleDispersion Dispersion { get; set; }
        public List<IParticleModifier> Modifiers { get; set; } = [];
        public List<Particle> Particles { get; private set; } = [];
        public Texture2D Texture { get; set; }
        public bool IsEmitting { get; set; }

        public ParticleEmitter()
        {
            for (int i = 0; i < MaxParticles; i++)
            {
                Particles.Add(new Particle { IsActive = false });
            }
        }

        public void Update(GameTime gameTime)
        {
            if (!IsActive) return;
            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            emissionAccumulator += EmissionRate * deltaSeconds;

            if (IsEmitting)
            {
                while (emissionAccumulator >= 1f)
                {
                    SpawnParticle(Position);
                    emissionAccumulator -= 1f;
                }
            }

            foreach (var particle in Particles.Where(p => p.IsActive))
            {
                foreach (var modifier in Modifiers)
                    modifier.Apply(particle, deltaSeconds);

                particle.Position += particle.Velocity * deltaSeconds;
                particle.Velocity += particle.Acceleration * deltaSeconds;
                particle.Rotation += particle.AngularVelocity * deltaSeconds;
                particle.Lifetime += deltaSeconds;

                if (particle.Lifetime >= particle.MaxLifetime)
                    particle.IsActive = false;
            }
        }

        public void Emit(int count)
        {
            for (int i = 0; i < count; i++)
            {
                SpawnParticleAbsolute(Position);
            }
        }
        public void Emit(Vector2 position)
        {
            for (int i = 0; i < EmissionRate; i++)
            {
                SpawnParticleAbsolute(position);
            }
        }
        public void Emit(int count, Vector2 position)
        {
            for (int i = 0; i < count; i++)
            {
                SpawnParticleAbsolute(position);
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 offset) {
            if (!IsActive) return;
            foreach (var particle in Particles.Where(p => p.IsActive))
            {
                particle.Draw(spriteBatch, offset);
            }
        }

        private void SpawnParticle(Vector2 position)
        {
            var particle = Particles.FirstOrDefault(p => !p.IsActive);
            if (particle == null) return;

            particle.Position = position;
            Dispersion.Apply(particle, this);
            particle.Rotation = 0f;
            particle.AngularVelocity = 0f;
            particle.Scale = 1f;
            particle.Color = Color.White;
            particle.Lifetime = 0f;
            particle.MaxLifetime = Lifetime;
            particle.IsActive = true;
            particle.Texture = Texture ?? TextureCache.White4;
        }

        private void SpawnParticleAbsolute(Vector2 position)
        {
            var particle = Particles.FirstOrDefault(p => !p.IsActive);
            if (particle == null) return;

            particle.Position = position;
            Dispersion.Apply(particle, this);
            particle.Rotation = 0f;
            particle.AngularVelocity = 0f;
            particle.Scale = 1f;
            particle.Color = Color.White;
            particle.Lifetime = 0f;
            particle.MaxLifetime = Lifetime;
            particle.IsActive = true;
            particle.Texture = Texture ?? TextureCache.White4;
        }
    }
}
