using Microsoft.AspNetCore.Components;

namespace WePing.Components
{
    public interface IToolBoxButton
    {
        EventCallback<ElementReference> OnClick { get; set; }

        Widget Widget { get; set; }


    }

    public interface IStateToolBoxButton<TState> : IToolBoxButton
    {
        TState State { get; set; }
    }
}
