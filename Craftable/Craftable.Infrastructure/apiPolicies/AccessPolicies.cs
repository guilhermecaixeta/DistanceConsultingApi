using Polly;
using Polly.CircuitBreaker;
using Polly.Extensions.Http;
using Polly.Retry;
using Polly.Timeout;
using System;
using System.Net.Http;

namespace Craftable.Infrastructure.apiPolicies
{
    public static class AccessPolicies
    {
        public static AsyncRetryPolicy<HttpResponseMessage> CreateRetryPolicy() =>
            HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .Or<TimeoutRejectedException>() // Thrown by Polly's TimeoutPolicy if the inner call gets timeout.
                    .WaitAndRetryAsync(1, _ => TimeSpan.FromMilliseconds(1));

        public static AsyncCircuitBreakerPolicy<HttpResponseMessage> CreateCircuitBreakerPolicy() =>
        HttpPolicyExtensions
              .HandleTransientHttpError()
              .Or<BrokenCircuitException>()
              .CircuitBreakerAsync(3, TimeSpan.FromMinutes(1));

    }
}
