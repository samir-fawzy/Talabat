using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.APIs.Helper;
using TalabatProject.Core;
using TalabatProject.Core.Interfaces;
using TalabatProject.Core.Repositories;
using TalabatProject.Core.Services;
using TalabatProject.Repository;
using TalabatProject.Repository.Basket;
using TalabatProject.Service;

namespace Talabat.APIs.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddScoped<IUnitOfWork,UnitOfWork>();

            Services.AddScoped<IOrderService, OrderService>();

            Services.AddAutoMapper(typeof(MappingProfile));

            // change the default Response for invalid model state
            Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState.Where(P => P.Value.Errors.Count > 0)
                                        .SelectMany(P => P.Value.Errors)
                                        .Select(E => E.ErrorMessage)
                                        .ToArray();
                    var ValidationErrorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(ValidationErrorResponse);
                };
            });

            Services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));

            return Services;
        }
    }
}
