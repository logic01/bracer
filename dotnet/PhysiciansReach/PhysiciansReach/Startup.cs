using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PR.Api.Filters;
using PR.Business;
using PR.Business.Business;
using PR.Business.Interfaces;
using PR.Constants.Configurations;
using PR.Data.Models;
using PR.Export;
using System;
using System.Text;

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
            ConfigureSecurity(services);
            ConfigureAppSettings(services);
            ConfigureDependecyInjection(services);
        }

        private void ConfigureSecurity(IServiceCollection services)
        {
            var securitySettings = Configuration.GetSection("SecuritySettings");
            var appSettings = securitySettings.Get<SecuritySettings>();
            var key = Encoding.UTF8.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.FromMinutes(0),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false                    
                };
            });
        }

        private void ConfigureAppSettings(IServiceCollection services)
        {
            // Add functionality to inject IOptions<T>
            services.AddOptions();

            // Add our Config object so it can be injected
            services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));
            services.Configure<SmtpSettings>(Configuration.GetSection("SmtpSettings"));
            services.Configure<SecuritySettings>(Configuration.GetSection("SecuritySettings"));
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

            app.UseAuthentication();
            app.UseMiddleware(typeof(ExceptionMiddleware));

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
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IAdminBusiness, AdminBusiness>();
            services.AddTransient<IIntakeFormBusiness, IntakeFormBusiness>();
            services.AddTransient<IAgentBusiness, AgentBusiness>();
            services.AddTransient<IPhysicianBusiness, PhysicianBusiness>();
            services.AddTransient<IVendorBusiness, VendorBusiness>();
            services.AddTransient<IAuthorizationBusiness, AuthorizationBusiness>();
            services.AddTransient<IPatientBusiness, PatientBusiness>();
            services.AddTransient<ILoggingBusiness, LoggingBusiness>();
            services.AddTransient<IDocumentBusiness, DocumentBusiness>();
            services.AddTransient<IEmailBusiness, EmailBusiness>();
            services.AddTransient<IDocumentGenerator, DocumentGenerator>();
            services.AddTransient<ISignatureBusiness, SignatureBusiness>();
            services.AddTransient<IUserAccountBusiness, UserAccountBusiness>();
        }
    }
}
