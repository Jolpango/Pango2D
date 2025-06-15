using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.Core;
using Pango2D.Core.Graphics;
using Pango2D.Utilities;
using Demo.Scenes ;

namespace Demo
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private GameHost gameHost;
        private Texture2D test;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
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
            gameHost = new GameHost(this, spriteBatch);
            gameHost.Initialize();
            gameHost.LoadInitialScene(new WorldScene());
            test = Content.Load<Texture2D>("background");
        }

        protected override void Update(GameTime gameTime)
        {
            gameHost.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            gameHost.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
