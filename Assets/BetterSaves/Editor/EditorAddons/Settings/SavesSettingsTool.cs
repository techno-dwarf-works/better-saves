using Better.EditorTools.SettingsTools;
using Better.Saves.Runtime;
using Better.Saves.Runtime.Settings;
using Better.Tools.Runtime;

namespace Better.Saves.EditorAddons.Settings
{
    public class SavesSettingsTool : ProjectSettingsTools<SavesSettings>
    {
        private const string MenuItemFolder = nameof(Saves);
        public const string MenuItemPrefix = BetterEditorDefines.BetterPrefix + "/" + MenuItemFolder;

        public SavesSettingsTool() : base(MenuItemFolder, MenuItemFolder)
        {
        }
    }
}