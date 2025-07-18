using Demo.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pango2D.ECS;
using Pango2D.ECS.Components;
using Pango2D.ECS.Components.CameraComponents;
using Pango2D.ECS.Components.Commands;
using Pango2D.ECS.Components.Physics;
using Pango2D.Graphics.Particles;
using Pango2D.Graphics.Sprites;
using Pango2D.Tiled.DTO.TiledData;
using Pango2D.Utilities;

namespace Demo
{
    public class PrefabFactory(World world)
    {
        public World World { get; set; } = world;
        public Entity? TiledFactory(TiledObject tiledObject)
        {
            return tiledObject.Name switch
            {
                "Player" => new EntityBuilder(World)
                    .AddComponent(new PlayerComponent())
                    .AddComponent(new Transform() { Position = new Vector2(tiledObject.X, tiledObject.Y), Scale = Vector2.One * 4 })
                    .AddRigidBody(mass: 1)
                    .AddComponent(new Sprite(World.Services.TextureRegistry["Soldier"]))
                    .AddComponent(new SpriteAnimator(AsepriteLoader.Load("Soldier.json")))
                    .AddComponent(new Collider() { Bounds = new Rectangle(25, 20, 50, 70) })
                    .AddComponent(new Light() { Color = Color.White, Radius = 2000, Intensity = 0.7f, Offset = new Vector2(32, 32) })
                    .AddComponent(new MainCameraTarget())
                    .AddComponent(new AnimationCommand() { SetAsDefault=true, AnimationName="Idle", Loop=true })
                    .Build(),
                "Enemy" => new EntityBuilder(World)
                    .AddComponent(new Transform() { Position = new Vector2(tiledObject.X, tiledObject.Y), Scale = Vector2.One * 4 })
                    .AddRigidBody(mass: 1f)
                    .AddComponent(new Sprite(World.Services.Content.Load<Texture2D>("Orc")))
                    .AddComponent(new SpriteAnimator(AsepriteLoader.Load("Orc.json")))
                    .AddComponent(new Collider() { Bounds = new Rectangle(25, 20, 50, 70) })
                    .AddComponent(new EnemyComponent())
                    .AddComponent(ParticleEffect.FromJson(System.IO.File.ReadAllText("Content/Effects/Blood.pef"), World.Services.TextureRegistry))
                    .AddComponent(new Light() { Color = Color.White, Radius = 2000, Intensity = 0.7f, Offset = new Vector2(32, 32) })
                    .AddComponent(new AnimationCommand() { SetAsDefault = true, AnimationName = "idle", Loop = true })
                    .Build(),
                "Coin" => new EntityBuilder(World)
                    .AddComponent(new Transform() { Position = new Vector2(tiledObject.X, tiledObject.Y) })
                    .AddComponent(new Sprite(World.Services.Content.Load<Texture2D>("coin")))
                    .AddComponent(new SpriteAnimator(AsepriteLoader.Load("coin.json")))
                    .AddComponent(new Collider() { Bounds = new Rectangle(0, 0, 16, 16), Behavior = ColliderBehavior.Dynamic })
                    .AddComponent(new Light() { Color = Color.Yellow, Radius = 100f, Intensity = 0.5f })
                    .AddComponent(new AnimationCommand() { AnimationName = "spin", Loop = true })
                    .AddComponent(new CoinComponent())
                    .Build(),
                _ => null
            };
        }
    }
}
