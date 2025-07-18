using Microsoft.Xna.Framework;
using Pango2D.ECS.Components.Contracts;

namespace Pango2D.ECS.Components
{
    /// <summary>
    /// Represents the type of collider used in collision detection.
    /// </summary>
    /// <remarks>This enumeration defines the available collider types for use in physics calculations or
    /// collision detection systems.</remarks>
    public enum ColliderType
    {
        AABB,
        Circle
    }

    /// <summary>
    /// Defines the behavior of a collider in a physics simulation.
    /// </summary>
    /// <remarks>This enumeration specifies how a collider interacts with other objects in the simulation. Use
    /// <see cref="Static"/> for immovable objects, <see cref="Dynamic"/> for objects that respond to forces, <see
    /// cref="Trigger"/> for colliders that detect overlaps without physical interaction, and <see cref="Transient"/>
    /// for temporary or special-purpose colliders.</remarks>
    public enum ColliderBehavior
    {
        Static,
        Dynamic,
        Trigger,
        Transient
    }

    /// <summary>
    /// Represents a component that defines collision detection behavior for an entity.
    /// </summary>
    /// <remarks>The <see cref="Collider"/> class provides properties to configure the type, shape, and
    /// behavior of the collider. It supports multiple collision types, such as axis-aligned bounding boxes (AABB) and
    /// circles(not yet), and allows customization of collision properties such as static or trigger behavior.</remarks>
    public class Collider : IComponent
    {
        /// <summary>
        /// Gets or sets the type of collider used for collision detection.
        /// </summary>
        public ColliderType Type { get; set; } = ColliderType.AABB;

        /// <summary>
        /// AABB bounds of the collider in local space.
        /// </summary>
        public Rectangle Bounds { get; set; }

        /// <summary>
        /// Circle radius of the collider in local space.
        /// </summary>
        public float Radius { get; set; } = 0f;

        /// <summary>
        /// Gets or sets the offset from the center point. Used by the circle collider to define its position
        /// </summary>
        public Vector2 CenterOffset { get; set; } = Vector2.Zero;

        /// <summary>
        /// Gets or sets the behavior of the collider, which determines how it interacts with other objects.
        /// </summary>
        public ColliderBehavior Behavior { get; set; } = ColliderBehavior.Dynamic;

    }
}
