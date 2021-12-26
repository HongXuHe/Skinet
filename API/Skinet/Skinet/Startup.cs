using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Skinet.Core.Interfaces;
using Skinet.Infrastructure.Data;

namespace Skinet
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
            services.AddControllers();
            //add cors
            services.AddCors(options =>
            {
                options.AddPolicy("allowCors", policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            //add db context
            services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlite("Data Source=test.db");
            });

            //add services
            services.AddScoped<IProductRepo, ProductRepo>();
            services.AddScoped<IProductBrandRepo, ProductBrandRepoRepo>();
            services.AddScoped<IProductTypeRepo, ProductTypeRepoRepo>();
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseCors("allowCors");
            // app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
          
        }

   
    }
}
