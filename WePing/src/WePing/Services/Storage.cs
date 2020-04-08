namespace WePing.Services
{/*
    public class StorageEventArgs : EventArgs
    {
        public string Key { get; set; }
        public object OldValue { get; set; }
        public object NewValue { get; set; }
    }
    public abstract class StorageBase
    {
        private readonly IJsRuntimeAccess _jsRuntime;
        private readonly string _fullTypeName;

        private EventHandler<StorageEventArgs> _storageChanged;

        protected abstract string StorageTypeName { get; }

        protected internal StorageBase(IJSRuntime jsRuntime)
        {
            if (jsRuntime is IJSInProcessRuntime rt)
                _jsRuntime = new ClientSideJsRuntimeAccess(rt);
            else _jsRuntime = new ServerSideJsRuntimeAccess(jsRuntime);
            _fullTypeName = GetType().FullName.Replace('.', '_');
        }

        public void Clear()
        {
            _jsRuntime.JsRuntimeInvoke<object>($"{_fullTypeName}.Clear");
        }

        public async ValueTask ClearAsync(CancellationToken cancellationToken = default)
        {
            await _jsRuntime.JsRuntimeInvokeAsync<object>($"{_fullTypeName}.Clear", Enumerable.Empty<object>(), cancellationToken);
        }

        public string GetItem(string key)
        {
            return _jsRuntime.JsRuntimeInvoke<string>($"{_fullTypeName}.GetItem", key);
        }

        public ValueTask<string> GetItemAsync(string key, CancellationToken cancellationToken = default)
        {
            return _jsRuntime.JsRuntimeInvokeAsync<string>($"{_fullTypeName}.GetItem", new object[] { key }, cancellationToken);
        }

        public T GetItem<T>(string key)
        {
            var json = GetItem(key);
            return string.IsNullOrEmpty(json) ? default : JsonSerializer.Deserialize<T>(json);
        }

        public async Task<T> GetItemAsync<T>(string key, CancellationToken cancellationToken = default)
        {
            var json = await GetItemAsync(key, cancellationToken);
            return string.IsNullOrEmpty(json) ? default : JsonSerializer.Deserialize<T>(json);
        }

        public string Key(int index)
        {
            return _jsRuntime.JsRuntimeInvoke<string>($"{_fullTypeName}.Key", index);
        }

        public ValueTask<string> KeyAsync(int index, CancellationToken cancellationToken = default)
        {
            return _jsRuntime.JsRuntimeInvokeAsync<string>($"{_fullTypeName}.Key", new object[] { index }, cancellationToken);
        }

        public int Length => _jsRuntime.JsRuntimeInvoke<int>($"{_fullTypeName}.Length");

        public ValueTask<int> LengthAsync(CancellationToken cancellationToken = default) => _jsRuntime.JsRuntimeInvokeAsync<int>($"{_fullTypeName}.Length", Enumerable.Empty<object>(), cancellationToken);

        public void RemoveItem(string key)
        {
            _jsRuntime.JsRuntimeInvoke<object>($"{_fullTypeName}.RemoveItem", key);
        }

        public async ValueTask RemoveItemAsync(string key, CancellationToken cancellationToken = default)
        {
            await _jsRuntime.JsRuntimeInvokeAsync<object>($"{_fullTypeName}.RemoveItem", new object[] { key }, cancellationToken);
        }

        public void SetItem(string key, string data)
        {
            _jsRuntime.JsRuntimeInvoke<object>($"{_fullTypeName}.SetItem", key, data);
        }

        public async ValueTask SetItemAsync(string key, string data, CancellationToken cancellationToken = default)
        {
            await _jsRuntime.JsRuntimeInvokeAsync<object>($"{_fullTypeName}.SetItem", new object[] { key, data }, cancellationToken);
        }

        public void SetItem(string key, object data)
        {
            SetItem(key, JsonSerializer.Serialize(data));
        }

        public ValueTask SetItemAsync(string key, object data, CancellationToken cancellationToken = default)
        {
            return SetItemAsync(key, JsonSerializer.Serialize(data), cancellationToken);
        }

        public string this[string key]
        {
            get => _jsRuntime.JsRuntimeInvoke<string>($"{_fullTypeName}.GetItemString", key);
            set => _jsRuntime.JsRuntimeInvoke<object>($"{_fullTypeName}.SetItemString", key, value);
        }

        public string this[int index]
        {
            get => _jsRuntime.JsRuntimeInvoke<string>($"{_fullTypeName}.GetItemNumber", index);
            set => _jsRuntime.JsRuntimeInvoke<object>($"{_fullTypeName}.SetItemNumber", index, value);
        }

        public event EventHandler<StorageEventArgs> StorageChanged
        {
            add
            {
                if (_storageChanged == null)
                {
                    _jsRuntime.JsRuntimeInvokeAsync<object>(
                        $"{_fullTypeName}.AddEventListener",
                        DotNetObjectReference.Create(this)
                    );
                }
                _storageChanged += value;
            }
            remove
            {
                _storageChanged -= value;
                if (_storageChanged == null)
                {
                    _jsRuntime.JsRuntimeInvokeAsync<object>($"{_fullTypeName}.RemoveEventListener");
                }
            }
        }

        [JSInvokable]
        public virtual void OnStorageChanged(string key, object oldValue, object newValue)
        {
            EventHandler<StorageEventArgs> handler = _storageChanged;
            handler?.Invoke(this, new StorageEventArgs
            {
                Key = key,
                OldValue = oldValue,
                NewValue = newValue,
            });
        }



    }

    public sealed class LocalStorage : StorageBase
    {
        protected override string StorageTypeName => nameof(LocalStorage);

        public LocalStorage(IJSRuntime jsRuntime) : base(jsRuntime)
        {
        }
    }

    public sealed class SessionStorage : StorageBase
    {
        protected override string StorageTypeName => nameof(SessionStorage);

        public SessionStorage(IJSRuntime jsRuntime) : base(jsRuntime)
        {
        }
    }

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddStorage(this IServiceCollection services)
        {
            services.TryAddScoped<LocalStorage>();
            services.TryAddScoped<SessionStorage>();
            return services;
        }
    }*/
}