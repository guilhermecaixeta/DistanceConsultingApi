using Craftable.Core.entities.handlers;
using Craftable.Core.interfaces.CQRS.commands;
using Craftable.Core.interfaces.CQRS.queries;
using Craftable.Core.interfaces.Repository;
using Craftable.Core.interfaces.services;
using Craftable.Core.services;
using Craftable.Infrastructure.queries;
using Craftable.Infrastructure.repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Craftable.Web.extensions
{
    public static class CoreServices
    {
        /// <summary>
        /// Adds the core services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IPostcodeServiceAsync, PostcodeServiceAsync>();
            return services;
        }

        public static IServiceCollection AddCoreRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IPostCodeAddressRepository, PostCodeAddressRepositoryAsync>();
            return services;
        }

        public static IServiceCollection AddCoreHandlers(this IServiceCollection services)
        {
            services.AddSingleton<AddressRangedCommandHandler>();
            services.AddSingleton<AddressRangedQueryHandler>();
            services.AddSingleton<IPostalcodeQueryHandler>(opt => opt.GetService<AddressRangedQueryHandler>());
            services.AddSingleton<IPostalcodeListQueryHandler>(opt => opt.GetService<AddressRangedQueryHandler>());
            services.AddSingleton<IPostalcodeCommandHandler>(opt => opt.GetService<AddressRangedCommandHandler>());
            return services;
        }
    }
}