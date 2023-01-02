using Microsoft.Data.SqlClient;
using RUTS.CustomModels;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace RUTS.Repositories
{
    public class CorrespondentRepository : Repository<Correspondent>, ICorrespondentRepository
    {
        public CorrespondentRepository(RUTSDesignTimeDbContextFactory db) : base(db)
        {
        }

        public List<ComboData> GetComboData()
        {
            List<ComboData> list = _dbContext.Correspondents
                                    .Where(u => u.Deleted_at == null)
                                    .Select(u => new ComboData() { Id = u.Id, Value = u.Name }).ToList();
            return list;
        }

        public string GetCorrespondentName(int id)
        {
           return _dbContext.Correspondents
                     .Where(u => u.Deleted_at == null && u.Id == id)
                     .Select(u => u.Name)
                     .ToString();
        }

        public bool Update(Correspondent correspondent)
        {
            var result = base._dbContext.Update(correspondent);
            return result == null ? false : true;
        }
    }
}
