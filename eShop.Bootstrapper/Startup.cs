using eShop.Bootstrapper.Stores.CounterStore;
using eShop.Business.Services;
using eShop.Data.InMemory;
using eShop.StateStore.LocalStorage;
using eShop.UseCases.Interfaces.DataStore;
using eShop.UseCases.Interfaces.StateStore;
using eShop.UseCases.Interfaces.UI;
using eShop.UseCases.SearchProductScreen;
using eShop.UseCases.ShoppingCartScreen;
using eShop.UseCases.ViewProductScreen;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace eShop.Bootstrapper
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            //sigleton == a single static instance
            //transient == always a new instance
            services.AddSingleton<IProductRepository, ProductRepository>();
            services.AddSingleton<IOrderRepository, OrderRepository>();
            services.AddScoped<IShoppingCartStateStore, ShoppingCartStateStore>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IShoppingCart, eShop.ShoppingCart.LocalStorage.ShoppingCart>();
            services.AddTransient<IViewProductUseCase, ViewProductUseCase>();
            services.AddTransient<IViewShoppingCartUseCase, ViewShoppingCartUseCase>();
            services.AddTransient<ISearchProductUseCase, SearchProductUseCase>();
            services.AddTransient<IAddProductToCartUseCase, AddProductToCartUseCase>();
            services.AddTransient<IDeleteProductUseCase, DeleteProductUseCase>();
            services.AddTransient<IUpdateQuantityUseCase, UpdateQuantityUseCase>();

            //scoped == one instane per connection! When you refresh the page, signalR deconnects => it creates a different connection -> a new store
            //scoped - can be used for  state management related to user ( not singleton, beause we dont want all the users to see the same data)
            services.AddScoped<CounterStore>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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