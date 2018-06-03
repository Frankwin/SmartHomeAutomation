using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartHomeAutomation.Domain.Models;
using SmartHomeAutomation.Services.Interfaces;
using SmartHomeAutomation.Services.Services;
using Swashbuckle.AspNetCore.Swagger;

namespace SmartHomeAutomation.Api
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
            const string connection = @"Server=localhost;Database=SmartHomeAutomation;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<SmartHomeAutomationContext>(opt => opt.UseSqlServer(connection));
            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Version = "v1",
                        Title = "Smart Home Automation API", 
                        Description = "API for the Smart Home Automation software",
                        TermsOfService = "None",
                        Contact = new Contact
                        {
                            Name = "Frankwin.Hooglander",
                            Email = "frankwin.hooglander@gmail.com",
                            Url = "https://twitter.com/frankwinh"
                        },
                        License = new License
                        {
                            Name = "MIT License",
                            Url = "https://opensource.org/licenses/MIThttps://opensource.org/licenses/MIT"
                        }
                    });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Register application services
            services.AddScoped<ISmartHomeAutomationService, SmartHomeAutomationService>();
            services.AddScoped<IManufacturerService, ManufacturerService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IDeviceCategoryService, DeviceCategoryService>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Smart Home Automation API V1");
                c.RoutePrefix = string.Empty;
            });

            if (env.IsDevelopment())
            {
                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    if (!serviceScope.ServiceProvider.GetService<SmartHomeAutomationContext>().AllMigrationsApplied())
                    {
                        serviceScope.ServiceProvider.GetService<SmartHomeAutomationContext>().Database.Migrate();
                        serviceScope.ServiceProvider.GetService<SmartHomeAutomationContext>().EnsureSeeded();
                    }
                }

                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
