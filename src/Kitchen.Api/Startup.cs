using Kitchen.Api.Data;
using Kitchen.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Steeltoe.CloudFoundry.Connector.MySql.EFCore;

namespace Kitchen.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        private IConfiguration Configuration { get; }
        private IHostingEnvironment HostingEnvironment { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                if (HostingEnvironment.IsDevelopment())
                    options.UseInMemoryDatabase("Kitchen.Api"); // in-memory for local development
                else
                    options.UseMySql(Configuration); // Steeltoe MySql connector for PCF
            });

            services.AddTransient<IInventoryRepository, InventoryRepository>();

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using (var scope = app.ApplicationServices.CreateScope()) // demo purposes only
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                Seed.SeedData(context);
            }

            app.UseCors(config =>
            {
                config.AllowAnyOrigin();
                config.AllowAnyMethod();
                config.AllowAnyHeader();
            });

            app.UseMvcWithDefaultRoute();
        }
    }
}
