using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.Core;
using Pango2D.Core.Graphics;
using Pango2D.Utilities;
using Demo.Scenes;
using Pango2D.Core.Services;

namespace Demo
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private GameHost gameHost;
        public Game1()
        {
            graphics = new(this)
            {
                PreferredBackBufferWidth = 1920,
                PreferredBackBufferHeight = 1080,
                //SynchronizeWithVerticalRetrace = false
            };
            //IsFixedTimeStep = false;
            Window.AllowUserResizing = true;
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
            spriteBatch = new(GraphicsDevice);
            gameHost = new(this, spriteBatch, (GameServices services) =>
            {
                // Add or configure services here if needed
                // In this case, we are using the default services provided by GameHost, but those can be changed.
                // services.Register(TextureService);
                // Or overwrite a service
                // services.Set(new GamePadInputProvider());
                // Core services can be accessed directly, core services are:(Will later refactor to be interfaces for easier customization

                // Monogame
                // ========
                // Game
                // GameWindow
                // ContentManager
                // SpriteBatch
                // =========

                // Pango2D
                // =========
                // RenderPipeline(Changing this is not supported well)
                // RenderTargetRegistry(Changing this is not supported well)

                // FontRegistry (common uses here might be to add a new font(required sizes and naming conventions)
                // services.FontRegistry.Add("Arial-8", Content.Load<SpriteFont>("Fonts/Arial/Arial-8"));
                // or where Arial is the "family" name, and the path is without size suffix(will try to load 8, 12, 14, 16, 24, 36, 48, and 72 sizes unless otherwise specified)
                // services.FontRegistry.AddFontCollection("Arial", "Fonts/Arial");
                // Keep in mind this might cause certain UI elements default sizing to throw exceptions if the font is not available in the required sizes.
                // services.FontRegistry.AddFontCollection("Arial", "Fonts/Arial", [8, 14, 22]);

                // SoundEffectRegistry
                // ViewPortService(Changing this is not supported well)
                // IInputProvider(standard is InputProvider which currently provides support for mouse and keyboard
                // DebugService(currently only measures FPS)
            });
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
            gameHost.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
