using Application.utils;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            // Register filter strategies
            services.AddScoped<IPropertyListingFilterStrategy, TypeFilterStrategy>();
            services.AddScoped<IPropertyListingFilterStrategy, PriceFilterStrategy>();
            services.AddScoped<IPropertyListingFilterStrategy, SquareFootageFilterStrategy>();
            services.AddScoped<IPropertyListingFilterStrategy, NumberOfBedroomsFilterStrategy>();
            services.AddScoped<IPropertyListingFilterStrategy, NumberOfBathroomsFilterStrategy>();
            services.AddScoped<IPropertyListingFilterStrategy, StatusFilterStrategy>();

            return services;
        }
   
    }
}
