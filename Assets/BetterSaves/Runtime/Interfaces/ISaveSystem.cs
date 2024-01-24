namespace Better.Saves.Runtime.Interfaces
{
    public interface ISaveSystem
    {
        /// <summary>
        /// Checks if a specific key exists for the specified type.
        /// </summary>
        /// <typeparam name="T">The type of data to check for.</typeparam>
        /// <param name="key">The key associated with the data.</param>
        /// <returns>True if the key exists; otherwise, false.</returns>
        public bool Has<T>(string key);

        /// <summary>
        /// Checks if there is any saved data for the specified type.
        /// </summary>
        /// <typeparam name="T">The type of data to check for.</typeparam>
        /// <returns>True if there is saved data; otherwise, false.</returns>
        public bool Has<T>();

        /// <summary>
        /// Loads data of the specified type using the provided key.
        /// </summary>
        /// <typeparam name="T">The type of data to load.</typeparam>
        /// <param name="key">The key associated with the data.</param>
        /// <param name="defaultValue">The default value to return if no data is found.</param>
        /// <returns>The loaded data or the default value if not found.</returns>
        public T Load<T>(string key, T defaultValue = default);

        /// <summary>
        /// Loads data of the specified type without using a key.
        /// Key will be determined based on the type of the required object.
        /// </summary>
        /// <typeparam name="T">The type of data to load.</typeparam>
        /// <param name="defaultValue">The default value to return if no data is found.</param>
        /// <returns>The loaded data or the default value if not found.</returns>
        public T Load<T>(T defaultValue = default);

        /// <summary>
        /// Saves data of the specified type using the provided key.
        /// </summary>
        /// <typeparam name="T">The type of data to save.</typeparam>
        /// <param name="key">The key associated with the data.</param>
        /// <param name="data">The data to save.</param>
        public void Save<T>(string key, T data);

        /// <summary>
        /// Saves data of the specified type without using a key.
        /// Key will be determined based on the type of the provided object.
        /// </summary>
        /// <typeparam name="T">The type of data to save.</typeparam>
        /// <param name="data">The data to save.</param>
        public void Save<T>(T data);

        /// <summary>
        /// Deletes saved data of the specified type using the provided key.
        /// </summary>
        /// <typeparam name="T">The type of data to delete.</typeparam>
        /// <param name="key">The key associated with the data.</param>
        public bool Delete<T>(string key);

        /// <summary>
        /// Deletes all saved data of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of data to delete.</typeparam>
        public void Delete<T>();

        /// <summary>
        /// Deletes all saved data across all types.
        /// </summary>
        public void DeleteAll();
    }
}