using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;

namespace EfCoreInterceptors.Interceptors;

public class LoggingConnectionInterceptor : DbConnectionInterceptor
{
    private readonly ILogger<LoggingConnectionInterceptor> _logger;

    public LoggingConnectionInterceptor(ILogger<LoggingConnectionInterceptor> logger)
    {
        _logger = logger;
    }

    public override void ConnectionOpened(DbConnection connection, ConnectionEndEventData eventData)
    {
        _logger.LogInformation($"Database connection opened:: {connection.Database}");
        base.ConnectionOpened(connection, eventData);
    }

    public override void ConnectionClosed(DbConnection connection, ConnectionEndEventData eventData)
    {
        _logger.LogInformation($"Database connection closed:: {connection.Database}");
        base.ConnectionClosed(connection, eventData);
    }

    public override void ConnectionFailed(DbConnection connection, ConnectionErrorEventData eventData)
    {
        _logger.LogError($"Database connection error: {eventData.Exception.Message}");
        base.ConnectionFailed(connection, eventData);
    }
}
