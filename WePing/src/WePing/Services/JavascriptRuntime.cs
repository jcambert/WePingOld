namespace WePing.Services
{
    /* public class JavascriptRuntime
     {
         private  IJsRuntimeAccess _jsRuntime;
         private IJSRuntime _js;
         public JavascriptRuntime()
         {

         }

         [Inject]
         public IJSRuntime Runtime {
             get => _js;
             set
             {
                 _js = value;
                 if (value is IJSInProcessRuntime rt)
                     _jsRuntime = new ClientSideJsRuntimeAccess(rt);
                 else _jsRuntime = new ServerSideJsRuntimeAccess(value);
             }
         }

         public IJsRuntimeAccess Interop => _jsRuntime;
     }*/
    /*
        public static class JsExtension
        {
            public static T JsRuntimeInvoke<T>(this IJsRuntimeAccess jsRuntime, string identifier, params object[] args)
            {
                return jsRuntime.Invoke<T>(identifier, args);
            }

            public static ValueTask<T> JsRuntimeInvokeAsync<T>(this IJsRuntimeAccess jsRuntime, string identifier, params object[] args)
            {
                return jsRuntime.InvokeAsync<T>(identifier, args);
            }

            public static ValueTask<T> JsRuntimeInvokeAsync<T>(this IJsRuntimeAccess jsRuntime, string identifier, IEnumerable<object> args, CancellationToken cancellationToken = default)
            {
                return jsRuntime.InvokeAsync<T>(identifier, args, cancellationToken);
            }
        }

        public interface IJsRuntimeAccess
        {
            T Invoke<T>(string identifier, params object[] args);

            ValueTask<T> InvokeAsync<T>(string identifier, params object[] args);

            ValueTask<T> InvokeAsync<T>(string identifier, IEnumerable<object> args,CancellationToken cancellationToken = default);
        }

        public abstract class JsRuntimeAccessBase<TJsRuntime> : IJsRuntimeAccess where TJsRuntime : IJSRuntime
        {
            protected TJsRuntime JsRuntime { get; }

            protected JsRuntimeAccessBase(TJsRuntime jsRuntime)
            {
                JsRuntime = jsRuntime;
            }

            public abstract T Invoke<T>(string identifier, params object[] args);

            public ValueTask<T> InvokeAsync<T>(string identifier, params object[] args)
            {
                return JsRuntime.InvokeAsync<T>(identifier, args);
            }

            public ValueTask<T> InvokeAsync<T>(string identifier, IEnumerable<object> args, CancellationToken cancellationToken = default)
            {
                return JsRuntime.InvokeAsync<T>(identifier, cancellationToken, args.ToArray());
            }
        }

        public class ServerSideJsRuntimeAccess : JsRuntimeAccessBase<IJSRuntime>
        {
            public ServerSideJsRuntimeAccess(IJSRuntime jsRuntime) : base(jsRuntime) { }

            public override T Invoke<T>(string identifier, params object[] args)
            {
                throw new NotSupportedException("Synchronous storage access is not supported in a server-side app. Please use the asynchronous implementation.");
            }
        }

        public class ClientSideJsRuntimeAccess : JsRuntimeAccessBase<IJSInProcessRuntime>
        {
            public ClientSideJsRuntimeAccess(IJSInProcessRuntime jsRuntime) : base(jsRuntime) { }

            public override T Invoke<T>(string identifier, params object[] args)
            {
                return JsRuntime.Invoke<T>(identifier, args);
            }
        }
        */
}
