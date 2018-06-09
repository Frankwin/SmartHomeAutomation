using System;
using System.Buffers;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SmartHomeAutomation.Domain.Models;
using SmartHomeAutomation.Services.Interfaces;
using SmartHomeAutomation.Services.Interfaces.Account;
using SmartHomeAutomation.Services.Interfaces.Device;
using SmartHomeAutomation.Services.Interfaces.Settings;
using SmartHomeAutomation.Services.Interfaces.User;
using SmartHomeAutomation.Services.Services;
using SmartHomeAutomation.Services.Services.Account;
using SmartHomeAutomation.Services.Services.Device;
using SmartHomeAutomation.Services.Services.Settings;
using SmartHomeAutomation.Services.Services.User;
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
            services.AddMvc(options =>
            {
                options.OutputFormatters.Clear();
                options.OutputFormatters.Add(new JsonOutputFormatter(new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                }, ArrayPool<char>.Shared));
            });
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
            // Account Services
            services.AddScoped<IAccountService, AccountService>();
            // Device Services
            services.AddScoped<IDeviceCategoryService, DeviceCategoryService>();
            services.AddScoped<IDeviceService, DeviceService>();
            services.AddScoped<IDeviceTypeService, DeviceTypeService>();
            services.AddScoped<IManufacturerService, ManufacturerService>();
            // Settings Services
            services.AddScoped<IOwnedDeviceService, OwnedDeviceService>();
            services.AddScoped<IRoomService, RoomService>();
            // User Services
            services.AddScoped<IUserService, UserService>();
            
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
