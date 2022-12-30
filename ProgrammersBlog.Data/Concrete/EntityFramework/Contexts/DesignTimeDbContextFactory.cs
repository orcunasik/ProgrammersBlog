using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Data.Concrete.EntityFramework.Contexts
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ProgrammersBlogContext>
    {
        public ProgrammersBlogContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<ProgrammersBlogContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseSqlServer(Configuration.ConnectionString);
            return new(dbContextOptionsBuilder.Options);
        }
    }
}
