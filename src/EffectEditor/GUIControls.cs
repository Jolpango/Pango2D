using ImGuiNET;
using Pango2D.Graphics.Particles.Modifiers;
using System.Collections.Generic;

namespace EffectEditor
{
    public static class GUIControls
    {
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
                ImGui.PlotLines(label, ref values[0], values.Length, 0, null, min, max, new System.Numerics.Vector2(200, 80));

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

    }
}
