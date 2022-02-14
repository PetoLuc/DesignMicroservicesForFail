using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Producer_ProductApi
{
    public class HealthCheckAlwaysOK : IHealthCheck
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
            //custom check code here.....
            return Task.FromResult(new HealthCheckResult(HealthStatus.Healthy, "OK"));
        }
    }
}
