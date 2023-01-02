using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RUTS.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RUTS.Data
{
    public class RUTSDbContextFactory
    {
        private readonly string _connectionString = RepositoryBase.GetConnectionString();


        public RUTSDbContext CreateDbContext()
        {
            var Options = new DbContextOptionsBuilder<RUTSDbContext>().UseSqlServer(_connectionString).Options;

            return new RUTSDbContext(Options);
        }
    }
}
