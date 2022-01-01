using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Skinet.API.Extensions;
using Skinet.Infrastructure.Data;
using StackExchange.Redis;

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
            services.AddCors(options => {
                options.AddPolicy("allowCors", policy => {
                    policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            //add db context
            services.AddDbContext<StoreContext>(options => {
                options.UseSqlite("Data Source=test.db");
            });

            services.AddApplicationService();
            services.AddSwaggerDocumentation();
            services.AddSingleton<IConnectionMultiplexer>(p => {
                var options = new ConfigurationOptions()
                {
                    
                };
                var configuration = ConfigurationOptions.Parse(Configuration.GetConnectionString("redis"), true);
                return ConnectionMultiplexer.Connect($"192.168.1.128:6379,resolvedns=1,abortConnect=False,password=123456");
            });
            //  services.AddStackExchangeRedisCache
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCustomException();
            app.UseStatusCodePagesWithReExecute("apr/errors/{0}");

            app.UseRouting();
            app.UseStaticFiles();
            app.UseCors("allowCors");
            // app.UseAuthorization();
            app.UseSwaggerDocumentation();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });

        }


    }
}
