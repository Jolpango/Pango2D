using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pango2D.Core.Scenes
{
    public class SceneManager
    {
        private GameServices services;
        private Stack<SceneBase> scenes = new();
        public SceneManager(GameServices services) { this.services = services; }

        public void PushScene(SceneBase scene)
        {
            if (scene is null)
                throw new ArgumentNullException(nameof(scene), "Scene cannot be null.");
            scenes.Push(scene);
            scene.Initialize(services);
            scene.LoadContent();
        }

        public void PopScene()
        {
            if (scenes.Count > 0)
            {
                var scene = scenes.Pop();
                scene.UnloadContent();
            }
        }

        public void ChangeScene(SceneBase newScene)
        {
            if (newScene is null)
                throw new ArgumentNullException(nameof(newScene), "New scene cannot be null.");
            foreach (var scene in scenes)
            {
                scene.UnloadContent();
            }
            scenes.Clear();
            scenes.Push(newScene);
            newScene.Initialize(services);
            newScene.LoadContent();
        }

        public void Initialize()
        {

        }
        public void LoadContent()
        {

        }
        public void UnloadContent()
        {
            foreach(var scene in scenes)
            {
                scene.UnloadContent();
            }
            scenes.Clear();
        }
        public void Update(GameTime gameTime)
        {
            foreach(SceneBase scene in scenes.Reverse())
            {
                if(scene.IsEnabled)
                    scene.Update(gameTime);
                if (scene.IsBlocking)
                    break;
            }
        }
        public void Draw(GameTime gameTime)
        {
            foreach(var scene in scenes.Reverse())
            {
                if (scene.IsVisible)
                    scene.Draw(gameTime);
            }
        }
    }
}
