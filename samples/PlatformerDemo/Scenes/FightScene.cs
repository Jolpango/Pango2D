using Microsoft.Xna.Framework;
using Pango2D.Core.Scenes;
using Pango2D.ECS;
using Pango2D.ECS.Components;
using Pango2D.ECS.Components.CameraComponents;
using Pango2D.ECS.Components.Commands;
using Pango2D.ECS.Components.Physics;
using Pango2D.ECS.Systems.RenderSystems;
using Pango2D.ECS.Systems.UpdateSystems.CameraSystems;
using Pango2D.ECS.Systems.UpdateSystems.MovementSystems;
using Pango2D.Graphics.Sprites;
using Pango2D.Tiled.DTO.TiledData;
using Pango2D.UI;
using Pango2D.UI.Elements;
using Pango2D.Utilities;
using PlatformerDemo.Components;
using PlatformerDemo.Systems;
using System.Linq;

namespace PlatformerDemo.Scenes
{
    public class FightScene : HybridScene
    {
        public override void LoadContent()
        {
            Services.TextureRegistry.Load("char_blue_1", "Sprites/char_blue_1");
            Services.SoundEffectRegistry.Load("swing", "Sounds/SoundEffects/swing");
            base.LoadContent();
        }
        protected override World ConfigureScene(UIManager uiManager)
        {
            World world = new WorldBuilder(Services)
                .AddCoreSystems()
                .AddLightingSystems()
                .AddSystem(new TileMapRenderer())
                .AddSystem(new GravitySystem())
                .AddSystem(new MainCameraSystem())
                .AddSystem(new CameraShakeSystem())
                .AddSystem(new FighterSystem())
                .AddSystem(new FighterOnGroundSystem())
                .AddSystem(new DebugColliderRenderSystem())
                .AddSystem(new FighterAttackSystem())
                .Build();
            var camera = CreateCamera(world);
            var map = CreateMap(world);

            CreateDebugView(world, uiManager);

            return world;
        }

        private void CreateDebugView(World world, UIManager uiManager)
        {
            var playerEnumerator = world.Query<FighterComponent>().GetEnumerator();
            playerEnumerator.MoveNext();
            var player1 = playerEnumerator.Current;
            playerEnumerator.MoveNext();
            var player2 = playerEnumerator.Current;
            var uiLoader = new UILoader(Services);
            player1.Item2.Name = "Player 1";
            player2.Item2.Name = "Player 2";
            var player1Debug = uiLoader.LoadWithContext("Content/Views/FighterDebugView.xaml", player1.Item2);
            var player2Debug = uiLoader.LoadWithContext("Content/Views/FighterDebugView.xaml", player2.Item2);
            player2Debug.Anchor = AnchorPoint.TopRight;
            uiManager.AddRootElement(player1Debug);
            uiManager.AddRootElement(player2Debug);
        }

        private Entity CreateCamera(World world)
        {
            var camera = new EntityBuilder(world)
                .AddComponent(new Camera()
                {
                    Offset = new Vector2(32, 32),
                    Zoom = 1f,
                    ViewportWidth = Services.ViewportService.VirtualWidth,
                    ViewportHeight = Services.ViewportService.VirtualHeight
                })
                .AddComponent(new CameraAcceleration())
                .AddComponent(new CameraShake())
                .Build();
            return camera;
        }

        private Entity CreateMap(World world)
        {
            var prefabs = new PrefabFactory(world);
            var map = TiledWorldLoader.LoadMap("Content/Tiled/Tilemaps/islands.tmj", world, scale: 2, prefabFactory: prefabs.TiledFactory);
            world.AddComponent(map, new Light() { Type = LightType.Ambient, Color = Color.White, Intensity = 0.8f });
            return map;
        }
    }
    class PrefabFactory(World world)
    {
        public World World { get; set; } = world;
        public Entity? TiledFactory(TiledObject tiledObject)
        {
            return tiledObject.Name switch
            {
                "Player" => new EntityBuilder(World)
                    .AddComponent(new Transform() { Position = new Vector2(tiledObject.X, tiledObject.Y), Scale = Vector2.One * 2 })
                    .AddRigidBody(mass: 1)
                    .AddComponent(new Sprite(World.Services.TextureRegistry["char_blue_1"]))
                    .AddComponent(new SpriteAnimator(AsepriteLoader.Load("Sprites/char_blue_1.json")))
                    .AddComponent(new AnimationCommand()
                    {
                        AnimationName = "Idle",
                        SetAsDefault = true,
                        Loop = true
                    })
                    .AddComponent(new Collider() { Bounds = new Rectangle(5, 25, 40, 60) })
                    .AddComponent(new FighterComponent())
                    .AddComponent(new FighterControls())
                    .AddComponent(new Light() { Color = Color.White, Radius = 2000, Intensity = 0.7f, Offset = new Vector2(32, 32) })
                    .AddComponent(new MainCameraTarget())
                     .AddComponent(new Gravity() { Value = new Vector2(0, 40000) })
                    .AddComponent(new Friction() { Value = 0.85f })
                    .Build(),
                "Player2" => new EntityBuilder(World)
                     .AddComponent(new Transform() { Position = new Vector2(tiledObject.X, tiledObject.Y), Scale = Vector2.One * 2 })
                     .AddRigidBody(mass: 1)
                     .AddComponent(new Sprite(World.Services.TextureRegistry["char_blue_1"]))
                     .AddComponent(new SpriteAnimator(AsepriteLoader.Load("Sprites/char_blue_1.json")))
                     .AddComponent(new AnimationCommand()
                     {
                         AnimationName = "Idle",
                         SetAsDefault = true,
                         Loop = true
                     })
                     .AddComponent(new Collider() { Bounds = new Rectangle(5, 25, 40, 60) })
                     .AddComponent(new FighterComponent())
                     .AddComponent(new FighterControls() { PlayerIndex = PlayerIndex.Two })
                     .AddComponent(new Light() { Color = Color.White, Radius = 2000, Intensity = 0.7f, Offset = new Vector2(32, 32) })
                     .AddComponent(new MainCameraTarget())
                     .AddComponent(new Gravity() { Value = new Vector2(0, 40000) })
                     .AddComponent(new Friction() { Value = 0.85f })
                     .Build(),
                _ => null
            };
        }
    }
}
