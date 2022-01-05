using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Skinet.API.Errors;
using Skinet.API.MapperProfiles;
using Skinet.API.MapperResolver;
using Skinet.Core.Interfaces;
using Skinet.Infrastructure.Data;

namespace Skinet.API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            //add services
            services.AddScoped<IProductRepo, ProductRepo>();
            services.AddScoped<IProductBrandRepo, ProductBrandRepoRepo>();
            services.AddScoped<IProductTypeRepo, ProductTypeRepoRepo>();
            services.AddSingleton<IBasketRepo, BasketRepo>();
            services.AddScoped<IUserRepo, UserRepo>();
            //add automapper
            services.AddAutoMapper(config => {
                config.AddProfile<ProductMapperProfile>();
            });
            services.AddTransient<ProductMapperResolver>();
            services.Configure<ApiBehaviorOptions>(options => {
                options.InvalidModelStateResponseFactory = actionContext => {
                    var errors = actionContext.ModelState.Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage).ToArray();
                    var errorResponse = new ApiValidationErrorResponse() {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(errorResponse);
                };
            });
            return services;
        }
    }
}
