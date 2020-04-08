using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace RxTest
{
    public interface IStore<TState, TAction> : IDisposable
        where TState : new()
        where TAction : IAction
    {
    }
    public class Store<TState, TAction> : IStore<TState, TAction>
    where TState : new()
    where TAction : IAction
    {
        private Subject<TAction> Dispatcher = new Subject<TAction>();
        private TState state;

        public Store()
        {
            state = new TState();
        }
        public void Dispose()
        {

        }
        public TState State => state;
        public IObservable<TAction> On<T>()
          where T : TAction
        {

            return Dispatcher.AsObservable().Where(t => t.GetType() == typeof(T));
        }

        public void Dispatch<T>(T action) where T : TAction, new()
        {
            Dispatcher.OnNext(action);
        }


        public void Dispatch<T>(Action<T> action = null) where T : TAction, new()
        {
            T t = new T();
            action?.Invoke(t);
            Dispatch(t);
        }
    }
}
