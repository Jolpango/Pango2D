using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.ECS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pango2D.Core.Scenes
{
    public abstract class ECSScene : SceneBase
    {
        protected World World { get; private set; }

        public override void Initialize(GameServices services)
        {
            base.Initialize(services);
        }
        public override void LoadContent()
        {
            base.LoadContent();
            World = new World();
            ConfigureWorld(World);
        }
        protected abstract void ConfigureWorld(World world);
        public override void UnloadContent()
        {
            World?.Dispose();
        }
        public override void Update(GameTime gameTime)
        {
            World?.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            var spriteBatch = Services.Get<SpriteBatch>();
            World?.Draw(gameTime, spriteBatch);
        }
    }
}
