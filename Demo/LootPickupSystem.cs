using Microsoft.Xna.Framework;
using Pango2D.ECS;
using Pango2D.ECS.Components;
using Pango2D.ECS.Components.Commands;
using Pango2D.ECS.Systems;
using System.Linq;

namespace Demo
{
    public class LootPickupSystem : PostUpdateComponentSystem<CollisionEvent, CoinComponent>
    {
        protected override void PostUpdate(GameTime gameTime, Entity entity, CollisionEvent c1, CoinComponent c2)
        {
            World.DestroyEntity(entity);
            PlayerComponent player = World.Query<PlayerComponent>().FirstOrDefault().Item2;
            player.Gold += c2.Value;
            World.AddComponent(c1.Other, new SoundEffectCommand()
            {
                SoundEffectName = "bottle",
                Volume = 1f,
            });
        }
    }
}
