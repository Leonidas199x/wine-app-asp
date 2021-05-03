using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using wine_app.Domain;
using wine_app.Domain.Country;
using wine_app.Domain.Drinker;
using wine_app.Domain.Grape;
using wine_app.Domain.MapBox;
using wine_app.Domain.Region;
using wine_app.Domain.StopperType;
using wine_app.Mappers;

namespace wine_app
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
            services.AddControllersWithViews();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<IGrapeService, GrapeService>();
            services.AddTransient<IGrapeRepository, GrapeRepository>();
            services.AddTransient<IRegionService, RegionService>();
            services.AddTransient<IRegionRepository, RegionRepository>();
            services.AddTransient<IHttpRequestHandler, HttpRequestHandler>();
            services.AddTransient<IMapBoxService, MapBoxService>();
            services.AddTransient<IMapBoxRepository>(x => 
            new MapBoxRepository(
                x.GetRequiredService<IHttpRequestHandler>(), 
                Configuration.GetSection("MapBoxApiKey").Value));
            services.AddTransient<IDrinkerService, DrinkerService>();
            services.AddTransient<IDrinkerRepository, DrinkerRepository>();
            services.AddTransient<IStopperTypeRepository, StopperTypeRepository>();
            services.AddTransient<IStopperTypeService, StopperTypeService>();

            //Register automapper
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddHttpClient(ApiNames.WineApi, c =>
            {
                c.BaseAddress = new Uri(Configuration.GetSection("WineApiBaseAddress").Value);
            });

            services.AddHttpClient(ApiNames.MapBoxApi, c =>
            {
                c.BaseAddress = new Uri(Configuration.GetSection("MapBoxApi").Value);
            });
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
