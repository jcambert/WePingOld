using MicroS_Common;
using MicroS_Common.Controllers;
using MicroS_Common.Dispatchers;
using MicroS_Common.RabbitMq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using OpenTracing;

namespace weping.api.Controllers
{
 
    public class HomeController : BaseController
    {
        public HomeController(IDispatcher dispatcher, IConfiguration configuration, IOptions<AppOptions> appOptions) : base(dispatcher, configuration, appOptions)
        {
        }
    }
}