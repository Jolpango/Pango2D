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
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            AsepriteLoader.RootDirectory = Content.RootDirectory;
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {

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


            Entity player = new EntityBuilder(world.CreateEntity())
                .AddComponent(new TransformComponent())
                .AddComponent(new PlayerComponent())
                .AddComponent(new VelocityComponent(new Vector2(0, 0)))
                .AddComponent(new SpriteComponent(new Sprite(Content.Load<Texture2D>("spinning-dagger"))))
                .AddComponent(new AnimationComponent(new SpriteAnimator(AsepriteLoader.Load("spinning-dagger.json"))))
                .Build();
            player.GetComponent<AnimationComponent>().Animator.Play("default", true);
        }

        protected override void Update(GameTime gameTime)
        {
            inputProvider.Update();

            if (inputProvider.IsKeyPressed(Keys.Escape) || inputProvider.IsMouseButtonPressed(MouseButton.Right))
            {
                Exit();
            }

            world.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            world.Draw(gameTime, spriteBatch, registry);

            base.Draw(gameTime);
        }
    }
}
