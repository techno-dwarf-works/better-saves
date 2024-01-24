using System.IO;
using Better.EditorTools.SettingsTools;
using Better.Saves.EditorAddons.Utility;
using Better.Saves.Runtime;
using Better.Saves.Runtime.Settings;
using Better.Tools.Runtime;
using UnityEditor;

namespace Better.Saves.EditorAddons.Settings
{
    internal class SavesSettingsProvider : ProjectSettingsProvider<SavesSettings>
    {
        private readonly Editor _editor;

        public SavesSettingsProvider()
            : base(ProjectSettingsToolsContainer<SavesSettingsTool>.Instance, SettingsScope.Project)
        {
            _editor = Editor.CreateEditor(_settings);
        }

        protected override void DrawGUI()
        {
            _editor.OnInspectorGUI();
        }

        [MenuItem(SavesSettingsTool.MenuItemPrefix + "/" + BetterEditorDefines.HighlightPrefix, false, 999)]
        private static void HighlightSettings()
        {
            var settingsPath = ProjectSettingsToolsContainer<SavesSettingsTool>.Instance.ProjectSettingKey;
            SettingsService.OpenProjectSettings(settingsPath);
        }

        [MenuItem(SavesSettingsTool.MenuItemPrefix + "/Show In Explorer")]
        private static void ShowInExplorer()
        {
            var settings = ProjectSettingsToolsContainer<SavesSettings>.Instance;
            var folderPath = settings.GetFolderPath();

            Directory.CreateDirectory(folderPath);
            EditorUtility.OpenWithDefaultApp(folderPath);
        }

        [MenuItem(SavesSettingsTool.MenuItemPrefix + "/Clear All")]
        private static void ClearAll()
        {
            SavesEditorUtility.ClearAll();
        }
    }
}