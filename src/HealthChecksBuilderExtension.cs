using Microsoft.Extensions.Diagnostics.HealthChecks;
using Ongdat.Diagnostics.HealthChecks.MySql;

namespace Microsoft.Extensions.DependencyInjection;

public static class HealthChecksBuilderExtension
{
  public static IHealthChecksBuilder AddMySqlCheck(
    this IHealthChecksBuilder builder,
    string connectionString,
    string name,
    HealthStatus? failureStatus = default,
    params string[] tags)
  {
    if (tags == null || tags.Length == 0)
    {
      tags = new string[] { "MySql" };
    }

    return builder.AddTypeActivatedCheck<MySqlHealthCheck>(name, failureStatus, tags: tags, connectionString);
  }
}