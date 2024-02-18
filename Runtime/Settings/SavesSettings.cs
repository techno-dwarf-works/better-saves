using System.IO;
using Better.Tools.Runtime;
using Better.Tools.Runtime.Settings;
using UnityEditor;
using UnityEngine;

namespace Better.Saves.Runtime.Settings
{
    public class SavesSettings : ProjectSettings
    {
        public const string KeySeparator = "-";
        private const string DefaultFolderName = BetterEditorDefines.BetterPrefix + nameof(Saves);

        [SerializeField] private string _folderName = DefaultFolderName;

        public string GetFolderPath()
        {
            ValidateFolderName();

            var path = Path.Combine(Application.persistentDataPath, _folderName);
            return path;
        }

        private void OnValidate()
        {
            ValidateFolderName();
        }

        private void ValidateFolderName()
        {
            if (string.IsNullOrEmpty(_folderName) || string.IsNullOrWhiteSpace(_folderName))
            {
                _folderName = DefaultFolderName;

#if UNITY_EDITOR
                if (!Application.isPlaying)
                {
                    EditorUtility.SetDirty(this);
                    AssetDatabase.SaveAssetIfDirty(this);
                }
#endif
            }
        }
    }
}