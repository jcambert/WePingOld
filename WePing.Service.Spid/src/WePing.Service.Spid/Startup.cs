using Autofac;
using AutoMapper;
using Consul;
using MicroS_Common;
using MicroS_Common.Consul;
using MicroS_Common.Dispatchers;
//using MicroS_Common.Excel;
using MicroS_Common.Jeager;
using MicroS_Common.Mvc;
using MicroS_Common.RabbitMq;
using MicroS_Common.Redis;
using MicroS_Common.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using WePing.domain;
using WePing.domain.Services;
using WePing.Service.Spid.Services;

namespace WePing.Service.Spid
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomMvc(o =>
            {
                // o.Filters.Add(typeof(SpidExceptionFilter));
            });
            services.AddSwaggerDocs();
            services.AddConsul();
            services.AddJaeger();
            //services.AddOpenTracing();
            services.AddRedis();
            //services.AddInitializers(typeof(IMongoDbInitializer));
            services.AddHttpClient<ISpidRequest, SpidRequest>();//.AddPolicyHandler(GetRetryPolicy());
            services.AddAutoMapper(Assembly.GetEntryAssembly(), typeof(DomainProfile).Assembly);
            services.AddSingleton<SpidOptions>();
            services.AddSingleton<SpidRequester>();
            
            services.AddScoped<ISpidRequest, SpidRequest>();
            services.AddScoped<ICalculateurPoints, CalculateurPoints>();

        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly()).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof(DomainProfile).Assembly).AsImplementedInterfaces();
            builder.AddRabbitMq();
            // builder.AddMongo();
            // builder.AddMongoRepository<Product>("Products");
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
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseAllForwardedHeaders();
            app.UseSwaggerDocs();
            app.UseErrorHandler();
            app.UseServiceId();
            //app.UseExcelResponseMiddleware();
#pragma warning disable MVC1005
            app.UseMvc();
#pragma warning restore MVC1005
            app.UseRabbitMq()
                //.SubscribeCommand<CreateProduct>(onError: (c, e) => new CreateProductRejected(c.Id, e.Message, e.Code))
                //.SubscribeCommand<UpdateProduct>(onError: (c, e) => new UpdateProductRejected(c.Id, e.Message, e.Code))
                // .SubscribeCommand<DeleteProduct>(onError: (c, e) => new DeleteProductRejected(c.Id, e.Message, e.Code))
                // .SubscribeCommand<ReserveProducts>(onError: (c, e) => new ReserveProductsRejected(c.OrderId, e.Message, e.Code))
                // .SubscribeCommand<ReleaseProducts>(onError: (c, e) => new ReleaseProductsRejected(c.OrderId, e.Message, e.Code))
                ;

            var consulServiceId = app.UseConsul();
            applicationLifetime.ApplicationStopped.Register(() =>
            {
                client.Agent.ServiceDeregister(consulServiceId);
            });

            startupInitializer.InitializeAsync();
        }

        /* static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
         {
             return HttpPolicyExtensions
                 .HandleTransientHttpError()
                 .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                 .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
         }*/
    }
}
