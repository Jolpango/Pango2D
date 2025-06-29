using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.ECS;
using Pango2D.ECS.Components;
using Pango2D.Graphics.Sprites;
using Pango2D.Tiled;
using Pango2D.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    public class PrefabFactory(World world)
    {
        public World World { get; set; } = world;
        /*
         *             Entity player = new EntityBuilder(world)
                .AddComponent(new PlayerComponent())
                .AddComponent(new Transform())
                .AddComponent(new Velocity(Vector2.One * 100))
                .AddComponent(new Sprite(Content.Load<Texture2D>("spinning-dagger")))
                .AddComponent(new SpriteAnimator(AsepriteLoader.Load("spinning-dagger.json")))
                .AddComponent(new BoxCollider() { Bounds = new Rectangle(0, 0, 32, 32) })
                .AddComponent(new Light() { Color = Color.White, Radius = 200, Intensity = 0.7f, Offset = new Vector2(16, 16) })
                .AddComponent(new MainCameraTarget())
                .Build();

            Entity enemy = new EntityBuilder(world)
                .AddComponent(new Transform() { Position = new Vector2(200, 200)})
                .AddComponent(new BoxCollider() { Bounds = new Rectangle(0, 0, 32, 32), IsStatic = true })
                .AddComponent(new Sprite(Content.Load<Texture2D>("spinning-dagger")))
                .AddComponent(new SpriteAnimator(AsepriteLoader.Load("spinning-dagger.json")))
                .AddComponent(new Light() { Color = Color.Red, Radius = 50, Intensity = 0.7f, Offset = new Vector2(16, 16) })
                .Build();
            world.AddComponent(enemy, new AnimationCommand() { AnimationName = "default", Loop = true });

            Entity light = new EntityBuilder(world)
                .AddComponent(new Transform() { Position = new Vector2(100, 200)})
                .AddComponent(new Light() { Color = Color.Orange, Radius = 100f, Intensity = 1f })
                .Build();

            Entity light2 = new EntityBuilder(world)
                .AddComponent(new Transform() { Position = new Vector2(100, 0) })
                .AddComponent(new Light() { Color = Color.Orange, Radius = 100f, Intensity = 0.5f })
                .Build();
         */
        public Entity? TiledFactory(TiledObject tiledObject)
        {
            return tiledObject.Name switch
            {
                "Player" => new EntityBuilder(World)
                    .AddComponent(new PlayerComponent())
                    .AddComponent(new Transform() { Position = new Vector2(tiledObject.X, tiledObject.Y) })
                    .AddComponent(new Velocity(Vector2.One * 100))
                    .AddComponent(new Sprite(World.Services.Get<ContentManager>().Load<Texture2D>("Ninja")))
                    .AddComponent(new SpriteAnimator(AsepriteLoader.Load("Ninja.json")))
                    .AddComponent(new BoxCollider() { Bounds = new Rectangle(0, 0, 16, 16) })
                    .AddComponent(new Light() { Color = Color.White, Radius = 200, Intensity = 0.7f, Offset = new Vector2(16, 16) })
                    .AddComponent(new MainCameraTarget())
                    .Build(),
                "Light" => new EntityBuilder(World)
                    .AddComponent(new Transform() { Position = new Vector2(tiledObject.X, tiledObject.Y) })
                    .AddComponent(new Light() { Color = Color.Orange, Radius = 100f, Intensity = 0.5f })
                    .Build(),
                 "Enemy" => new EntityBuilder(World)
                    .AddComponent(new Transform() { Position = new Vector2(tiledObject.X, tiledObject.Y) })
                    .AddComponent(new BoxCollider() { Bounds = new Rectangle(0, 0, 16, 16), IsStatic = true })
                    .AddComponent(new Sprite(World.Services.Get<ContentManager>().Load<Texture2D>("Ninja")))
                    .AddComponent(new SpriteAnimator(AsepriteLoader.Load("Ninja.json")))
                    .AddComponent(new Light() { Color = Color.Red, Radius = 50, Intensity = 0.7f, Offset = new Vector2(16, 16) })
                    .Build(),
                _ => null
            };
        }
    }
}
