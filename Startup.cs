using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using SportsStoreApi.Helpers;
using SportsStoreApi.Interfaces;
using SportsStoreApi.Services;

namespace SportsStoreApi
{
    public class Startup
    {
        // Config class is injected through di
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => {
                options.AddPolicy(
                    "CorsPolicy", 
                    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
                );
            });

            // Adds services for controllers to the specified Microsoft.Extensions.DependencyInjection.IServiceCollection. This method will not register services used for views or pages.
            services.AddControllers();

            // from the file {root}/appsettings.json
            // map the json "AppSettings": { ... } to the object AppSettings
            // then cast the json to AppSettings object
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();

            // convert string into byte[]
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            // configure the authentication method to be JWT
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
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddMemoryCache();
            
            // Database context
            // TODO: add config file
            // https://hackernoon.com/asp-net-core-how-to-use-dependency-injection-in-entity-framework-core-4388fc5c148b
            // services.AddDbContext<StoreContext>(options => options.UseMySQL(Configuration["ConnectionStrings:DefaultConnection"]));
            Console.WriteLine("services.AddDbContext<StoreContext>(");
            services.AddDbContext<StoreContext>(
                options => options.UseMySQL("server=mysql-db;database=sportstore;user=root;password=password"));

            // configure DI for application services
            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.0#service-lifetimes
            // Scoped lifetime services (AddScoped) are created once per client request (connection).
            Console.WriteLine("services.AddScoped");
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });

        }
    }
}
