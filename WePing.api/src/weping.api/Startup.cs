using Autofac;
using Consul;
using MicroS_Common;
using MicroS_Common.Authentication;
using MicroS_Common.Consul;
using MicroS_Common.Dispatchers;
//using MicroS_Common.Excel;
using MicroS_Common.Jeager;
using MicroS_Common.Messages;
using MicroS_Common.Mvc;
using MicroS_Common.RabbitMq;
using MicroS_Common.Redis;
using MicroS_Common.RestEase;
using MicroS_Common.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using weping.api.Services;

namespace WePing.api
{
    public class Startup
    {
        private static readonly string[] Headers = new[] { "X-Operation", "X-Resource", "X-Total-Count" };

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomMvc();
            services.AddSwaggerDocs();
            services.AddConsul();
            services.AddJwt();
            services.AddJaeger();
            //services.AddOpenTracing();
            services.AddRedis();
            services.AddAuthorization(x => x.AddPolicy("admin", p => p.RequireRole("admin")));
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", cors =>
                        cors
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            //.AllowCredentials()
                            .WithExposedHeaders(Headers));
            });
            services.RegisterServiceForwarder<ISpidService>("spid-service");
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                    .AsImplementedInterfaces();

            builder.AddRabbitMq();
            builder.AddDispatchers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime applicationLifetime, IConsulClient client,
            IStartupInitializer startupInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");
            app.UseAllForwardedHeaders();
            app.UseSwaggerDocs();
            app.UseErrorHandler();
            app.UseAuthentication();
            app.UseAccessTokenValidator();
            app.UseServiceId();

#pragma warning disable MVC1005
            app.UseMvc();
#pragma warning restore MVC1005
            app.UseRabbitMq().SubscribeEvent<IRejectedEvent>(onError: (ee, e) =>
            {
                return null;
            });

            var consulServiceId = app.UseConsul();
            applicationLifetime.ApplicationStopped.Register(() =>
            {
                client.Agent.ServiceDeregister(consulServiceId);

            });

            startupInitializer.InitializeAsync();


        }
    }
}
