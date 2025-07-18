using Microsoft.Xna.Framework;
using Pango2D.ECS.Components.Contracts;

namespace Pango2D.ECS.Components.CameraComponents
{
    public class CameraShake : IComponent
    {
        public float Intensity { get; set; }
        public float RotationalIntensity { get; set; }
        public float RotationalOffset { get; set; }
        public float Duration { get; set; }
        public float TimeLeft { get; set; }
        public Vector2 Offset { get; set; }
    }
}
