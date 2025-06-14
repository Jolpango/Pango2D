using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.Core.Graphics;
using Pango2D.Core.Input;
using Pango2D.Core.Input.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pango2D.UI.Elements
{
    public class UIButton : UIElement
    {
        private string text = "Button";

        private SpriteFont font;

        public Color BackgroundColor { get; set; } = Color.Gray;
        public Color HoverColor { get; set; } = Color.LightGray;
        public Color TextColor { get; set; } = Color.White;

        public Action OnClick;

        private bool isHovered = false;
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
        public override void Measure()
        {
            if (Font != null)
            {
                var textSize = Font.MeasureString(Text).ToPoint();
                DesiredSize = new Point(textSize.X + Padding.X * 2, textSize.Y + Padding.Y * 2);
                Size = DesiredSize;
            }
            else
            {
                Size = Padding;
            }
        }
        public SpriteFont Font
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

        public override void Update(GameTime time, IInputProvider input)
        {
            var mousePos = input.MousePosition;
            var rect = new Rectangle(Position, Size);

            isHovered = rect.Contains(mousePos);

            if (isHovered && input.IsMouseButtonPressed(MouseButton.Left))
                OnClick?.Invoke();

            base.Update(time, input);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var color = isHovered ? HoverColor : BackgroundColor;
            spriteBatch.Draw(TextureCache.White, new Rectangle(Position, Size), color);

            if (Font is not null)
            {
                var textSize = Font.MeasureString(Text);
                var textPos = new Vector2(
                    Position.X + (Size.X - textSize.X) / 2,
                    Position.Y + (Size.Y - textSize.Y) / 2
                );
                spriteBatch.DrawString(Font, Text, textPos, TextColor);
            }

            base.Draw(spriteBatch);
        }
    }
}
