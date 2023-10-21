using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace OpenIddict.Demo.Data;

public class DemoDbContextFactory : IDesignTimeDbContextFactory<DemoDbContext>
{
    public DemoDbContext CreateDbContext(string[] args)
    {

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<DemoDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new DemoDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
