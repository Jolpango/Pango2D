using Microsoft.Xna.Framework;
using Pango2D.ECS.Components.CameraComponents;
using Pango2D.ECS.Systems.Contracts;
using System;

namespace Pango2D.ECS.Systems.UpdateSystems.CameraSystems
{
    public class CameraShakeSystem : IPostUpdateSystem
    {
        public World World { get; set; }

        public void Initialize() { }

        public void PostUpdate(GameTime gameTime)
        {
            foreach (var (_, camera, shake) in World.Query<Camera, CameraShake>())
            {
                if (shake.TimeLeft > 0)
                {
                    shake.TimeLeft -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    var random = new Random();
                    shake.Offset = new Vector2(
                        (float)(random.NextDouble() * 2 - 1) * shake.Intensity,
                        (float)(random.NextDouble() * 2 - 1) * shake.Intensity
                    );
                    shake.RotationalOffset = (float)(random.NextDouble() * 2 - 1) * shake.RotationalIntensity;
                }
                else
                {
                    shake.Offset = Vector2.Zero;
                    shake.RotationalOffset = 0f;
                    camera.Rotation = 0f;
                }
                camera.Position += shake.Offset;
                camera.Rotation += shake.RotationalOffset;
            }
        }
    }
}
