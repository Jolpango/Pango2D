using Microsoft.Xna.Framework;
using Pango2D.Graphics.Sprites;

namespace Pango2D.ECS.Systems.UpdateSystems.Animation
{
    public class AnimationSystem : PostUpdateComponentSystem<SpriteAnimator, Sprite>
    {
        protected override void PostUpdate(GameTime gameTime, Entity entity, SpriteAnimator animation, Sprite sprite)
        {
            animation.Update(gameTime);
            
            sprite.SourceRectangle = animation.CurrentFrameRect;
        }
    }
}
