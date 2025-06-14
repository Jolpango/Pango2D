using Demo.Content.UI;
using Demo.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pango2D.Core;
using Pango2D.Core.Graphics;
using Pango2D.Core.Input;
using Pango2D.Core.Input.Contracts;
using Pango2D.ECS;
using Pango2D.ECS.Components;
using Pango2D.ECS.Systems;
using Pango2D.ECS.Systems.RenderSystems;
using Pango2D.ECS.Systems.UpdateSystems;
using Pango2D.Graphics.Sprites;
using Pango2D.UI;
using Pango2D.UI.Views;
using Pango2D.Utilities;
using System.Collections.Generic;

namespace Demo
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private GameHost gameHost;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
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
            gameHost.LoadInitialScene(new MainMenu());
        }

        protected override void Update(GameTime gameTime)
        {
            gameHost.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            gameHost.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
