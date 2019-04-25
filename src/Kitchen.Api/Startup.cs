using Kitchen.Api.Data;
using Kitchen.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Steeltoe.CloudFoundry.Connector.MySql.EFCore;
using Steeltoe.Extensions.Configuration.CloudFoundry;
using Steeltoe.Management.CloudFoundry;

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
            services.ConfigureCloudFoundryOptions(Configuration);
            services.AddCloudFoundryActuators(Configuration);

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                if (HostingEnvironment.IsDevelopment())
                    options.UseInMemoryDatabase("Kitchen.Api"); // In-Memory for local development
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

            Seed.SeedApplication(app.ApplicationServices); // demo purposes only

            app.UseCors(config =>
            {
                config.AllowAnyOrigin();
                config.AllowAnyMethod();
                config.AllowAnyHeader();
            });

            app.UseMvc();
        }
    }
}
