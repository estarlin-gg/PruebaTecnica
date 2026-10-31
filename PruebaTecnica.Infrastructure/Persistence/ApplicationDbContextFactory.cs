using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PruebaTecnica.Infrastructure.Context;

namespace PruebaTecnica.Infrastructure.Persistence;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlite("Data Source=pruebatecnica.db");

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
