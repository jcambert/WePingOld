using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WePing.Pages
{
    public  abstract partial class PingBasePage: ComponentBase
    {
        protected virtual Task LoadDatas() => Task.CompletedTask;
    }
}
