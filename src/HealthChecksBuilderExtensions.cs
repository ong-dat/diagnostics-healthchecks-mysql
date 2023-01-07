using Microsoft.Extensions.Diagnostics.HealthChecks;
using Ongdat.Diagnostics.HealthChecks.MySql;

namespace Microsoft.Extensions.DependencyInjection;

public static class HealthChecksBuilderExtensions
{
  public static IHealthChecksBuilder AddMySqlCheck(
    this IHealthChecksBuilder builder,
    string name,
    string connectionString,
    params string[] tags) => builder.AddMySqlCheck(name, connectionString, null, tags);

  public static IHealthChecksBuilder AddMySqlCheck(
    this IHealthChecksBuilder builder,
    string name,
    string connectionString,
    HealthStatus? failureStatus,
    params string[] tags)
  {
    if (tags == null || tags.Length == 0)
    {
      tags = new string[] { "MySql" };
    }

    return builder.AddTypeActivatedCheck<MySqlHealthCheck>(name, failureStatus, tags: tags, connectionString);
  }
}