using EfCoreInterceptors.Interceptors;
using EfCoreInterceptors.Models;
using Microsoft.EntityFrameworkCore;

namespace EfCoreInterceptors.Context;

public class ApplicationDbContext : DbContext
{
    private readonly ILoggerFactory _loggerFactory;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ILoggerFactory loggerFactory)
        : base(options)
    {
        _loggerFactory = loggerFactory;
    }

    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .AddInterceptors(new LoggingPerformanceInterceptor(_loggerFactory.CreateLogger<LoggingPerformanceInterceptor>()))
            .AddInterceptors(new LoggingConnectionInterceptor(_loggerFactory.CreateLogger<LoggingConnectionInterceptor>()))
            .AddInterceptors(new LoggingTransactionInterceptor(_loggerFactory.CreateLogger<LoggingTransactionInterceptor>()))
            .AddInterceptors(new LoggingSaveChangesInterceptor(_loggerFactory.CreateLogger<LoggingSaveChangesInterceptor>()));
    }
}
