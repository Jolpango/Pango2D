using Microsoft.Xna.Framework;
using Pango2D.Graphics.Particles.Contracts;
using Pango2D.Graphics.Particles.Dispersion;
using Pango2D.Graphics.Particles.Interpolations;
using Pango2D.Graphics.Particles.Modifiers;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Pango2D.Graphics.Particles
{
    public class ParticleEffect
    {
        public List<ParticleEmitter> Emitters { get; set; } = [];
        public bool IsActive { get; set; } = true;
        public Vector2 Position { get; set; } = Vector2.Zero;
        public string Name { get; set; }

        public void Update(GameTime gameTime)
        {
            if (!IsActive) return;
            foreach (var emitter in Emitters)
            {
                emitter.Update(gameTime);
            }
        }
        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            if (!IsActive) return;
            foreach (var emitter in Emitters)
            {
                emitter.Draw(spriteBatch, Position);
            }
        }

        // DTOs for serialization
        private class ParticleEmitterDto
        {
            public string Name { get; set; }
            public int MaxParticles { get; set; }
            public float EmissionRate { get; set; }
            public float Lifetime { get; set; }
            public bool IsActive { get; set; }
            public Vector2Dto Position { get; set; }
            public DispersionDto Dispersion { get; set; }
            public List<ParticleModifierDto> Modifiers { get; set; } = [];
        }

        private class DispersionDto
        {
            public string Type { get; set; }
            public JsonElement Data { get; set; }
        }

        private class ParticleModifierDto
        {
            public string Type { get; set; }
            public JsonElement Data { get; set; }
            public InterpelatorDto Interpelator { get; set; }
        }

        private class Vector2Dto
        {
            public float X { get; set; }
            public float Y { get; set; }
            public static Vector2Dto FromVector2(Vector2 vector)
            {
                return new Vector2Dto { X = vector.X, Y = vector.Y };
            }
            public Vector2 ToVector2()
            {
                return new Vector2(X, Y);
            }
        }

        private class ParticleEffectDto
        {
            public List<ParticleEmitterDto> Emitters { get; set; }
            public bool IsActive { get; set; }
        }
        private class InterpelatorDto
        {
            public string Type { get; set; }
            public JsonElement Data { get; set; }
        }

        public static ParticleEffect FromJson(string json)
        {
            var dto = JsonSerializer.Deserialize<ParticleEffectDto>(json);
            if (dto == null) return new ParticleEffect();

            var effect = new ParticleEffect
            {
                IsActive = dto.IsActive,
                Emitters = []
            };

            foreach (var emitterDto in dto.Emitters)
            {
                var emitter = new ParticleEmitter
                {
                    Name = emitterDto.Name,
                    MaxParticles = emitterDto.MaxParticles,
                    EmissionRate = emitterDto.EmissionRate,
                    Lifetime = emitterDto.Lifetime,
                    IsActive = emitterDto.IsActive,
                    Position = emitterDto.Position.ToVector2(),
                    Dispersion = DeserializeDispersion(emitterDto.Dispersion),
                    Modifiers = DeserializeModifiers(emitterDto.Modifiers)
                };
                effect.Emitters.Add(emitter);
            }

            return effect;
        }

        public static string ToJson(ParticleEffect effect)
        {
            var dto = new ParticleEffectDto
            {
                IsActive = effect.IsActive,
                Emitters = []
            };

            foreach (var emitter in effect.Emitters)
            {
                dto.Emitters.Add(new ParticleEmitterDto
                {
                    Name = emitter.Name,
                    MaxParticles = emitter.MaxParticles,
                    EmissionRate = emitter.EmissionRate,
                    Lifetime = emitter.Lifetime,
                    IsActive = emitter.IsActive,
                    Position = Vector2Dto.FromVector2(emitter.Position),
                    Dispersion = SerializeDispersion(emitter.Dispersion),
                    Modifiers = SerializeModifiers(emitter.Modifiers)
                });
            }

            var options = new JsonSerializerOptions { WriteIndented = true };
            return JsonSerializer.Serialize(dto, options);
        }


        private static DispersionDto SerializeDispersion(IParticleDispersion dispersion)
        {
            if (dispersion == null) return null;
            var type = dispersion.GetType().Name;
            var data = JsonSerializer.SerializeToElement(dispersion, dispersion.GetType());
            return new DispersionDto { Type = type, Data = data };
        }
        
        private static List<ParticleModifierDto> SerializeModifiers(List<IParticleModifier> modifiers)
        {
            var result = new List<ParticleModifierDto>();
            foreach (var modifier in modifiers)
            {
                var serializedModifier = SerializeModifier(modifier);
                if (serializedModifier != null)
                {
                    result.Add(serializedModifier);
                }
            }
            return result;
        }

        private static ParticleModifierDto SerializeModifier(IParticleModifier modifier)
        {
            if (modifier == null) return null;
            var type = modifier.GetType().Name;
            var data = JsonSerializer.SerializeToElement(modifier, modifier.GetType());
            var interpelator = SerializeInterpelator(modifier.Interpelator);
            return new ParticleModifierDto { Type = type, Data = data, Interpelator = interpelator };
        }

        private static IParticleDispersion DeserializeDispersion(DispersionDto dto)
        {
            if (dto == null) return null;

            return dto.Type switch
            {
                "RandomDispersion" => dto.Data.Deserialize<RandomDispersion>(),
                _ => null
            };
        }

        private static List<IParticleModifier> DeserializeModifiers(List<ParticleModifierDto> modifiers)
        {
            var result = new List<IParticleModifier>();
            foreach (var modifier in modifiers)
            {
                var deserializedModifier = DeserializeModifier(modifier);
                if (deserializedModifier != null)
                {
                    result.Add(deserializedModifier);
                }
            }
            return result;
        }

        private static IParticleModifier DeserializeModifier(ParticleModifierDto dto)
        {
            if (dto == null) return null;

            IParticleModifier modifier = dto.Type switch
            {
                "ScaleModifier" => dto.Data.Deserialize<ScaleModifier>(),
                "OpacityModifier" => dto.Data.Deserialize<OpacityModifier>(),
                _ => null
            };

            if (modifier != null && dto.Interpelator != null)
            {
                modifier.Interpelator = DeserializeInterpelator(dto.Interpelator);
            }

            return modifier;
        }

        private static InterpelatorDto SerializeInterpelator(IInterpelator interpelator)
        {
            if (interpelator == null) return null;
            var type = interpelator.GetType().Name;
            var data = JsonSerializer.SerializeToElement(interpelator, interpelator.GetType());
            return new InterpelatorDto { Type = type, Data = data };
        }

        private static IInterpelator DeserializeInterpelator(InterpelatorDto dto)
        {
            if (dto == null) return null;
            return dto.Type switch
            {
                "LinearInterpelator" => dto.Data.Deserialize<LinearInterpelator>(),
                "EaseInInterpelator" => dto.Data.Deserialize<EaseInInterpelator>(),
                // Add more as needed
                _ => null
            };
        }

        public void Emit(Vector2 position, int amount)
        {
            foreach(var emitter in Emitters)
            {
                if (emitter.IsActive)
                {
                    emitter.Emit(amount, position);
                }
            }
        }

        public void Emit(Vector2 position)
        {
            foreach (var emitter in Emitters)
            {
                if (emitter.IsActive)
                {
                    emitter.Emit(position);
                }
            }
        }
    }
}
