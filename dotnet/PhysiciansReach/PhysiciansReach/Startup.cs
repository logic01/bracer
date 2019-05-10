using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PR.Business;
using PR.Business.Business;
using PR.Business.Interfaces;
using PR.Constants.Configurations;
using PR.Data.Models;

namespace PhysiciansReach
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }
        public Startup(IHostingEnvironment env)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }

        private readonly string MyAllowSpecificOrigins = "localOrigins";

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
            ConfigureAppSettings(services);
            ConfigureDependecyInjection(services);
        }

        private void ConfigureAppSettings(IServiceCollection services)
        {
            // Add functionality to inject IOptions<T>
            services.AddOptions();

            // Add our Config object so it can be injected
            services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));
            services.Configure<SmtpSettings>(Configuration.GetSection("SmtpSettings"));
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
            var connection = Configuration.GetValue<string>("ConnectionStrings:PRContext");
            services.AddDbContext<DataContext>(options => options.UseSqlServer(connection));
        }

        private void ConfigureDependecyInjection(IServiceCollection services)
        {
            services.AddTransient<IAdminBusiness, AdminBusiness>();
            services.AddTransient<IIntakeFormBusiness, IntakeFormBusiness>();
            services.AddTransient<IAgentBusiness, AgentBusiness>();
            services.AddTransient<IPhysicianBusiness, PhysicianBusiness>();
            services.AddTransient<IVendorBusiness, VendorBusiness>();
            services.AddTransient<ILoginBusiness, LoginBusiness>();
            services.AddTransient<IPatientBusiness, PatientBusiness>();
            services.AddTransient<ILoggingBusiness, LoggingBusiness>();
            services.AddTransient<IDocumentBusiness, DocumentBusiness>();
            services.AddTransient<IEmailBusiness, EmailBusiness>();
        }
    }
}
