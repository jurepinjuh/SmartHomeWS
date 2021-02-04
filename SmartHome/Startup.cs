using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SmartHome.Filters;
using SmartHome.Hubs;
using SmartHome.Models;
using SmartHome.Services;

namespace SmartHome
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsP", builder =>
                {
                    builder.AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed(s=>true).AllowCredentials();
                });
            });


            services.Configure<SmartHomeDatabaseSettings>(
                Configuration.GetSection(nameof(SmartHomeDatabaseSettings))
                );

            services.AddSingleton<ISmartHomeDatabaseSettings>(provider =>
            provider.GetRequiredService<IOptions<SmartHomeDatabaseSettings>>().Value);

            services.AddScoped<SmartHomeDataService>();

            services.AddSignalR();
            services.AddControllers();
            services.AddMvc(options =>
            {
                options.Filters.Add(new RequestValidationFilter());
               
            }).AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblyContaining<Startup>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("CorsP");
     

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<NotifyHub>("notify");
                endpoints.MapHub<SettingsHub>("settings");
            });
        }
    }
}
