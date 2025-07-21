using ImGuiNET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.ImGuiNet;
using Pango2D.Core.Graphics;
using Pango2D.Graphics.Particles;
using Pango2D.Graphics.Particles.Dispersion;
using System;

namespace EffectEditor
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public ImGuiRenderer GuiRenderer;
        private ParticleEffect particleEffect;

        System.Numerics.Vector4 _colorV4;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1920,
                PreferredBackBufferHeight = 1080
            };
            Window.AllowUserResizing = true;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _colorV4 = Color.CornflowerBlue.ToVector4().ToNumerics();

        }

        protected override void Initialize()
        {
            GuiRenderer = new ImGuiRenderer(this);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            GuiRenderer.RebuildFontAtlas();
            TextureCache.Initialize(GraphicsDevice);
            particleEffect = new();
            particleEffect.Position = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            particleEffect.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(_colorV4));
            _spriteBatch.Begin();
            particleEffect.Draw(_spriteBatch);
            _spriteBatch.End();
            DrawGUI(gameTime);
            base.Draw(gameTime);
        }

        private void DrawGUI(GameTime gameTime)
        {
            GuiRenderer.BeginLayout(gameTime);
            DrawBackgroundControl();
            DrawParticleEffectControl();
            GuiRenderer.EndLayout();
        }

        private void DrawBackgroundControl()
        {
            var viewport = ImGui.GetMainViewport();
            var windowSize = new System.Numerics.Vector2(250, 80); // Adjust as needed
            var pos = new System.Numerics.Vector2(
                viewport.WorkPos.X + viewport.WorkSize.X - windowSize.X - 10, // 10px margin
                viewport.WorkPos.Y + 10 // 10px from top
            );
            ImGui.SetNextWindowPos(pos, ImGuiCond.Always);
            ImGui.SetNextWindowSize(windowSize, ImGuiCond.Always);

            ImGui.Begin("Background", ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoMove);
            ImGui.ColorEdit4("Color", ref _colorV4);
            ImGui.End();
        }

        private void DrawParticleEffectControl()
        {
            ImGui.BeginMenuBar();
            ImGui.BeginChild("ParticleEffectControl");
            if (ImGui.Button("Add Emitter"))
            {
                particleEffect.Emitters.Add(new ParticleEmitter
                {
                    Name = "New Emitter",
                    Texture = TextureCache.White,
                    MaxParticles = 1000,
                    EmissionRate = 10f,
                    Lifetime = 5f,
                    IsActive = true,
                    IsEmitting = true,
                    Dispersion = new RandomDispersion(100, 200)
                });
            }
            foreach (var emitter in particleEffect.Emitters)
            {
                string name = emitter.Name;
                bool isActive = emitter.IsActive;
                bool isEmitting = emitter.IsEmitting;
                int maxParticles = emitter.MaxParticles;
                float emissionRate = emitter.EmissionRate;
                float lifetime = emitter.Lifetime;

                ImGui.PushID(emitter.Name);
                ImGui.InputText("Name", ref name, 100);
                ImGui.Text($"Emitter: {emitter.Name}");
                ImGui.Checkbox("Active", ref isActive);
                ImGui.Checkbox("Emitting", ref isEmitting);
                ImGui.SliderInt("Max Particles", ref maxParticles, 1, 10000);
                ImGui.SliderFloat("Emission Rate", ref emissionRate, 0.1f, 100f);
                ImGui.SliderFloat("Lifetime", ref lifetime, 0.1f, 10f);
                emitter.Name = name;
                emitter.IsActive = isActive;
                emitter.IsEmitting = isEmitting;
                emitter.MaxParticles = maxParticles;
                emitter.EmissionRate = emissionRate;
                emitter.Lifetime = lifetime;

                if (ImGui.Button("Remove"))
                {
                    particleEffect.Emitters.Remove(emitter);
                    break; // Exit loop to avoid modifying collection while iterating
                }
                ImGui.PopID();

            }
            ImGui.EndMenuBar();
        }
    }
}
