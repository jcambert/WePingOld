using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using WePing.Services;
using WeRedux;
using WeReduxBlazor;
using WeStrap;

namespace WePing.Components.Base
{
    public abstract class WePingComponentBase : WeTag
    {
        [CascadingParameter] protected ReduxDevTools<WePingState, IAction> AppState { get; set; }
        protected IStore<WePingState, IAction> Store => AppState.Store;
        [Inject] protected LocalStorage Storage { get; set; }
        [Inject] protected IRedux<WePingState, IAction> Redux { get; set; }
        [Inject] protected ISpidService Spid { get; set; }

        protected async Task StateHasChangedAsync()
        {
            await this.InvokeAsync(() => StateHasChanged());
        }
        public async Task ClearStorage()
        {
            await Storage.ClearAsync();
            Redux.Store.Reset();
        }

        public abstract void LoadData();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            if (firstRender)
            {
                Store.OnChanged.Subscribe(o =>
                {
                    StateHasChanged();
                });
                Store.OnTravelTo.Subscribe(timeLaps =>
                {
                    StateHasChanged();
                });
                Store.On<LoadingAction>().Subscribe(action =>
                {
                    Store.State.IsLoading = true;
                    Store.StateChanged(action);
                });
            }
        }
    }
}
