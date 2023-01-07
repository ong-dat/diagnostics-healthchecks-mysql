using Microsoft.Extensions.Diagnostics.HealthChecks;
using MySqlConnector;

namespace Ongdat.Diagnostics.HealthChecks.MySql;

public class MySqlHealthCheck : IHealthCheck
{
  private readonly string connectionString;

  public MySqlHealthCheck(string connectionString)
  {
    if (string.IsNullOrEmpty(connectionString))
    {
      throw new ArgumentException($"'{nameof(connectionString)}' cannot be null or empty.", nameof(connectionString));
    }

    this.connectionString = connectionString;
  }

  public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
  {
    try
    {
      using var connection = new MySqlConnection(connectionString);
      await connection.OpenAsync(cancellationToken).ConfigureAwait(false);

      return await connection.PingAsync(cancellationToken).ConfigureAwait(false)
        ? HealthCheckResult.Healthy()
        : new HealthCheckResult(context.Registration.FailureStatus);
    }
    catch (Exception exception)
    {
      return new HealthCheckResult(context.Registration.FailureStatus, exception: exception);
    }
  }
}