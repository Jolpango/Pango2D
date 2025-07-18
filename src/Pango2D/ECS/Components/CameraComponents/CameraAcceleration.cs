using Pango2D.ECS.Components.Contracts;

namespace Pango2D.ECS.Components.CameraComponents
{
    public class CameraAcceleration : IComponent
    {
        public float Acceleration { get; set; } = 5f;
        public float MaxSpeed { get; set; } = 2500f;
        public float Friction { get; set; } = 0.75f;
    }
}
