using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.Core;
using Pango2D.Core.Graphics;
using PlatformerDemo.Scenes;


namespace PlatformerDemo
{
    public class PlatformerGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GameHost gameHost;

        public PlatformerGame()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1280,
                PreferredBackBufferHeight = 720,
            };
            _graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
        }

        protected override void Initialize()
        {
            TextureCache.Initialize(GraphicsDevice);
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            gameHost = new GameHost(this, _spriteBatch);
            gameHost.Initialize();
            gameHost.LoadInitialScene(new FightScene());
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
