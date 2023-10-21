using Microsoft.EntityFrameworkCore;
using Volo.Abp.DependencyInjection;

namespace OpenIddict.Demo.Data;

public class DemoEFCoreDbSchemaMigrator : ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public DemoEFCoreDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the DemoDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<DemoDbContext>()
            .Database
            .MigrateAsync();
    }
}
