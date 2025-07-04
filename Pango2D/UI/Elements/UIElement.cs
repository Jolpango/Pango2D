using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.Core.Graphics;
using Pango2D.Core.Input;
using Pango2D.Core.Input.Contracts;
using Pango2D.UI.Elements.Contracts;
using System;
using System.Collections.Generic;

namespace Pango2D.UI.Elements
{
    public struct NineSlice
    {
        public int Left, Top, Right, Bottom;
        public float Scale;
        public NineSlice(int left, int top, int right, int bottom, float scale = 4)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
            Scale = scale;
        }
    }
    /// <summary>
    /// Represents the options for how an image should be displayed within a UI element.
    /// </summary>
    public enum ImageOptions
    {
        None,
        Fill,
        Center
    }

    /// <summary>
    /// Represents the alignment options for text within a UI element.
    /// </summary>
    public enum TextAlign
    {
        Left,
        Center,
        Right
    }

    public enum ElementAlignment
    {
        None,
        Start,
        Center,
        End
    }

    /// <summary>
    /// Represents the style of a font used in UI elements.
    /// </summary>
    public enum FontStyle
    {
        Regular,
        Bold,
        Italic
    }

    public enum AnchorPoint
    {
        None,
        TopLeft,
        TopCenter,
        TopRight,
        MiddleLeft,
        MiddleCenter,
        MiddleRight,
        BottomLeft,
        BottomCenter,
        BottomRight
    }

    /// <summary>
    /// Base class for all UI elements in the Pango2D framework.
    /// </summary>
    public class UIElement
    {
        private Point position;
        private Point size;
        private Point minSize = Point.Zero;
        private Point padding = Point.Zero;
        private ElementAlignment elementAlignment = ElementAlignment.None;
        private AnchorPoint anchor = AnchorPoint.None;
        private string text = string.Empty;
        private string font = "Default";
        private float opacity = 1.0f;
        private NineSlice? nineSlice = null;
        private List<UIBinding> bindings = new();

        public UIElement(GameWindow gameWindow, FontRegistry fontRegistry)
        {
            this.gameWindow = gameWindow ?? throw new ArgumentNullException(nameof(gameWindow));
            this.fontRegistry = fontRegistry ?? throw new ArgumentNullException(nameof(fontRegistry));
            this.gameWindow.ClientSizeChanged += (sender, args) =>
            {
                InvalidateLayout();
            };
        }

        /// <summary>
        /// Registry for managing fonts used in the UI elements.
        /// </summary>
        protected readonly FontRegistry fontRegistry;

        /// <summary>
        /// Game window reference for accessing game-related properties and methods.
        /// </summary>
        protected readonly GameWindow gameWindow;

        /// <summary>
        /// Indicates whether the mouse was previously over this UI element last frame.
        /// </summary>
        protected bool wasMouseOver = false;

        /// <summary>
        /// Indicates whether the mouse button is currently pressed over this UI element.
        /// </summary>
        protected bool isMousePressing = false;

        /// <summary>
        /// Indicates whether the layout of the UI element is dirty and needs to be recalculated.
        /// </summary>
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

        public AnchorPoint Anchor
        {
            get => anchor;
            set
            {
                if (anchor != value)
                {
                    anchor = value;
                    InvalidateLayout();
                }
            }
        }

        public ElementAlignment AlignSelf
        {
            get => elementAlignment;
            set
            {
                if (elementAlignment != value)
                {
                    elementAlignment = value;
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
        /// Gets or sets text associated with the UI element
        /// </summary>
        public string Text
        {
            get => text;
            set
            {
                if (text != value)
                {
                    text = value;
                    InvalidateLayout();
                }
            }
        }

        public float Opacity
        {
            get => opacity;
            set
            {
                if (opacity != value)
                {
                    opacity = Math.Clamp(value, 0f, 1f);
                }
            }
        }

        public NineSlice? NineSlice
        {
            get => nineSlice;
            set
            {
                if (!nineSlice.Equals(value))
                {
                    nineSlice = value;
                    InvalidateLayout();
                }
            }
        }

        /// <summary>
        /// Gets or sets the font size used for rendering text in the UI element.
        /// </summary>
        public int FontSize { get; set; } = 12;

        /// <summary>
        /// Gets or sets the font style used for rendering text in the UI element.
        /// </summary>
        public FontStyle FontStyle { get; set; } = FontStyle.Regular;

        /// <summary>
        /// Gets or sets the color of the text displayed by the UI element.
        /// </summary>
        public Color FontColor { get; set; } = Color.Black;

        /// <summary>
        /// Gets or sets the text alignment for the UI element.
        /// </summary>
        public TextAlign TextAlign { get; set; } = TextAlign.Left;

        /// <summary>
        /// Gets or sets the background image of the UI element.
        /// </summary>
        public Texture2D BackgroundImage { get; set; }

        /// <summary>
        /// Gets or sets the options for how the background image is displayed.
        /// </summary>
        public ImageOptions ImageOptions { get; set; } = ImageOptions.None;

        /// <summary>
        /// Gets or sets the background color of the UI element.
        /// </summary>
        public Color BackgroundColor { get; set; } = Color.Transparent;

        /// <summary>
        /// Gets or sets font associated with this element, default font is "Default".
        /// </summary>
        public string Font
        {
            get => font;
            set
            {
                if (font != value)
                {
                    font = value;
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

        /// <summary>
        /// Gets or sets the minimum size for this UI element.
        /// </summary>
        public Point MinSize
        {
            get => minSize;
            set
            {
                if (minSize != value)
                {
                    minSize = value;
                    InvalidateLayout();
                }
            }
        }

        /// <summary>
        /// Event triggered when the UI element is clicked.
        /// </summary>
        public event Action<UIElement> OnClick;

        /// <summary>
        /// Event triggered when the mouse enters the UI element.
        /// </summary>
        public event Action<UIElement> OnMouseEnter;

        /// <summary>
        /// Event triggered when the mouse leaves the UI element.
        /// </summary>
        public event Action<UIElement> OnMouseLeave;

        /// <summary>
        /// Adds a binding to the UI element that updates a property based on a value getter function.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="valueGetter"></param>
        public void AddBinding(string propertyName, Func<object> valueGetter)
        {
            bindings.Add(new UIBinding(propertyName, valueGetter));
        }

        /// <summary>
        /// Sets the children of this UI element using a factory function to create each child from a collection of items.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="factory"></param>
        public void SetChildren<T>(IEnumerable<T> items, Func<T, UIElement> factory)
        {
            Children.Clear();
            foreach (var item in items)
            {
                var child = factory(item);
                AddChild(child);
            }
            InvalidateLayout();
        }

        /// <summary>
        /// Updates the bindings of the UI element, setting properties based on their associated value getters.
        /// </summary>
        public virtual void UpdateBindings()
        {
            foreach (var binding in bindings)
            {
                var property = GetType().GetProperty(binding.PropertyName);
                if (property != null)
                {
                    var newValue = binding.ValueGetter();
                    var parsed = newValue;
                    property.SetValue(this, parsed);
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
            if (Children.Count > 0)
            {
                foreach (var child in Children)
                    child.Measure();
            }
            else
            {
                var font = fontRegistry.Get(Font, FontSize, FontStyle);
                var textSize = font.MeasureString(Text);
                Size = new Point((int)textSize.X + Padding.X * 2, (int)textSize.Y + Padding.Y * 2);
            }

            // Enforce MinSize
            Size = new Point(
                Math.Max(Size.X, MinSize.X),
                Math.Max(Size.Y, MinSize.Y)
            );
        }

        /// <summary>
        /// Arranges the current element and its child elements at the specified offset.
        /// </summary>
        /// <param name="offset"></param>
        public virtual void Arrange(Point offset)
        {
            if(Parent is null || Parent is not ILayoutContainer)
            {
                Position = CalculateAnchoredPosition(offset, Size, Anchor, Parent?.Size ?? new Point(gameWindow.ClientBounds.Width, gameWindow.ClientBounds.Height));
            }
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
                Arrange(Point.Zero);
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
            UpdateMouseStates(input);
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
            DrawBackgroundColor(spriteBatch);
            DrawBackgroundImage(spriteBatch);
            foreach (var child in Children)
                child.Draw(spriteBatch);
            DrawText(spriteBatch);
        }

        /// <summary>
        /// Draws the background color of the UI element using the specified <see cref="SpriteBatch"/>.
        /// </summary>
        /// <param name="spriteBatch"></param>
        protected virtual void DrawBackgroundColor(SpriteBatch spriteBatch)
        {
            if(BackgroundColor != Color.Transparent)
            {
                spriteBatch.Draw(TextureCache.White, new Rectangle(Position, Size), BackgroundColor * Opacity);
            }
        }

        /// <summary>
        /// Draws the background image of the UI element based on the specified <see cref="ImageOptions"/>.
        /// </summary>
        /// <param name="spriteBatch"></param>
        protected virtual void DrawBackgroundImage(SpriteBatch spriteBatch)
        {
            if (BackgroundImage != null)
            {
                var destinationRectangle = new Rectangle(Position, Size);
                if (NineSlice.HasValue)
                {
                    DrawNineSlice(spriteBatch, BackgroundImage, destinationRectangle, NineSlice.Value, Color.White);
                }
                else
                {
                    switch (ImageOptions)
                {
                    case ImageOptions.Fill:
                        spriteBatch.Draw(BackgroundImage, destinationRectangle, Color.White);
                        break;
                    case ImageOptions.Center:
                        var aspectRatio = (float)BackgroundImage.Width / BackgroundImage.Height;
                        var scaledWidth = Size.X;
                        var scaledHeight = Size.Y;

                        if (aspectRatio > 1)
                        {
                            // Image is wider than tall, scale by width  
                            scaledHeight = (int)(Size.X / aspectRatio);
                        }
                        else
                        {
                            // Image is taller than wide, scale by height  
                            scaledWidth = (int)(Size.Y * aspectRatio);
                        }

                        var centeredRectangle = new Rectangle(
                           Position.X + (Size.X - scaledWidth) / 2,
                           Position.Y + (Size.Y - scaledHeight) / 2,
                           scaledWidth,
                           scaledHeight
                        );

                        spriteBatch.Draw(BackgroundImage, centeredRectangle, Color.White);
                        break;
                    default:
                        spriteBatch.Draw(BackgroundImage, Position.ToVector2(), Color.White);
                        break;
                }
                }
            }
        }

        protected void DrawNineSlice(SpriteBatch spriteBatch, Texture2D texture, Rectangle dest, NineSlice borders, Color color)
        {
            int texW = texture.Width, texH = texture.Height;
            float scale = borders.Scale;

            // Scaled border sizes
            int l = (int)(borders.Left * scale);
            int t = (int)(borders.Top * scale);
            int r = (int)(borders.Right * scale);
            int b = (int)(borders.Bottom * scale);

            // Source rectangles (from the texture)
            Rectangle[] src =
            {
                new(0, 0, borders.Left, borders.Top),                // Top-left
                new(borders.Left, 0, texW - borders.Left - borders.Right, borders.Top),     // Top
                new(texW - borders.Right, 0, borders.Right, borders.Top),         // Top-right
                new(0, borders.Top, borders.Left, texH - borders.Top - borders.Bottom),     // Left
                new(borders.Left, borders.Top, texW - borders.Left - borders.Right, texH - borders.Top - borders.Bottom), // Center
                new(texW - borders.Right, borders.Top, borders.Right, texH - borders.Top - borders.Bottom),     // Right
                new(0, texH - borders.Bottom, borders.Left, borders.Bottom),         // Bottom-left
                new(borders.Left, texH - borders.Bottom, texW - borders.Left - borders.Right, borders.Bottom),     // Bottom
                new(texW - borders.Right, texH - borders.Bottom, borders.Right, borders.Bottom),  // Bottom-right
            };

            // Destination rectangles (on the UI)
            int dw = dest.Width, dh = dest.Height;
            Rectangle[] dst =
            {
                new(dest.X, dest.Y, l, t),
                new(dest.X + l, dest.Y, dw - l - r, t),
                new(dest.X + dw - r, dest.Y, r, t),
                new(dest.X, dest.Y + t, l, dh - t - b),
                new(dest.X + l, dest.Y + t, dw - l - r, dh - t - b),
                new(dest.X + dw - r, dest.Y + t, r, dh - t - b),
                new(dest.X, dest.Y + dh - b, l, b),
                new(dest.X + l, dest.Y + dh - b, dw - l - r, b),
                new(dest.X + dw - r, dest.Y + dh - b, r, b),
            };

            for (int i = 0; i < 9; i++)
                spriteBatch.Draw(texture, dst[i], src[i], color);
        }

        /// <summary>
        /// Draws the text associated with the UI element using the specified <see cref="SpriteBatch"/>.
        /// </summary>
        /// <param name="spriteBatch"></param>
        protected virtual void DrawText(SpriteBatch spriteBatch)
        {
            if (Text != null)
            {
                var font = fontRegistry.Get(Font, FontSize, FontStyle);
                switch (TextAlign)
                {
                    case TextAlign.Center:
                        var centerPosition = new Vector2(
                            Position.X + (Size.X / 2) - (font.MeasureString(Text).X / 2),
                            Position.Y + (Size.Y / 2) - (font.MeasureString(Text).Y / 2)
                        );
                        spriteBatch.DrawString(font, Text, centerPosition, FontColor, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
                        break;
                    case TextAlign.Left:
                        var leftPosition = new Vector2(
                            Position.X + Padding.X,
                            Position.Y + (Size.Y / 2) - (font.MeasureString(Text).Y / 2)
                        );
                        spriteBatch.DrawString(font, Text, leftPosition, FontColor, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
                        break;
                    case TextAlign.Right:
                        var rightPosition = new Vector2(
                            Position.X + Size.X - Padding.X - font.MeasureString(Text).X,
                            Position.Y + (Size.Y / 2) - (font.MeasureString(Text).Y / 2)
                        );
                        spriteBatch.DrawString(font, Text, rightPosition, FontColor, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
                        break;
                }
            }
        }

        /// <summary>
        /// Updates the mouse states based on the current input provider's mouse position and button states.
        /// </summary>
        /// <param name="input"></param>
        private void UpdateMouseStates(IInputProvider input)
        {
            var mouseCurrentlyOver = new Rectangle(Position, Size).Contains(input.MousePosition);
            if (mouseCurrentlyOver)
            {
                isMousePressing = input.IsMouseButtonDown(MouseButton.Left);

                if (input.IsMouseButtonReleased(MouseButton.Left))
                    OnClick?.Invoke(this);

                if (!wasMouseOver)
                {
                    wasMouseOver = true;
                    OnMouseEnter?.Invoke(this);
                }
            }
            else
            {
                if (wasMouseOver)
                {
                    OnMouseLeave?.Invoke(this);
                    wasMouseOver = false;
                }

                isMousePressing = false;
            }
        }

        protected static Point CalculateAnchoredPosition(Point offset, Point size, AnchorPoint anchor, Point parentSize)
        {
            int x = offset.X, y = offset.Y;
            switch (anchor)
            {
                case AnchorPoint.TopLeft:
                    // No change
                    break;
                case AnchorPoint.TopCenter:
                    x += (parentSize.X - size.X) / 2;
                    break;
                case AnchorPoint.TopRight:
                    x += parentSize.X - size.X;
                    break;
                case AnchorPoint.MiddleLeft:
                    y += (parentSize.Y - size.Y) / 2;
                    break;
                case AnchorPoint.MiddleCenter:
                    x += (parentSize.X - size.X) / 2;
                    y += (parentSize.Y - size.Y) / 2;
                    break;
                case AnchorPoint.MiddleRight:
                    x += parentSize.X - size.X;
                    y += (parentSize.Y - size.Y) / 2;
                    break;
                case AnchorPoint.BottomLeft:
                    y += parentSize.Y - size.Y;
                    break;
                case AnchorPoint.BottomCenter:
                    x += (parentSize.X - size.X) / 2;
                    y += parentSize.Y - size.Y;
                    break;
                case AnchorPoint.BottomRight:
                    x += parentSize.X - size.X;
                    y += parentSize.Y - size.Y;
                    break;
                case AnchorPoint.None:
                default:
                    // No change
                    break;
            }
            return new Point(x, y);
        }
    }
}