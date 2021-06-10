using Craftable.Core.aggregate.postcode.commands;
using Craftable.Core.aggregate.postcode.handlers;
using Craftable.Core.aggregate.postcode.queries;
using Craftable.Core.interfaces;
using Craftable.Core.interfaces.Repository;
using Craftable.Core.interfaces.services;
using Craftable.Core.services;
using Craftable.Infrastructure.queries;
using Craftable.Infrastructure.repositories;
using Craftable.SharedKernel.DTO;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

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
            services.AddScoped<IPostalCodeRepository, PostalCodeRepositoryAsync>();
            return services;
        }

        public static IServiceCollection AddCoreHandlers(this IServiceCollection services)
        {
            services.AddScoped<AddressRangedCommandHandler>();
            services.AddScoped<AddressRangedQueryHandler>();
            services.AddScoped<IRequestHandlerAsync<AddressesQuery, IQueryResult<IReadOnlyList<PostcodeDTO>>>>(opt => opt.GetService<AddressRangedQueryHandler>());
            services.AddScoped<IRequestHandlerAsync<PostalCodeQuery, IQueryResult<PostcodeAddressRangedDTO>>>(opt => opt.GetService<AddressRangedQueryHandler>());
            services.AddScoped<IRequestHandlerAsync<AddressRangedCommand, ICommandResult>>(opt => opt.GetService<AddressRangedCommandHandler>());
            return services;
        }
    }
}