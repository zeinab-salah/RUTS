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
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(RUTSDesignTimeDbContextFactory db) : base(db)
        {
        }

        public bool Update(Transaction transaction)
        {
            var result = base._dbContext.Update(transaction);
            return result == null ? false : true;
        }
    }
}
