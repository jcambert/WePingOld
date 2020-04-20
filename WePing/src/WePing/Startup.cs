using MicroS_Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using WeChart;
using WeCommon;
using WePing.Components;
using WePing.Services;
using WeRedux;
using WeReduxBlazor;
using WeStrap;

namespace WePing
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLocalization(o =>
            {
                o.ResourcesPath = "Resources";
            });
            services.AddRazorPages()
                .AddDataAnnotationsLocalization()
                .AddJsonOptions(opt =>
                opt.JsonSerializerOptions.IgnoreNullValues = true
                );
            services.AddServerSideBlazor();
            services.AddSingleton(Configuration.GetOptions<TilesOptions>(TilesOptions.TILES_OPTIONS));
            services.AddSingleton<Helper>();
            services.AddScoped(typeof(IPagedResultWithLinks<>), typeof(PagedResultWithLinks<>));

            services.AddHttpClient<ISpidService, SpidService>(SpidService.SPID, c =>
              {
                  c.BaseAddress = new Uri(Configuration.GetValue<string>(SpidService.SPID + ":url"));
              });


            services.AddQueries(typeof(WePing.domain.Clubs.Queries.BrowseClubs).Assembly);


            services.AddRedux<WePingState, IAction>("WePingState", null, null, redux => redux.Clear()
            );

            services.AddScoped<IActionService, ActionService>();
            services.AddWeChart<IChartConfiguration, ChartConfiguration>(cfg =>
            {
                cfg.Type = ChartType.Bar.ToDescriptionString();
                cfg.Options = new Options() { Responsive = true };
            });
            services.AddWeStrap();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseQueries();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
