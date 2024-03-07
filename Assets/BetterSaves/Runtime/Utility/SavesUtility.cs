using System;
using Better.Saves.Runtime.Interfaces;
using Better.Saves.Runtime.Settings;
using UnityEngine;

namespace Better.Saves.Runtime.Utility
{
    public static class SavesUtility
    {
#if BETTER_SERVICES && BETTER_LOCATOR
        private static readonly Locators.Runtime.ServiceProperty<SavesService> _serviceProperty = new();
#endif

        public static ISaveSystem GetSystem()
        {
#if BETTER_SERVICES && BETTER_LOCATOR
            if (_serviceProperty.IsRegistered)
            {
                return _serviceProperty.CachedService;
            }
#endif

#if BETTER_SINGLETONS
            return SavesManager.Instance;
#endif

#pragma warning disable CS0162
            return new SavesSystem();
#pragma warning restore CS0162
        }

        /// <summary>
        /// Combine parts with hyphen and ensure proper format
        /// </summary>
        public static string CombineKey(params string[] parts)
        {
            var value = string.Join(SavesSettings.KeySeparator, parts);
            return value;
        }

        public static string CombineKey(string key1, string key2)
        {
            var formattedKey = $"{key1}{SavesSettings.KeySeparator}{key2}";
            return formattedKey;
        }

        public static string CombineKey(string key1, string key2, string key3)
        {
            var value = $"{key1}{SavesSettings.KeySeparator}{key2}{SavesSettings.KeySeparator}{key3}";
            return value;
        }

        public static string GetKeyByType(Type type)
        {
            if (type == null)
            {
                var exception = new ArgumentNullException(nameof(type));
                Debug.LogException(exception);

                return string.Empty;
            }

            return type.ToString();
        }

        public static string GetKeyByType(object target)
        {
            if (target == null)
            {
                var exception = new ArgumentNullException(nameof(target));
                Debug.LogException(exception);

                return string.Empty;
            }

            var type = target.GetType();
            return GetKeyByType(type);
        }

        public static string GetKeyByType<T>()
        {
            var type = typeof(T);
            return GetKeyByType(type);
        }

        public static bool ValidateKey(string value, bool logException = true)
        {
            var isInvalid = string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value);
            if (isInvalid && logException)
            {
                var message = $"Invalid {nameof(value)}({value})";
                var exception = new ArgumentException(message);
                Debug.LogException(exception);
            }

            return !isInvalid;
        }

        public static bool ValidateType(Type type, bool logException = true)
        {
            var isValid = type.IsSerializable;
            if (!isValid && logException)
            {
                var message = $"Invalid {nameof(type)}({type.Name})";
                var exception = new ArgumentException(message);
                Debug.LogException(exception);
            }

            return isValid;
        }

        public static bool ValidateType<T>(bool logException = true)
        {
            var type = typeof(T);
            return ValidateType(type, logException);
        }
    }
}