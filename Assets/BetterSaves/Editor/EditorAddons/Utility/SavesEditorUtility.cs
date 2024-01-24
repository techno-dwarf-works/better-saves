using System.IO;
using Better.EditorTools.SettingsTools;
using Better.Saves.Runtime;
using Better.Saves.Runtime.Settings;
using UnityEditor;
using UnityEngine;

namespace Better.Saves.EditorAddons.Utility
{
    public static class SavesEditorUtility
    {
        public static void ClearAll()
        {
            if (EditorUtility.DisplayDialog(
                    $"{nameof(SavesEditorUtility)}: Clear All?",
                    "Are you sure want to clear all UserPrefs? This action cannot be undone.",
                    "Yes",
                    "No"))
            {
                var settings = ProjectSettingsToolsContainer<SavesSettings>.Instance;
                var folderPath = settings.GetFolderPath();

                Directory.Delete(folderPath, true);
                Directory.CreateDirectory(folderPath);
                
                Debug.Log($"[{nameof(SavesEditorUtility)}] Cleared");
            }
        }
    }
}