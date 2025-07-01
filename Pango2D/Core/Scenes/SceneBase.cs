using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.Core.Audio;
using Pango2D.Core.Graphics;
using Pango2D.Core.Input.Contracts;
using Pango2D.Core.Services;

namespace Pango2D.Core.Scenes
{
    public abstract class SceneBase
    {
        protected GameServices Services { get; private set; }
        protected SceneManager SceneManager => Services.Get<SceneManager>();
        public bool IsVisible { get; set; } = true;
        public bool IsEnabled { get; set;} = true;
        public bool IsBlocking { get; set; } = true;

        public ContentManager Content => Services.Get<ContentManager>();
        public SoundEffectRegistry SoundEffectRegistry => Services.Get<SoundEffectRegistry>();
        public FontRegistry FontRegistry => Services.Get<FontRegistry>();
        public Game Game => Services.Get<Game>();
        public GraphicsDevice GraphicsDevice => Services.Get<GraphicsDevice>();
        public GameWindow Window => Services.Get<GameWindow>();
        public IInputProvider InputProvider => Services.Get<IInputProvider>();
        public SpriteBatch SpriteBatch => Services.Get<SpriteBatch>();
        /// <summary>
        /// Initializes a scene, call base first to ensure services are loaded.
        /// </summary>
        /// <param name="services"></param>
        public virtual void Initialize(GameServices services)
        {
            Services = services;
        }
        /// <summary>
        /// Loads the necessary content for the object.
        /// </summary>
        /// <remarks>This method is intended to be overridden in a derived class to load specific
        /// resources or content  required by the object. The base implementation does not perform any
        /// actions.</remarks>
        public virtual void LoadContent() { }

        /// <summary>
        /// Unloads and cleans up resources associated with the content.
        /// </summary>
        /// <remarks> Override this method  in a derived class to include additional
        /// cleanup logic specific to the subclass.</remarks>
        public virtual void UnloadContent()
        {
        }

        /// <summary>
        /// Updates the state of the game or game component.
        /// </summary>
        /// <remarks>Override this method in a derived class to implement custom update logic for the game
        /// or game component. This method is typically called once per frame during the game's update cycle.</remarks>
        /// <param name="gameTime">An object that provides a snapshot of the game's timing values, including the elapsed time since the last
        /// update.</param>
        public virtual void Update(GameTime gameTime) { }

        /// <summary>
        /// Renders the game object to the screen.
        /// </summary>
        /// <remarks>Override this method in a derived class to implement custom rendering logic for the
        /// game object.</remarks>
        /// <param name="gameTime">An object representing the elapsed game time since the last update, used to synchronize rendering with the
        /// game's timing.</param>
        public virtual void Draw(GameTime gameTime) { }
    }
}
