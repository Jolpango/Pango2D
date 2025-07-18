using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.Core.Audio;
using Pango2D.Core.Graphics;
using Pango2D.Core.Input;
using Pango2D.Core.Input.Contracts;
using Pango2D.Core.Scenes;
using Pango2D.Core.Services;
using System;

namespace Pango2D.Core
{
    public class GameHost
    {
        private readonly Game game;
        private readonly DebugService debugService = new();
        private readonly GameServices gameServices = new();
        private SceneManager sceneManager;
        public GameHost(Game game, SpriteBatch spriteBatch, Action<GameServices> configureServices = null)
        {
            this.game = game;
            RegisterCoreServices(spriteBatch);
            configureServices?.Invoke(gameServices);
            ValidateRequiredServices();
        }
        protected virtual void RegisterCoreServices(SpriteBatch spriteBatch)
        {
            gameServices.Register(game);
            gameServices.Register(game.GraphicsDevice);
            gameServices.Register(game.Content);
            gameServices.Register(spriteBatch);
            gameServices.Register(game.Window);
            gameServices.Register(new ViewportService());
            gameServices.Register(new FontRegistry(game.Content, "Fonts/Default"));
            gameServices.Register(new SoundEffectRegistry(game.Content));
            gameServices.Register<IInputProvider>(new InputManager());
            gameServices.Register(new GamePadManager());
            gameServices.Register(new RenderTargetRegistry(game.GraphicsDevice, gameServices.Get<ViewportService>()));
            gameServices.Register(debugService);
            gameServices.Register(new TextureRegistry(game.Content));
            UpdateViewportServiceWindowBounds();
            game.Window.ClientSizeChanged += Window_ClientSizeChanged;
        }


        public virtual void Initialize()
        {
            sceneManager = new SceneManager(gameServices);
            gameServices.Register(sceneManager);
        }

        public virtual void LoadInitialScene(SceneBase initialScene)
        {
            ArgumentNullException.ThrowIfNull(initialScene);
            sceneManager.ChangeScene(initialScene);
        }

        public virtual void Update(GameTime gameTime)
        {
            debugService.Update(gameTime);
            gameServices.InputProvider?.Update();
            gameServices.GamePadManager?.Update();
            sceneManager.Update(gameTime);
        }

        public virtual void Draw(GameTime gameTime)
        {
            gameServices.RenderTargetRegistry.ClearRenderTargets();
            sceneManager.Draw(gameTime);
        }

        private void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            UpdateViewportServiceWindowBounds();
        }

        private void UpdateViewportServiceWindowBounds()
        {
            var viewport = gameServices.Get<ViewportService>();
            var windowBounds = gameServices.Get<GameWindow>().ClientBounds;
            viewport.UpdateWindowSize(windowBounds.Width, windowBounds.Height);
        }
        private void ValidateRequiredServices()
        {
            var required = new Type[]
            {
                typeof(Game),
                typeof(GraphicsDevice),
                typeof(ContentManager),
                typeof(SpriteBatch),
                typeof(GameWindow),
                typeof(ViewportService),
                typeof(FontRegistry),
                typeof(SoundEffectRegistry),
                typeof(IInputProvider),
                typeof(RenderTargetRegistry),
                typeof(DebugService)
            };

            foreach (var type in required)
            {
                if (!gameServices.Has(type))
                    throw new InvalidOperationException($"Required service {type.Name} is not registered.");
            }
        }
    }
}
