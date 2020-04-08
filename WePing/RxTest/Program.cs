using System;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RxTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var store = new Store<State, IAction>();

            store.On<LoadingAction>().Subscribe(state =>
            {
                Console.WriteLine($"On Loading Action");
                store.Dispatch<ChangedAction>();
            });
            store.On<LoadedAction>().Subscribe(action =>
            {
                Console.WriteLine($"On Loaded Action");
                store.Dispatch<ChangedAction>();
            });

            store.On<IncrementAction>().Subscribe(action =>
            {
                Console.WriteLine($"On Increment Action:{store.State.Counter += ((IncrementAction)action).Step}");
                store.Dispatch<ChangedAction>();

            });
            store.On<IncrementAsyncAction>().Subscribe(state =>
            {
                CancellationTokenSource source = new CancellationTokenSource();
                var t = Task.Run(async delegate
                {
                    Console.WriteLine("waiting 3s");
                    await Task.Delay(TimeSpan.FromSeconds(3), source.Token);
                    return;
                });
                t.Wait();
                Console.WriteLine($"On async Increment Action:{++store.State.Counter}");
                store.Dispatch<ChangedAction>();

            });
            store.On<DecrementAction>().Subscribe(state =>
            {
                Console.WriteLine($"On Decrement Action:{--store.State.Counter}");
                store.Dispatch<ChangedAction>();

            });
            store.On<ChangedAction>().Subscribe(action =>
            {
                Console.WriteLine("Changed was  made");
            });
            store.Dispatch<LoadingAction>();
            store.Dispatch<IncrementAction>();
            store.Dispatch<IncrementAction>();
            store.Dispatch<DecrementAction>();
            store.Dispatch<IncrementAsyncAction>();
            store.Dispatch<IncrementAction>();
            store.Dispatch<IncrementAction>();
            store.Dispatch<IncrementAction>();
            store.Dispatch<IncrementAction>(action =>
            {
                action.Step = 100;
            });
            store.Dispatch<LoadedAction>();
            Console.WriteLine("Press a key....");
            Console.ReadLine();
        }
    }


    public interface IAction
    {
        string Name { get; }
    }
    public class ChangedAction : IAction
    {
        public string Name => nameof(ChangedAction);
    }
    public class LoadingAction : IAction
    {
        public string Name => nameof(LoadingAction);
    }

    public class LoadedAction : IAction
    {
        public string Name => nameof(LoadedAction);
    }
    public class IncrementAction : IAction
    {
        public string Name => nameof(IncrementAction);
        public int Step { get; set; } = 1;
    }

    public class IncrementAsyncAction : IAction
    {
        public string Name => nameof(IncrementAsyncAction);
    }

    public class DecrementAction : IAction
    {
        public string Name => nameof(DecrementAction);
    }
    class State
    {
        public int Counter { get; set; } = 0;
    }
    /*class Sample<TAction>
        where TAction:IAction
    {
        public Subject<bool> EnabledSubject = new Subject<bool>();

        public readonly Store<State, IAction> store = new Store<State, IAction>();
        public Sample()
        {


        }

        public IObservable< IAction> On<T>()
           where T : TAction
        {
            return store.On<T>();
        }
        public void Dispatch<T>() where T:TAction
        {
            store.Dispatch<T>();
        }

    }*/
}
