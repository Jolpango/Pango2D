using ImGuiNET;
using System;
using System.IO;
using System.Collections.Generic;
using System.Numerics;

public static class SimpleFileDialog
{
    private static bool openPopup = false;
    private static string currentDir = Environment.CurrentDirectory;
    private static string selectedFile = "";
    private static string newFileName = "";
    private static Vector2 popupSize = new(500, 400);

    public static bool Show(string popupId, out string filePath)
    {
        filePath = null;

        if (openPopup)
        {
            ImGui.OpenPopup(popupId);
            openPopup = false;
        }

        bool fileChosen = false;
        if (ImGui.BeginPopupModal(popupId))
        {
            ImGui.Text($"Current Directory: {currentDir}");
            ImGui.Separator();

            // Parent directory button
            if (ImGui.Button(".."))
            {
                var parent = Directory.GetParent(currentDir);
                if (parent != null)
                    currentDir = parent.FullName;
            }
            ImGui.SameLine();
            if (ImGui.Button("Refresh"))
            {
                // Just triggers a redraw
            }

            ImGui.BeginChild("FileList", popupSize);

            // List directories
            foreach (var dir in Directory.GetDirectories(currentDir))
            {
                if (ImGui.Selectable("[DIR] " + Path.GetFileName(dir)))
                {
                    currentDir = dir;
                }
            }

            // List files
            foreach (var file in Directory.GetFiles(currentDir))
            {
                if (ImGui.Selectable(Path.GetFileName(file)))
                {
                    selectedFile = file;
                }
            }
            ImGui.EndChild();

            ImGui.Separator();
            ImGui.Text("Selected File:");
            ImGui.InputText("##SelectedFile", ref selectedFile, 256);

            ImGui.Separator();
            ImGui.Text("Create New File:");
            ImGui.InputText("##NewFileName", ref newFileName, 256);
            if (ImGui.Button("New File") && !string.IsNullOrWhiteSpace(newFileName))
            {
                var newFilePath = Path.Combine(currentDir, newFileName);
                try
                {
                    if (!File.Exists(newFilePath))
                    {
                        File.WriteAllText(newFilePath, ""); // Create empty file
                    }
                    selectedFile = newFilePath;
                }
                catch (Exception)
                {
                    // Optionally show error popup
                }
            }

            if (ImGui.Button("OK") && File.Exists(selectedFile))
            {
                filePath = selectedFile;
                fileChosen = true;
                ImGui.CloseCurrentPopup();
            }
            ImGui.SameLine();
            if (ImGui.Button("Cancel"))
            {
                ImGui.CloseCurrentPopup();
            }

            ImGui.EndPopup();
        }

        return fileChosen;
    }

    public static void Open(string startDir = null)
    {
        openPopup = true;
        if (!string.IsNullOrEmpty(startDir) && Directory.Exists(startDir))
            currentDir = startDir;
        selectedFile = "";
        newFileName = "";
    }
}
