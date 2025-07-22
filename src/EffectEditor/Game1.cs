using ImGuiNET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.ImGuiNet;
using Pango2D.Core.Graphics;
using Pango2D.Graphics.Particles;
using Pango2D.Graphics.Particles.Contracts;
using Pango2D.Graphics.Particles.Dispersion;
using Pango2D.Graphics.Particles.Modifiers;
using System;
using System.Collections.Generic;

namespace EffectEditor
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public ImGuiRenderer GuiRenderer;
        private ParticleEffect particleEffect;
        private bool effectAtMousePosition = false;
        System.Numerics.Vector4 _colorV4;
        MouseState prev;

        private Dictionary<int, int> emitterModifierSelections = new();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1280,
                PreferredBackBufferHeight = 720
            };
            Window.AllowUserResizing = true;
            Content.RootDirectory = "Content";
            IsFixedTimeStep = true;
            IsMouseVisible = true;
            _colorV4 = new Color(237, 100, 100).ToVector4().ToNumerics();

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
            particleEffect = new()
            {
                Position = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2)
            };
            prev = Mouse.GetState();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if(Mouse.GetState().LeftButton == ButtonState.Pressed && prev.LeftButton == ButtonState.Released)
            {
                particleEffect.Emit(particleEffect.Position, 20);
            }
            if (effectAtMousePosition)
            {
                particleEffect.Position = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            }
            else
            {
                particleEffect.Position = new Vector2(GraphicsDevice.Viewport.Width / 2 - 200, GraphicsDevice.Viewport.Height / 2);
            }
            prev = Mouse.GetState();
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
            DrawParticleEmitterControls();
            GuiRenderer.EndLayout();
        }

        private void DrawBackgroundControl()
        {
            ImGui.Begin("Background");
            ImGui.ColorEdit4("Color", ref _colorV4);
            ImGui.End();
        }

        private void DrawParticleEffectControl()
        {
            ImGui.Begin("Effect");
            GUIControls.LoadAndSaveControl(ref particleEffect);
            ImGui.Checkbox("Effect at Mouse Position", ref effectAtMousePosition);
            ImGui.End();
        }

        private void DrawParticleEmitterControls()
        {
            ImGui.Begin("Emitters");
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

            if (ImGui.BeginTabBar("EmittersTabBar"))
            {
                for (int i = 0; i < particleEffect.Emitters.Count; i++)
                {
                    ParticleEmitter emitter = particleEffect.Emitters[i];
                    string tabName = $"{emitter.Name}##{i}";
                    if (ImGui.BeginTabItem(tabName))
                    {
                        bool flowControl = DrawEmitterControl(i, emitter);
                        ImGui.EndTabItem();
                        if (!flowControl)
                            break;
                    }
                }
                ImGui.EndTabBar();
            }
            ImGui.End();
        }

        private bool DrawEmitterControl(int i, ParticleEmitter emitter)
        {
            ImGui.BeginChild(i.ToString());
            if (ImGui.Button("Remove"))
            {
                particleEffect.Emitters.Remove(emitter);
                return false;
            }

            string name = emitter.Name;
            bool isActive = emitter.IsActive;
            bool isEmitting = emitter.IsEmitting;
            int maxParticles = emitter.MaxParticles;
            float emissionRate = emitter.EmissionRate;
            float lifetime = emitter.Lifetime;
            ImGui.InputText("Name", ref name, 100);
            ImGui.Checkbox("Active", ref isActive);
            ImGui.Checkbox("Emitting", ref isEmitting);
            ImGui.SliderInt("Max Particles", ref maxParticles, 1, 10000);
            ImGui.SliderFloat("Emission Rate", ref emissionRate, 0.1f, 1000f);
            ImGui.SliderFloat("Lifetime", ref lifetime, 0.1f, 10f);
            emitter.Name = name;
            emitter.IsActive = isActive;
            emitter.IsEmitting = isEmitting;
            emitter.MaxParticles = maxParticles;
            emitter.EmissionRate = emissionRate;
            emitter.Lifetime = lifetime;

            GUIControls.DrawDispersionControl(emitter.Dispersion);

            string[] modifierTypes = { "Scale", "Opacity", "Color", "AngularVelocity" };
            if (!emitterModifierSelections.ContainsKey(i))
                emitterModifierSelections[i] = 0;

            int selectedModifierIndex = emitterModifierSelections[i];
            ImGui.Combo("Modifier Type", ref selectedModifierIndex, modifierTypes, modifierTypes.Length);
            emitterModifierSelections[i] = selectedModifierIndex;
            if (ImGui.Button("Add modifier"))
            {
                switch (modifierTypes[selectedModifierIndex])
                {
                    case "Scale":
                        emitter.Modifiers.Add(new ScaleModifier());
                        break;
                    case "Opacity":
                        emitter.Modifiers.Add(new OpacityModifier());
                        break;
                    case "Color":
                        emitter.Modifiers.Add(new ColorModifier());
                        break;
                    case "AngularVelocity":
                        emitter.Modifiers.Add(new AngularVelocityModifier());
                        break;
                }
            }
            ImGui.BeginTabBar("Modifiers");
            for (int m = 0; m < emitter.Modifiers.Count; m++)
            {
                IParticleModifier modifier = emitter.Modifiers[m];
                string tabName = modifier.GetType().Name + "##" + m;
                if (ImGui.BeginTabItem(tabName))
                {
                    DrawModifierControl(modifier);
                    if (ImGui.Button("Remove"))
                    {
                        emitter.Modifiers.RemoveAt(m);
                        ImGui.EndTabItem();
                        break;
                    }
                    ImGui.EndTabItem();
                }
            }
            ImGui.EndTabBar();

            ImGui.EndChild();
            return true;
        }

        public void DrawModifierControl(IParticleModifier modifier)
        {
            switch (modifier)
            {
                case ScaleModifier scaleModifier:
                    DrawScaleModifierControl(scaleModifier);
                    break;
                case ColorModifier colorModifier:
                    DrawColorModifierControl(colorModifier);
                    break;
                case AngularVelocityModifier angularVelocityModifier:
                    DrawAngularVelocityModifierControl(angularVelocityModifier);
                    break;
                case OpacityModifier opacityModifier:
                    DrawOpacityModifierControl(opacityModifier);
                    break;
                default:
                    ImGui.Text("Unknown Modifier Type");
                    break;
            }
        }
        private static void DrawOpacityModifierControl(OpacityModifier modifier)
        {
            var keyFrames = modifier.KeyFrames;
            GUIControls.DrawFloatKeyframeCurve("Opacity Curve", keyFrames, 0f, 1f);
        }
        private static void DrawScaleModifierControl(ScaleModifier modifier)
        {
            var keyFrames = modifier.Keyframes;
            GUIControls.DrawFloatKeyframeCurve("Scale Curve", keyFrames, 0f, 10f);
        }
        private void DrawColorModifierControl(ColorModifier modifier)
        {
            var keyFrames = modifier.KeyFrames;
            GUIControls.DrawColorKeyFrames(keyFrames);

        }
        private void DrawAngularVelocityModifierControl(AngularVelocityModifier modifier)
        {
            var keyFrames = modifier.KeyFrames;
            GUIControls.DrawFloatKeyframeCurve("Angular Velocity Curve", keyFrames, -10f, 10f);
        }

    }
}
