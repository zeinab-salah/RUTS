using Microsoft.Data.SqlClient;
using RUTS.Data;
using RUTS.Models;
using RUTS.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RUTS.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(RUTSDesignTimeDbContextFactory db) : base(db)
        {
        }

    }
}
