using Pango2D.ECS.Components.Contracts;
using Pango2D.Graphics.Sprites;

namespace Pango2D.ECS.Components
{
    public class SpriteComponent : IComponent
    {
        public Sprite Sprite { get; set; }

        public SpriteComponent(Sprite sprite)
        {
            Sprite = sprite;
        }
    }
}
