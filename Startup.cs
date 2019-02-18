using Blazor.Extensions.Storage;
using BlazorClient.Services;
using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorClient
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
           
            services.AddStorage();
            services.AddTransient<IHttpService, HttpService>();
            services.AddTransient<AuthService>();
            services.AddSingleton<LogService>();
        }

        public void Configure(IBlazorApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
