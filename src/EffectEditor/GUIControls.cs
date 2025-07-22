using ImGuiNET;
using Pango2D.Graphics.Particles;
using Pango2D.Graphics.Particles.Contracts;
using Pango2D.Graphics.Particles.Dispersion;
using Pango2D.Graphics.Particles.Modifiers;
using System;
using System.Collections.Generic;

namespace EffectEditor
{
    public static class GUIControls
    {
        const int bufferSize = 256;
        static string filePath = "particle.json";
        static byte[] filePathBuffer = new byte[bufferSize];
        public static void LoadAndSaveControl(ref ParticleEffect particleEffect)
        {
            if (filePath.Length < bufferSize)
                Array.Copy(System.Text.Encoding.UTF8.GetBytes(filePath), filePathBuffer, filePath.Length);

            ImGui.InputText("File Path", filePathBuffer, (uint)bufferSize);
            filePath = System.Text.Encoding.UTF8.GetString(filePathBuffer).TrimEnd('\0');

            if (ImGui.Button("Save"))
            {
                try
                {
                    var json = ParticleEffect.ToJson(particleEffect);
                    System.IO.File.WriteAllText(filePath, json);
                }
                catch (Exception ex)
                {
                    ImGui.OpenPopup("Save Error");
                }
            }
            ImGui.SameLine();
            if (ImGui.Button("Load"))
            {
                try
                {
                    if (System.IO.File.Exists(filePath))
                    {
                        var json = System.IO.File.ReadAllText(filePath);
                        particleEffect = ParticleEffect.FromJson(json, null);
                    }
                }
                catch (Exception ex)
                {
                    ImGui.OpenPopup("Load Error");
                }
            }
            ImGui.SameLine();
            if (ImGui.Button("Browse..."))
            {
                SimpleFileDialog.Open();
            }

            if (SimpleFileDialog.Show("FileDialog", out var chosenFile))
            {
                // Use chosenFile (e.g., load or save)
                filePath = chosenFile;
            }

            // Error popups
            if (ImGui.BeginPopup("Save Error"))
            {
                ImGui.Text("Failed to save file.");
                ImGui.EndPopup();
            }
            if (ImGui.BeginPopup("Load Error"))
            {
                ImGui.Text("Failed to load file.");
                ImGui.EndPopup();
            }
        }

        public static void DrawFloatKeyframeCurve(string label, List<FloatKeyframe> keyFrames, float min = 0f, float max = 1f)
        {
            if (keyFrames == null || keyFrames.Count == 0)
            {
                ImGui.Text("No keyframes.");
            }
            float[] values = new float[keyFrames.Count];
            for (int i = 0; i < keyFrames.Count; i++)
                values[i] = keyFrames[i].Value;
            if (values.Length != 0)
                ImGui.PlotLines("", ref values[0], values.Length, 0, null, min, max, new System.Numerics.Vector2(200, 80));

            keyFrames.Sort((a, b) => a.Time.CompareTo(b.Time));
            for (int i = 0; i < keyFrames.Count; i++)
            {
                FloatKeyframe keyframe = keyFrames[i];
                float value = keyframe.Value;
                float time = keyframe.Time;
                //ImGui.PushID(i);
                ImGui.Text($"Keyframe {i}: Time {keyframe.Time:0.00}");
                ImGui.SliderFloat($"Time##{i}", ref time, 0, 1);
                ImGui.SliderFloat($"Value##{i}", ref value, min, max);
                keyframe.Value = value;
                keyframe.Time = time;
                keyFrames[i] = keyframe;
                if (ImGui.Button("Remove"))
                {
                    keyFrames.RemoveAt(i);
                    ImGui.PopID();
                    break;
                }
                //ImGui.PopID();
                ImGui.Separator();
            }

            float newTime = 0f, newValue = 1f;
            ImGui.Separator();
            ImGui.InputFloat("New Time", ref newTime);
            ImGui.InputFloat("New Value", ref newValue);
            if (ImGui.Button("Add Keyframe"))
            {
                keyFrames.Add(new FloatKeyframe(newTime, newValue));
            }
        }
        public static void DrawColorKeyFrames(List<ColorKeyframe> keyframes)
        {
            if (keyframes == null || keyframes.Count == 0)
            {
                ImGui.Text("No keyframes.");
                float newTime1 = 0f;
                var newColorV41 = new System.Numerics.Vector4(1, 1, 1, 1);
                ImGui.InputFloat("New Time", ref newTime1);
                ImGui.ColorEdit4("New Color", ref newColorV41, ImGuiColorEditFlags.AlphaBar);
                if (ImGui.Button("Add Keyframe"))
                {
                    var newColor = new Microsoft.Xna.Framework.Color(
                        (int)(newColorV41.X * 255),
                        (int)(newColorV41.Y * 255),
                        (int)(newColorV41.Z * 255),
                        (int)(newColorV41.W * 255)
                    );
                    keyframes.Add(new ColorKeyframe(newTime1, newColor));
                }
                return;
            }

            // --- Gradient Preview ---
            // Create a gradient preview using ImGui's draw list
            var drawList = ImGui.GetWindowDrawList();
            var cursorPos = ImGui.GetCursorScreenPos();
            var gradientWidth = 200f;
            var gradientHeight = 24f;
            int steps = 100;

            for (int i = 0; i < steps; i++)
            {
                float t = i / (float)(steps - 1);
                // Find the two keyframes to interpolate between
                ColorKeyframe prev = keyframes[0];
                ColorKeyframe next = keyframes[^1];
                for (int k = 1; k < keyframes.Count; k++)
                {
                    if (t < keyframes[k].Time)
                    {
                        next = keyframes[k];
                        prev = keyframes[k - 1];
                        break;
                    }
                }
                float localT = (t - prev.Time) / Math.Max(next.Time - prev.Time, 0.0001f);
                localT = Math.Clamp(localT, 0f, 1f);
                var color = new System.Numerics.Vector4(
                    prev.Color.R / 255f + (next.Color.R - prev.Color.R) / 255f * localT,
                    prev.Color.G / 255f + (next.Color.G - prev.Color.G) / 255f * localT,
                    prev.Color.B / 255f + (next.Color.B - prev.Color.B) / 255f * localT,
                    prev.Color.A / 255f + (next.Color.A - prev.Color.A) / 255f * localT
                );
                float x0 = cursorPos.X + i * (gradientWidth / steps);
                float x1 = cursorPos.X + (i + 1) * (gradientWidth / steps);
                drawList.AddRectFilled(
                    new System.Numerics.Vector2(x0, cursorPos.Y),
                    new System.Numerics.Vector2(x1, cursorPos.Y + gradientHeight),
                    ImGui.ColorConvertFloat4ToU32(color)
                );
            }
            ImGui.Dummy(new System.Numerics.Vector2(gradientWidth, gradientHeight));
            ImGui.Separator();

            // --- Keyframe List ---
            keyframes.Sort((a, b) => a.Time.CompareTo(b.Time));
            for (int i = 0; i < keyframes.Count; i++)
            {
                var keyframe = keyframes[i];
                float time = keyframe.Time;
                var colorV4 = new System.Numerics.Vector4(
                    keyframe.Color.R / 255f,
                    keyframe.Color.G / 255f,
                    keyframe.Color.B / 255f,
                    keyframe.Color.A / 255f
                );

                ImGui.Text($"Keyframe {i}: Time {time:0.00}");
                ImGui.SliderFloat($"Time##{i}", ref time, 0, 1);
                ImGui.ColorEdit4($"Color##{i}", ref colorV4, ImGuiColorEditFlags.AlphaBar);

                keyframe.Time = time;
                keyframe.Color = new Microsoft.Xna.Framework.Color(
                    (int)(colorV4.X * 255),
                    (int)(colorV4.Y * 255),
                    (int)(colorV4.Z * 255),
                    (int)(colorV4.W * 255)
                );
                keyframes[i] = keyframe;

                if (ImGui.Button($"Remove##{i}"))
                {
                    keyframes.RemoveAt(i);
                    break;
                }
                ImGui.Separator();
            }

            // --- Add New Keyframe ---
            float newTime = 0f;
            var newColorV4 = new System.Numerics.Vector4(1, 1, 1, 1);
            ImGui.InputFloat("New Time", ref newTime);
            ImGui.ColorEdit4("New Color", ref newColorV4, ImGuiColorEditFlags.AlphaBar);
            if (ImGui.Button("Add Keyframe"))
            {
                var newColor = new Microsoft.Xna.Framework.Color(
                    (int)(newColorV4.X * 255),
                    (int)(newColorV4.Y * 255),
                    (int)(newColorV4.Z * 255),
                    (int)(newColorV4.W * 255)
                );
                keyframes.Add(new ColorKeyframe(newTime, newColor));
            }
        }
        public static void DrawDispersionControl(IParticleDispersion dispersion)
        {
            switch (dispersion)
            {
                case RandomDispersion random:
                    DrawRandomDispersion(random);
                    break;
                default:
                    throw new Exception();
            }
        }
        private static void DrawRandomDispersion(RandomDispersion dispersion)
        {
            float minAngle = dispersion.MinAngle;
            float maxAngle = dispersion.MaxAngle;
            float minSpeed = dispersion.MinSpeed;
            float maxSpeed = dispersion.MaxSpeed;
            ImGui.Separator();
            ImGui.SliderFloat("MinAngle", ref minAngle, 0, (float)Math.Tau);
            ImGui.SliderFloat("MaxAngle", ref maxAngle, minAngle, (float)Math.Tau);
            ImGui.SliderFloat("MinSpeed", ref minSpeed, 0, 1000);
            ImGui.SliderFloat("MaxSpeed", ref maxSpeed, minSpeed, 1000);
            ImGui.Separator();

            dispersion.MinAngle = minAngle;
            dispersion.MaxAngle = maxAngle;
            dispersion.MinSpeed = minSpeed;
            dispersion.MaxSpeed = maxSpeed;
        }
    }
}
