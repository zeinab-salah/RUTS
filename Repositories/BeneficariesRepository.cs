using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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

namespace RUTS.Repositories
{
    public class BeneficariesRepository : Repository<Benificary>, IBeneficariesRepository
    {
        public BeneficariesRepository(RUTSDesignTimeDbContextFactory db) : base(db)
        {
        }
        public List<ComboData> GetComboData()
        {
            List<ComboData> list = _dbContext.Beneficaries
                                    .Where(u => u.Deleted_at == null)
                                    .Select(u => new ComboData() { Id = u.Id, Value = u.Name }).ToList();
            return list;
        }
        public string GetBeneficaryName(int id)
        {
            return _dbContext.Beneficaries
                      .Where(u => u.Deleted_at == null && u.Id == id)
                      .Select(u => u.Name)
                      .ToString();
        }
        public bool Update(Benificary Benificary)
        {
           var result = base._dbContext.Update(Benificary);
           return result == null ? false : true;
        }
    }
}
