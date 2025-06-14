using Microsoft.Xna.Framework;

namespace Pango2D.Core.Scenes
{
    public abstract class SceneBase
    {
        protected GameServices Services { get; private set; }
        protected SceneManager SceneManager => Services.Get<SceneManager>();
        public bool IsVisible { get; set; } = true;
        public bool IsEnabled { get; set;} = true;
        public bool IsBlocking { get; set; } = true;

        /// <summary>
        /// Initializes a scene, call base first to ensure services are loaded.
        /// </summary>
        /// <param name="services"></param>
        public virtual void Initialize(GameServices services)
        {
            Services = services;
        }
        public virtual void LoadContent() { }
        public virtual void UnloadContent() { }
        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(GameTime gameTime) { }
    }
}
