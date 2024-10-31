using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;
using Microsoft.Extensions.Logging;

namespace EfCoreInterceptors.Interceptors;

public class LoggingTransactionInterceptor : DbTransactionInterceptor
{
    private readonly ILogger<LoggingTransactionInterceptor> _logger;

    public LoggingTransactionInterceptor(ILogger<LoggingTransactionInterceptor> logger)
    {
        _logger = logger;
    }

    public override void TransactionCommitted(DbTransaction transaction, TransactionEndEventData eventData)
    {
        if (transaction?.Connection?.Database != null)
        {
            _logger.LogInformation($"Transaction committed:: {transaction.Connection.Database}");
        }
        else
        {
            _logger.LogWarning("Transaction committed, but connection or database is null.");
        }

        base.TransactionCommitted(transaction, eventData);
    }

    public override void TransactionRolledBack(DbTransaction transaction, TransactionEndEventData eventData)
    {
        if (transaction?.Connection?.Database != null)
        {
            _logger.LogInformation($"Transaction rolled back:: {transaction.Connection.Database}");
        }
        else
        {
            _logger.LogWarning("Transaction rolled back, but connection or database is null.");
        }

        base.TransactionRolledBack(transaction, eventData);
    }

    public override void TransactionFailed(DbTransaction transaction, TransactionErrorEventData eventData)
    {
        if (eventData?.Exception != null)
        {
            _logger.LogError($"Transaction failed: {eventData.Exception.Message}");
        }
        else
        {
            _logger.LogError("Transaction failed, but no exception details are available.");
        }

        base.TransactionFailed(transaction, eventData);
    }
}
