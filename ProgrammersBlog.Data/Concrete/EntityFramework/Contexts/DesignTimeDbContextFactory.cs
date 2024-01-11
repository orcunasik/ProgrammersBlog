using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ProgrammersBlog.Data.Concrete.EntityFramework.Contexts;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ProgrammersBlogContext>
{
    public ProgrammersBlogContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<ProgrammersBlogContext> dbContextOptionsBuilder = new();
        dbContextOptionsBuilder.UseSqlServer(Configuration.ConnectionString);
        return new(dbContextOptionsBuilder.Options);
    }
}