using System.IO;
using Better.Internal.Core.Runtime;
using Better.ProjectSettings.EditorAddons;
using Better.Saves.EditorAddons.Utility;
using Better.Saves.Runtime.Settings;
using UnityEditor;

namespace Better.Saves.EditorAddons.Settings
{
    internal class SavesSettingsProvider : DefaultProjectSettingsProvider<SavesSettings>
    {
        public SavesSettingsProvider() : base(SavesSettings.SettingsPath)
        {
        }
        
        [MenuItem(SavesSettings.SettingsPath + "/" + PrefixConstants.HighlightPrefix, false, 999)]
        private static void HighlightSettings()
        {
            SettingsService.OpenProjectSettings(ProjectPath + SavesSettings.SettingsPath);
        }

        [MenuItem(SavesSettings.SettingsPath + "/Show In Explorer")]
        private static void ShowInExplorer()
        {
            var settings = SavesSettings.Instance;
            var folderPath = settings.GetFolderPath();

            Directory.CreateDirectory(folderPath);
            EditorUtility.OpenWithDefaultApp(folderPath);
        }

        [MenuItem(SavesSettings.SettingsPath + "/Clear All")]
        private static void ClearAll()
        {
            SavesEditorUtility.ClearAll();
        }
    }
}