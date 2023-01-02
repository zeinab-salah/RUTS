using Microsoft.Data.SqlClient;
using RUTS.Data;
using RUTS.Models;
using RUTS.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RUTS.Repositories
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        private readonly RUTSDbContext _RUTSDbContextFactory;

        public AccountRepository(RUTSDesignTimeDbContextFactory RUTSDbContextFactory) : base(RUTSDbContextFactory)
        {
            string[] args = { };
            _RUTSDbContextFactory = RUTSDbContextFactory.CreateDbContext(args);
        }
        public bool Update(Account Account)
        {
            var result = base._dbContext.Update(Account);
            return result == null ? false : true;
        }
    }
}
