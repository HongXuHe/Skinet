using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new
                        SymmetricSecurityKey
                        (Encoding.UTF8.GetBytes
                            (Configuration["Jwt:Key"]))
                };
                options.Events = new JwtBearerEvents {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException)) {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });
            services.AddAuthorization();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCustomException();
            app.UseStatusCodePagesWithReExecute("apr/errors/{0}");

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
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
