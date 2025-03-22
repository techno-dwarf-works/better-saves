#if BETTER_SERVICES
using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Saves.Runtime.Interfaces;
using Better.Services.Runtime;

namespace Better.Saves.Runtime
{
    [Serializable]
    public class SavesService : PocoService, ISaveSystem
    {
        private ISaveSystem _internalSystem;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            _internalSystem = new InternalSavesSystem();
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        #region ISaveSystem

        public event ISaveSystem.OnItemsCleared AllCleared
        {
            add => _internalSystem.AllCleared += value;
            remove => _internalSystem.AllCleared -= value;
        }

        public event ISaveSystem.OnItemCleared ItemCleared
        {
            add => _internalSystem.ItemCleared += value;
            remove => _internalSystem.ItemCleared -= value;
        }

        public bool Has<T>(string key) => _internalSystem.Has<T>(key);
        public bool Has<T>() => _internalSystem.Has<T>();
        public T Load<T>(string key, T defaultValue = default) => _internalSystem.Load(key, defaultValue);
        public T Load<T>(T defaultValue = default) => _internalSystem.Load(defaultValue);
        public void Save<T>(string key, T data) => _internalSystem.Save(key, data);
        public void Save<T>(T data) => _internalSystem.Save(data);
        public bool Delete<T>(string key) => _internalSystem.Delete<T>(key);
        public void Delete<T>() => _internalSystem.Delete<T>();
        public void DeleteAll() => _internalSystem.DeleteAll();

        #endregion
    }
}
#endif