using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using RUTS.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RUTS.Data
{
    public class RUTSDesignTimeDbContextFactory : IDesignTimeDbContextFactory<RUTSDbContext>
    {
        private RUTSDbContext _context = null;

        public RUTSDbContext CreateDbContext(string[] args)
        {
            DbContextOptions Options = new DbContextOptionsBuilder<RUTSDbContext>().UseSqlServer(RepositoryBase.GetConnectionString()).Options;

            if(_context == null)
            {
                _context = new RUTSDbContext(Options);
            }

            return _context;
        }
    }
}
