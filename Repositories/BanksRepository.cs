using RUTS.CustomModels;
using RUTS.Data;
using RUTS.Models;
using RUTS.Repositories.IRepositories;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace RUTS.Repositories
{
    public class BanksRepository : Repository<Bank>, IBanksRepository
    {
        public BanksRepository(RUTSDesignTimeDbContextFactory db) : base(db)
        {
        }
        public List<ComboData> GetComboData()
        {
            List<ComboData> list = _dbContext.Banks
                                    .Where(u => u.Deleted_at == null)
                                    .Select(u => new ComboData() { Id = u.Id, Value = u.Name }).ToList();
            return list;
        }
        public string GetBankName(int id)
        {
            return _dbContext.Banks
                      .Where(u => u.Deleted_at == null && u.Id == id)
                      .Select(u => u.Name)
                      .ToString();
        }
        public bool Update(Bank Bank)
        {
           var result = base._dbContext.Update(Bank);
           return result == null ? false : true;
        }
    }
}
