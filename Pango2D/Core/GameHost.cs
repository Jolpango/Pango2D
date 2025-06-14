using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.Core.Audio;
using Pango2D.Core.Graphics;
using Pango2D.Core.Input;
using Pango2D.Core.Input.Contracts;
using Pango2D.Core.Scenes;
using System;

namespace Pango2D.Core
{
    public class GameHost
    {
        private readonly Game game;
        private readonly GameServices gameServices = new();
        private SceneManager sceneManager;
        public GameHost(Game game, SpriteBatch spriteBatch)
        {
            this.game = game;
            RegisterCoreServices(spriteBatch);
        }
        protected virtual void RegisterCoreServices(SpriteBatch spriteBatch)
        {
            gameServices.Register(game);
            gameServices.Register(game.GraphicsDevice);
            gameServices.Register(game.Content);
            gameServices.Register(spriteBatch);
            gameServices.Register(game.Window);
            gameServices.Register(new FontRegistry(game.Content, "DefaultFont"));
            gameServices.Register(new SoundEffectRegistry(game.Content));
            gameServices.Register<IInputProvider>(new InputManager());
        }
        public virtual void Initialize()
        {
            // Initialization logic for the game host
            sceneManager = new SceneManager(gameServices);
        }
        public virtual void LoadInitialScene(SceneBase initialScene)
        {
            if (initialScene is null) throw new ArgumentNullException(nameof(initialScene));
            sceneManager.ChangeScene(initialScene);
        }
        public virtual void Update(GameTime gameTime)
        {
            // Update game logic, handle input, etc.
            gameServices.Get<IInputProvider>()?.Update();
            sceneManager.Update(gameTime);
        }
        public virtual void Draw(GameTime gameTime)
        {
            // Render the game graphics
            sceneManager.Draw(gameTime);
        }
    }
}
