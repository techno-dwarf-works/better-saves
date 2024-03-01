using Better.Internal.Core.Runtime;
using Better.ProjectSettings.EditorAddons;
using Better.Saves.Runtime.Settings;
using UnityEditor;

namespace Better.Saves.EditorAddons.Settings
{
    internal class SavesSettingsProvider : DefaultProjectSettingsProvider<SavesSettings>
    {
        private const string Path = PrefixConstants.BetterPrefix + "/" + nameof(Saves);

        public SavesSettingsProvider() : base(Path)
        {
        }

        [MenuItem(Path + "/" + PrefixConstants.HighlightPrefix, false, 999)]
        private static void Highlight()
        {
            SettingsService.OpenProjectSettings(ProjectPath + Path);
        }
    }
}