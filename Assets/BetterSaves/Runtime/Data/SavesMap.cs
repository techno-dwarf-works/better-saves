using System;
using System.Collections.Generic;
using System.Linq;
using Better.Saves.Runtime.Interfaces;
using Better.Saves.Runtime.Utility;
using UnityEngine;

namespace Better.Saves.Runtime.Data
{
    public class SavesMap<TItem>
    {
        private readonly ISaveSystem _saveSystem;
        private readonly string _keyPrefix;
        private readonly Dictionary<string, TItem> _map;

        public SavesMap(string keyPrefix, ISaveSystem saveSystem)
        {
            if (ReferenceEquals(saveSystem, null))
            {
                throw new ArgumentNullException(nameof(saveSystem));
            }

            SavesUtility.ValidateType<TItem>();

            _saveSystem = saveSystem;
            _keyPrefix = keyPrefix ?? string.Empty;
            _map = new();
        }

        public SavesMap(string keyPrefix) : this(keyPrefix, SavesUtility.GetSystem())
        {
        }

        #region Contains

        public bool Contains(string itemId)
        {
            if (!ValidateItemId(itemId))
            {
                return false;
            }

            if (_map.ContainsKey(itemId))
            {
                return true;
            }

            var key = GetKey(itemId);
            return _saveSystem.Has<TItem>(key);
        }

        public bool Contains(string itemId, TItem item)
        {
            return TryGet(itemId, out var mappedItem)
                   && EqualityComparer<TItem>.Default.Equals(mappedItem, item);
        }

        #endregion

        #region Save

        public void Save(string itemId, TItem item)
        {
            if (!Validate(itemId, item)) return;

            _map[itemId] = item;

            var key = GetKey(itemId);
            _saveSystem.Save(key, item);
        }

        public void Save(Tuple<string, TItem> tuple)
        {
            if (ReferenceEquals(tuple, null))
            {
                var message = $"{nameof(tuple)} cannot be null";
                var exception = new ArgumentNullException(message);
                Debug.LogException(exception);
                return;
            }

            Save(tuple.Item1, tuple.Item2);
        }

        public void Save(ValueTuple<string, TItem> tuple)
        {
            Save(tuple.Item1, tuple.Item2);
        }

        private void Save(KeyValuePair<string, TItem> pair)
        {
            Save(pair.Key, pair.Value);
        }

        public void Save()
        {
            foreach (var pair in _map)
            {
                Save(pair);
            }
        }

        #endregion

        #region Get

        public bool TryGet(string itemId, out TItem item)
        {
            if (!ValidateItemId(itemId) || !Contains(itemId))
            {
                item = default;
                return false;
            }

            if (!_map.TryGetValue(itemId, out item))
            {
                var key = GetKey(itemId);
                item = _saveSystem.Load<TItem>(key);
                _map.Add(itemId, item);
            }

            return true;
        }

        public bool TryGet<T>(string itemId, out T item)
            where T : TItem
        {
            if (TryGet(itemId, out var rawItem)
                && rawItem is T castedItem)
            {
                item = castedItem;
                return true;
            }

            item = default;
            return false;
        }

        #endregion

        #region Remove

        public bool Remove(string itemId)
        {
            if (!ValidateItemId(itemId))
            {
                return false;
            }

            var key = GetKey(itemId);
            return _map.Remove(itemId) | _saveSystem.Delete<TItem>(key);
        }

        public void Clear()
        {
            var itemIds = _map.Keys.ToArray();
            foreach (var itemId in itemIds)
            {
                Remove(itemId);
            }
        }

        #endregion

        #region Validation

        private bool Validate(string itemId, TItem item, bool logException = true)
        {
            return ValidateItemId(itemId, logException)
                   && ValidateItem(item, logException);
        }

        private bool ValidateItemId(string id, bool logException = true)
        {
            var isInvalid = string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id);
            if (isInvalid && logException)
            {
                var message = $"Invalid {nameof(id)}({id})";
                var exception = new ArgumentException(message);
                Debug.LogException(exception);
            }

            return !isInvalid;
        }

        private bool ValidateItem(TItem item, bool logException = true)
        {
            var isInvalid = ReferenceEquals(item, null);
            if (isInvalid && logException)
            {
                var message = $"Invalid {nameof(item)}";
                var exception = new ArgumentException(message);
                Debug.LogException(exception);
            }

            return !isInvalid;
        }

        #endregion

        private string GetKey(string itemId)
        {
            var key = SavesUtility.CombineKey(_keyPrefix, itemId);
            return key;
        }
    }
}