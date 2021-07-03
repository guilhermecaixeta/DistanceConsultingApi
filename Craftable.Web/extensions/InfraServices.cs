using Craftable.Infrastructure.client.policies;
using Craftable.Infrastructure.client.postcode;
using Craftable.Infrastructure.client.postcode.api;
using Craftable.Infrastructure.data;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;

namespace Craftable.Web.extensions
{
    public static class InfraServices
    {
        public static IServiceCollection AddEFCoreDataProvider(this IServiceCollection services)
        {
            services.AddDbContext<CraftableContext>();
            return services;
        }

        public static IServiceCollection AddRefitDataProvider(this IServiceCollection services)
        {
            services
            .AddRefitClient<IPostCodeApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://api.postcodes.io"))
            .SetHandlerLifetime(TimeSpan.FromMinutes(1))
            .AddPolicyHandler(AccessPolicies.CreateRetryPolicy())
            .AddPolicyHandler(AccessPolicies.CreateCircuitBreakerPolicy());

            services.AddScoped<IPostCodeClient, PostCodeClient>();

            return services;
        }
    }
}
