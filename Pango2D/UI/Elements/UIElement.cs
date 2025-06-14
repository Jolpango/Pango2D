using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.Core.Input.Contracts;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace Pango2D.UI.Elements
{
    public class UIElement
    {
        private Point position;
        private Point size;
        private Point desiredSize;
        private Point padding = Point.Zero;

        protected bool isLayoutDirty = true;

        /// <summary>
        /// Identifier for the UI element, used for referencing.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Indicates whether the UI element is currently visible.
        /// </summary>
        public bool IsVisible { get; set; } = true;

        /// <summary>
        /// Indicates whether the UI element is currently enabled and can receive input or be interacted with.
        /// </summary>
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Actual calculated size of the UI element.
        /// </summary>
        public Point Size
        {
            get => size;
            protected set => size = value;
        }

        /// <summary>
        /// Gets or sets the desired size of the element.
        /// </summary>
        public Point DesiredSize
        {
            get => desiredSize;
            protected set => desiredSize = value;
        }

        /// <summary>
        /// Position of the UI element relative to its parent.
        /// </summary>
        public Point Position
        {
            get => position;
            set
            {
                if (position != value)
                {
                    position = value;
                    InvalidateLayout();
                }
            }
        }

        /// <summary>
        /// Gets or sets the padding applied to the UI element.
        /// </summary>
        /// <remarks>Changing the padding will invalidate the current layout, triggering a
        /// recalculation.</remarks>
        public Point Padding
        {
            get => padding;
            set
            {
                if (padding != value)
                {
                    padding = value;
                    InvalidateLayout();
                }
            }
        }

        /// <summary>
        /// Gets the parent <see cref="UIElement"/> of this element in the visual tree.
        /// </summary>
        public UIElement Parent { get; private set; }

        /// <summary>
        /// Gets the collection of child UI elements contained within this element.
        /// </summary>
        public List<UIElement> Children { get; } = new();

        private List<UIBinding> bindings = new();

        public void AddBinding(string propertyName, Func<object> valueGetter)
        {
            bindings.Add(new UIBinding(propertyName, valueGetter));
        }

        public virtual void UpdateBindings()
        {
            foreach (var binding in bindings)
            {
                var property = GetType().GetProperty(binding.PropertyName);
                if (property != null)
                {
                    var newValue = binding.ValueGetter();
                    property.SetValue(this, newValue);
                }
            }

            foreach (var child in Children)
                child.UpdateBindings();
        }

        /// <summary>
        /// Marks the current layout as invalid and propagates the invalidation to the parent.
        /// </summary>
        /// <remarks>This method sets the layout state to dirty, indicating that it needs to be
        /// recalculated. If the current object has a parent, the invalidation is also propagated to the parent. Use
        /// this method when changes to the layout require a refresh or recalculation.</remarks>
        public void InvalidateLayout()
        {
            isLayoutDirty = true;
            Parent?.InvalidateLayout();
        }

        /// <summary>
        /// Adds a child element to the current UI element.
        /// </summary>
        /// <remarks>This method sets the parent of the specified child element to the current element,
        /// adds the child to the collection of children,  and invalidates the layout to ensure the UI is
        /// updated.</remarks>
        /// <param name="child">The <see cref="UIElement"/> to add as a child. Cannot be <see langword="null"/>.</param>
        public void AddChild(UIElement child)
        {
            child.Parent = this;
            Children.Add(child);
            InvalidateLayout();
        }

        /// <summary>
        /// Measures the size of the current element and its child elements.
        /// </summary>
        /// <remarks>This method iterates through all child elements and invokes their <see
        /// cref="Measure"/> method. After measuring the children, it sets the <see cref="DesiredSize"/> of the current
        /// element to its <see cref="Size"/>.</remarks>
        public virtual void Measure()
        {
            foreach (var child in Children)
                child.Measure();

            DesiredSize = Size;
        }

        /// <summary>
        /// Arranges the current element and its child elements at the specified offset.
        /// </summary>
        /// <param name="offset"></param>
        public virtual void Arrange(Point offset)
        {
            Position = offset;

            foreach (var child in Children)
                child.Arrange(new Point(Position.X + Padding.X, Position.Y + Padding.Y));

            isLayoutDirty = false;
        }

        /// <summary>
        /// Updates the layout if it is marked as dirty.
        /// </summary>
        /// <remarks>This method checks whether the layout requires updating and, if so, performs the
        /// necessary measurement and arrangement operations. It ensures that the layout is recalculated only when
        /// flagged as dirty.</remarks>
        public void UpdateLayoutIfDirty()
        {
            if (isLayoutDirty)
            {
                Measure();
                Arrange(Position);
            }
        }

        /// <summary>
        /// Updates the current state of the object and its children based on the provided game time and input.
        /// </summary>
        /// <remarks>This method iterates through all child objects and invokes their <see cref="Update"/>
        /// method,  ensuring that each child is updated with the same game time and input data.</remarks>
        /// <param name="time">The current game time, used to calculate updates relative to the game's progression.</param>
        /// <param name="input">An input provider that supplies user input data for processing during the update.</param>
        public virtual void Update(GameTime time, IInputProvider input)
        {
            if (!IsEnabled)
                return;
            foreach (var child in Children)
                child.Update(time, input);
        }

        /// <summary>
        /// Draws the current object and its child elements using the specified <see cref="SpriteBatch"/>.
        /// </summary>
        /// <remarks>This method iterates through all child elements and invokes their <see cref="Draw"/>
        /// method, ensuring that each child is rendered using the provided <see cref="SpriteBatch"/>.</remarks>
        /// <param name="spriteBatch">The <see cref="SpriteBatch"/> used to render the object and its children.</param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (!IsVisible)
                return;
            foreach (var child in Children)
                child.Draw(spriteBatch);
        }
    }

}