using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PR.Business;
using PR.Business.Business;
using PR.Business.Interfaces;
using PR.Data.Models;

namespace PhysiciansReach
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "localOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials()
                           .AllowAnyOrigin();
                });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            ConfigureDatabase(services);
            ConfigureDependecyInjection(services);
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
                app.UseHsts();
            }

            app.UseCors(MyAllowSpecificOrigins);

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private void ConfigureDatabase(IServiceCollection services)
        {
            var connection = @"Server=(localdb)\mssqllocaldb;Database=PhysiciansReach;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<DataContext>(options => options.UseSqlServer(connection));
        }

        private void ConfigureDependecyInjection(IServiceCollection services)
        {
            services.AddTransient<IAdminBusiness, AdminBusiness>();
            services.AddTransient<IAgentBusiness, AgentBusiness>();
            services.AddTransient<IPhysicianBusiness, PhysicianBusiness>();
            services.AddTransient<IVendorBusiness, VendorBusiness>();
            services.AddTransient<ILoginBusiness, LoginBusiness>();
        }
    }
}
