// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using CorrelationId;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Softeq.NetKit.Payments.ExceptionHandling;
using Softeq.NetKit.Payments.Logger;
using Softeq.NetKit.Payments.SQLRepository;
using Softeq.NetKit.Payments.Utility;
using Swashbuckle.AspNetCore.Swagger;

namespace Softeq.NetKit.Payments
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IContainer ApplicationContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvcCore(o =>
                {
                    o.Filters.Add(typeof(GlobalExceptionFilter));
                })
                .AddApiExplorer()
                .AddAuthorization()
                .AddJsonFormatters();

            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.ApiSecret = Configuration["Authentication:Secret"];
                    options.Authority = Configuration["Authentication:Authority"];
                    options.RequireHttpsMetadata = false;

                    options.ApiName = "api";
                });

            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddCors();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info {Title = "API doc", Version = "v1"});
            });

            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);
            containerBuilder.RegisterAssemblyModules(typeof(Startup).Assembly);

            containerBuilder.AddLogger();

            ApplicationContainer = containerBuilder.Build();

            return new AutofacServiceProvider(ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime applicationLifetime, IDatabaseInitializer seeder)
        {
            applicationLifetime.ApplicationStopping.Register(OnShutdown, app);

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseExceptionHandler(options =>
            {
                options.Run(async c => await ExceptionHandler.Handle(c, loggerFactory));
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCorrelationId();

            app.UseAuthentication();

            #region Swagger

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API");
            });

            #endregion

            #region Db Seed

            seeder.Seed().Wait();

            #endregion

            app.UseMvc();
        }

        private void OnShutdown(object builder)
        {
            if (builder is IApplicationBuilder applicationBuilder)
            {
                var telemetryClient = applicationBuilder.ApplicationServices.GetService<TelemetryClient>();
                if (telemetryClient != null)
                {
                    telemetryClient.Flush();
                    //Wait while the data is flushed
                    System.Threading.Thread.Sleep(1000);
                }

                Log.CloseAndFlush();
            }
        }
    }
}