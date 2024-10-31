using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace EfCoreInterceptors.Interceptors;

public class LoggingSaveChangesInterceptor : SaveChangesInterceptor
{
    private readonly ILogger<LoggingSaveChangesInterceptor> _logger;

    public LoggingSaveChangesInterceptor(ILogger<LoggingSaveChangesInterceptor> logger)
    {
        _logger = logger;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        _logger.LogInformation($"Saving changes for {eventData.Context.Database.GetDbConnection().Database}");
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Saving changes for {eventData.Context.Database.GetDbConnection().Database}");
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        _logger.LogInformation($"Saved changes for {eventData.Context.Database.GetDbConnection().Database}");
        return base.SavedChanges(eventData, result);
    }

    public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Saved changes for {eventData.Context.Database.GetDbConnection().Database}");
        return base.SavedChangesAsync(eventData, result, cancellationToken);
    }

    public override void SaveChangesFailed(DbContextErrorEventData eventData)
    {
        _logger.LogError($"Save changes failed: {eventData.Exception.Message}");
        base.SaveChangesFailed(eventData);
    }
}
