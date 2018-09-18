using Kloud.CodeTest.Core.Configurations;
using Kloud.CodeTest.Core.Contracts.DataProviders;
using Kloud.CodeTest.Core.Contracts.Services;
using Kloud.CodeTest.Core.DataProviders;
using Kloud.CodeTest.Core.Dto;
using Kloud.CodeTest.Core.Services;
using Kloud.CodeTest.Web.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace Kloud.CodeTest.Web
{
    public class Startup
    {
        public IHostingEnvironment HostingEnvironment { get; private set; }
        public IConfiguration Configuration { get; private set; }

        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources. Dont need this step if we keep configure file in root level
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            var settingsSection = Configuration.GetSection("AppSettings");
            var settings = settingsSection.Get<AppSettings>();

            // Inject AppSettings
            services.Configure<AppSettings>(settingsSection);

            services.AddTransient<ICarDataProvider, CarDataProvider>();
            services.AddTransient<ICarDataService, CarDataService>();

            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CarDataDto, HomeViewModel>();
            });

            // Inject Mapper
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            var restClient = new HttpClient()
            {
                BaseAddress = new Uri(settings.WebServiceUrl)
            };

            services.AddSingleton<HttpClient>(restClient);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}