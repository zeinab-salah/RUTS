﻿using RUTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RUTS.Repositories.IRepositories
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        bool Update(Transaction transaction);
    }
}
