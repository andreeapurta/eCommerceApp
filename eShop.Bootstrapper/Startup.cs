using eShop.Data.InMemory;
using eShop.UseCases.Interfaces.DataStore;
using eShop.UseCases.SearchProductScreen;
using eShop.UseCases.ViewProductScreen;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using eShop.Bootstrapper.Stores.CounterStore;
using eShop.Bootstrapper.Data;

namespace eShop.Bootstrapper
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
            services.AddRazorPages();
            services.AddServerSideBlazor();
            //sigleton == a single static instance
            services.AddSingleton<WeatherForecastService>();
            //transient == always a new instance
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ISearchProductUseCase, SearchProductUseCase>();
            services.AddTransient<IViewProductUseCase, ViewProductUseCase>();
            //scoped == one instane per connection! When you refresh the page, signalR deconnects => it creates a different connection -> a new store
            //scoped - can be used for  state management related to user ( not singleton, beause we dont want all the users to see the same data)
            services.AddScoped<CounterStore>();
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
                // The default HSTS value is 30 days. You may want to change this for production
                // scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

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