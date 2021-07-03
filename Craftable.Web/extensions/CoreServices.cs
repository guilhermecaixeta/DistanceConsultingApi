using Craftable.Core.interfaces.command;
using Craftable.Core.interfaces.queries;
using Craftable.Core.services;
using Craftable.Infrastructure.queries;
using Craftable.Infrastructure.repositories;
using Craftable.SharedKernel.interfaces.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Craftable.Web.extensions
{
    public static class CoreServices
    {
        /// <summary>
        /// Adds the core use cases.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection AddCoreUseCases(this IServiceCollection services)
        {
            services.AddScoped<IGetDistanceFromPostCodeUseCaseAsync, GetDistanceFromPostCodeInteractorAsync>();
            return services;
        }

        /// <summary>
        /// Adds the core repositories.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection AddCoreRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
            return services;
        }

        /// <summary>
        /// Adds the core handlers.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection AddCoreQueries(this IServiceCollection services)
        {
            services.AddScoped<AddressRangedQueryHandler>();
            services.AddScoped<IPostalcodeQueryHandler>(opt => opt.GetService<AddressRangedQueryHandler>());
            services.AddScoped<IPostalcodeListQueryHandler>(opt => opt.GetService<AddressRangedQueryHandler>());
            return services;
        }
    }
}