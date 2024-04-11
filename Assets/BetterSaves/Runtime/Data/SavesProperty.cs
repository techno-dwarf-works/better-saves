using System;
using Better.Commons.Runtime.DataStructures.Properties;
using Better.Saves.Runtime.Interfaces;
using Better.Saves.Runtime.Utility;
using UnityEngine;

namespace Better.Saves.Runtime.Data
{
    public class SavesProperty<T> : ReactiveProperty<T>
    {
        private readonly ISaveSystem _saveSystem;
        private readonly string _key;

        public SavesProperty(ISaveSystem saveSystem, string key, T defaultValue = default)
            : base(defaultValue)
        {
            if (ReferenceEquals(saveSystem, null))
            {
                throw new ArgumentNullException(nameof(saveSystem));
            }

            SavesUtility.ValidateType<T>();
            if (!SavesUtility.ValidateKey(key))
            {
                key = typeof(T).Name;
                var message = $"[{nameof(SavesProperty<T>)}] {nameof(SavesProperty<T>)}: {nameof(key)} auto-replace to \"{key}\"";
                Debug.LogWarning(message);
            }
            
            _key = key;
            _saveSystem = saveSystem;
            _value = _saveSystem.Load(_key, defaultValue);
        }

        public SavesProperty(string key, T defaultValue = default)
            : this(SavesUtility.GetSystem(), key, defaultValue)
        {
        }

        public override void SetDirty()
        {
            _saveSystem.Save(_key, Value);
            base.SetDirty();
        }
    }
}