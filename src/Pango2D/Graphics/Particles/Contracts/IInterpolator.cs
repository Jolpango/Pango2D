
namespace Pango2D.Graphics.Particles.Contracts
{
    public interface IInterpolator
    {
        public float Interpolate(float start, float end, float t);
    }
}
