// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using AspNetCoreMultitenancy.Database;
using AspNetCoreMultitenancy.Multitenancy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace AspNetCoreMultitenancy
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            CurrentEnvironment = env;
        }

        public IConfiguration Configuration { get; }
        private IWebHostEnvironment CurrentEnvironment { get; set; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Register Scoped DbContexts:
            services
                // Register the Tenant Database:
                .AddDbContext<TenantDbContext>(options => options.UseNpgsql("Host=localhost;Port=5432;Database=sampledb;Pooling=false;User Id=app_user;Password=app_user;"))
                // Register the Application Database:
                .AddDbContext<ApplicationDbContext>(options => options
                    .AddInterceptors(new PostgresTenantDbConnectionInterceptor())
                    .UseNpgsql("Host=localhost;Port=5432;Database=sampledb;Pooling=false;User Id=app_user;Password=app_user;"));

            services.AddControllers();
            if (CurrentEnvironment.IsDevelopment())
            {
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Multitenancy API", Version = "v1" });
                    c.OperationFilter<AddHeaderParameter>();
                });
            }
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
            }

            app.UseStaticFiles();

            app.UseMultiTenant();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
             if (env.IsDevelopment())
             {
                 app.UseSwagger();
                app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Multitenancy API V1"); });            
             }
        }
    }
}
