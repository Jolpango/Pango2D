using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pango2D.ECS;
using Pango2D.ECS.Components;
using Pango2D.ECS.Systems;
using Pango2D.ECS.Systems.RenderSystems;
using Pango2D.ECS.Systems.UpdateSystems;
using Pango2D.Graphics.Sprites;
using Pango2D.Input;
using Pango2D.UI;
using Pango2D.Utilities;
using System.Collections.Generic;

namespace Demo
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private InputManager inputProvider = new InputManager();
        private SpriteBatch spriteBatch;
        private World world;
        private RenderPassRegistry registry = new RenderPassRegistry();
        private UIManager uiManager = new UIManager();
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            AsepriteLoader.RootDirectory = Content.RootDirectory;
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            TextureCache.Initialize(GraphicsDevice);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            world = new WorldBuilder()
                .AddSystem(new MovementSystem())
                .AddSystem(new AnimationSystem())
                .AddSystem(new AnimationCommandSystem())
                .AddSystem(new PlayerInputSystem(inputProvider))
                .AddSystem(new SpriteTransformSystem())
                .AddSystem(new SpriteRenderingSystem())
                .Build();

            Entity player = new EntityBuilder(world)
                .AddComponent(new TransformComponent() { Position = new Vector2(300, 300)})
                .AddComponent(new PlayerComponent())
                .AddComponent(new VelocityComponent(new Vector2(0, 0)))
                .AddComponent(new SpriteComponent(new Sprite(Content.Load<Texture2D>("spinning-dagger"))))
                .AddComponent(new AnimationComponent(new SpriteAnimator(AsepriteLoader.Load("spinning-dagger.json"))))
                .Build();

            UIStackPanel panel = new UIStackPanel()
            {
                Orientation = Orientation.Vertical,
                Gap = 10,
                Padding = new Point(10, 10),
                Id = "MainPanel",
                Position = new Point(10, 10)
            };
            UIStackPanel innerPanel = new UIStackPanel()
            {
                Orientation = Orientation.Horizontal,
                Gap = 10,
                Id = "InnerPanel"
            };
            UIButton button = new UIButton()
            {
                Id = "TestButton",
                Text = "Click Me",
                OnClick = () =>
                {
                    // Example action on button click
                    System.Diagnostics.Debug.WriteLine("Button clicked!");
                },
                Font = Content.Load<SpriteFont>("DefaultFont"),
                BackgroundColor = Color.LightGray,
                HoverColor = Color.Gray,
                TextColor = Color.White,
                Padding = new Point(10, 10)
            };
            UIButton button2 = new UIButton()
            {
                Id = "TestButton2",
                Text = "Click Me",
                OnClick = () =>
                {
                    // Example action on button click
                    System.Diagnostics.Debug.WriteLine("Button clicked!");
                },
                Font = Content.Load<SpriteFont>("DefaultFont"),
                BackgroundColor = Color.LightGray,
                HoverColor = Color.Gray,
                TextColor = Color.White,
                Padding = new Point(40, 10)
            };
            UIButton button3 = new UIButton()
            {
                Id = "TestButton3",
                Text = "Click Me",
                OnClick = () =>
                {
                    // Example action on button click
                    System.Diagnostics.Debug.WriteLine("Button clicked!");
                },
                Font = Content.Load<SpriteFont>("DefaultFont"),
                BackgroundColor = Color.LightGray,
                HoverColor = Color.Gray,
                TextColor = Color.White,
                Padding = new Point(90, 20)
            };
            innerPanel.AddChild(button);
            innerPanel.AddChild(button2);
            panel.AddChild(innerPanel);
            panel.AddChild(button3);
            uiManager.AddRootElement(panel);
        }

        protected override void Update(GameTime gameTime)
        {
            inputProvider.Update();

            if (inputProvider.IsKeyPressed(Keys.Escape))
            {
                Exit();
            }
            uiManager.Update(gameTime, inputProvider);
            world.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            world.Draw(gameTime, spriteBatch);
            uiManager.Draw(spriteBatch, new RenderPassSettings());
            base.Draw(gameTime);
        }
    }
}
