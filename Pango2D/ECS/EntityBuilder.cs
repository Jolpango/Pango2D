using Microsoft.Xna.Framework;
using Pango2D.ECS.Components.Contracts;
using Pango2D.ECS.Components.Physics;
using System;

namespace Pango2D.ECS
{
    public class EntityBuilder
    {
        private readonly Entity entity;
        private readonly World world;
        public EntityBuilder(World world)
        {
            this.world = world ?? throw new ArgumentNullException(nameof(world));
            entity = world.CreateEntity();
        }
        public EntityBuilder AddComponent<T>(T component) where T : IComponent
        {
            world.AddComponent(entity, component);
            return this;
        }
        public EntityBuilder AddRigidBody(
            Vector2 velocity = default,
            Vector2 acceleration = default,
            float mass = 1f)
        {
            world.AddComponent(entity, new Velocity(velocity));
            world.AddComponent(entity, new Acceleration(acceleration));
            world.AddComponent(entity, new Mass(mass));
            return this;
        }
        public Entity Build()
        {
            return entity;
        }
    }
}
