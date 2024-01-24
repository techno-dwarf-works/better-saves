using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Better.Saves.Runtime.Interfaces;
using Better.Saves.Runtime.Settings;
using Better.Saves.Runtime.Utility;
using UnityEngine;

namespace Better.Saves.Runtime
{
    public sealed class InternalSavesSystem : ISaveSystem
    {
        private static SavesSettings _settings;

        private string _folderPath;
        private IFormatter _formatter;

        public InternalSavesSystem(IFormatter formatter)
        {
            if (_settings is null)
            {
                _settings = Resources.Load<SavesSettings>(nameof(SavesSettings));
            }

            _folderPath = _settings.GetFolderPath();
            _formatter = formatter;
        }

        // TODO: GH-2 (Newtonsoft formatter dependency)
        public InternalSavesSystem() : this(new BinaryFormatter())
        {
        }

        #region ISaveSystem

        public bool Has<T>(string key)
        {
            var filePath = GetFilePath<T>(key);
            return File.Exists(filePath);
        }

        public bool Has<T>()
        {
            var key = SavesUtility.GetKeyByType<T>();
            return Has<T>(key);
        }

        public T Load<T>(string key, T defaultValue = default)
        {
            if (!SavesUtility.ValidateType<T>())
            {
                return defaultValue;
            }

            if (!Has<T>(key))
            {
                return defaultValue;
            }

            var filePath = GetFilePath<T>(key);
            var file = File.Open(filePath, FileMode.Open);
            var data = (T)_formatter.Deserialize(file);
            file.Close();

            return data;
        }

        public T Load<T>(T defaultValue = default)
        {
            var key = SavesUtility.GetKeyByType<T>();
            return Load(key, defaultValue);
        }

        public void Save<T>(string key, T data)
        {
            if (!SavesUtility.ValidateType<T>()) return;

            var filePath = GetFilePath<T>(key);
            FileStream file;

            if (Has<T>(key))
            {
                file = File.Open(filePath, FileMode.Open);
            }
            else
            {
                file = File.Create(filePath);
            }

            _formatter.Serialize(file, data);
            file.Close();
        }

        public void Save<T>(T data)
        {
            var key = SavesUtility.GetKeyByType<T>();
            Save(key, data);
        }

        public bool Delete<T>(string key)
        {
            if (Has<T>(key) == false)
            {
                return false;
            }

            var filePath = GetFilePath<T>(key);
            File.Delete(filePath);
            return true;
        }

        public void Delete<T>()
        {
            var key = SavesUtility.GetKeyByType<T>();
            Delete<T>(key);
        }

        public void DeleteAll()
        {
            var files = Directory.GetFiles(_folderPath);

            for (var i = 0; i < files.Length; i++)
            {
                File.Delete(files[i]);
            }
        }

        #endregion

        private string GetFilePath<T>(string key)
        {
            SavesUtility.ValidateKey(key);

            var typeName = typeof(T).Name.ToLower();
            var typeExtension = $".{typeName}";
            var fileName = key + typeExtension;

            return Path.Combine(_folderPath, fileName);
        }
    }
}