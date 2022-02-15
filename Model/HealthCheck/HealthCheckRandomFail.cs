using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Common.HealthCheck
{
    public class HealthCheckRandomFail : IHealthCheck
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var randomNumber = new Random().Next(1, 100);
            if (randomNumber >= 50)
                return Task.FromResult(new HealthCheckResult(HealthStatus.Healthy, randomNumber.ToString()));
            else if (randomNumber < 50 && randomNumber > 30)
                return Task.FromResult(new HealthCheckResult(HealthStatus.Degraded, randomNumber.ToString()));
            else
                return Task.FromResult(new HealthCheckResult(HealthStatus.Unhealthy, randomNumber.ToString()));
        }
    }
}
