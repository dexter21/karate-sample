using api.Auth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace api
{
    public class Startup
    {
        public Startup(IWebHostEnvironment environment)
            => Configuration = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<Program>()
                .AddEnvironmentVariables()
                .Build();

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
            => services
                .AddControllers().Services
                .AddSingleton<ITokenStorage, TokenStorage>()
                .AddScoped<AuthAttribute>()
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = "forbidScheme";
                    options.DefaultForbidScheme = "forbidScheme";
                    options.AddScheme<CustomAuthenticationHandler>("forbidScheme", "Handle Forbidden");
                });

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
                .UseHttpsRedirection()
                .UseRouting()
                .UseAuthentication()
                .UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
