using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;

namespace EfCoreInterceptors.Interceptors;

public class LoggingPerformanceInterceptor : DbCommandInterceptor
{
    private readonly ILogger<LoggingPerformanceInterceptor> _logger;

    public LoggingPerformanceInterceptor(ILogger<LoggingPerformanceInterceptor> logger)
    {
        _logger = logger;
    }

    public override InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result)
    {
        _logger.LogInformation($"Command Type: {eventData.Command.CommandText}");
        return base.ReaderExecuting(command, eventData, result);
    }


    public override ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Command Type: {eventData.Command.CommandText}");
        return base.ReaderExecutingAsync(command, eventData, result, cancellationToken);
    }
}