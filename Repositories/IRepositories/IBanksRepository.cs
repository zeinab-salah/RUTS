using RUTS.CustomModels;
using RUTS.Models;
using System.Collections.Generic;

namespace RUTS.Repositories.IRepositories
{
    public interface IBanksRepository : IRepository<Bank>
    {
        List<ComboData> GetComboData();
        bool Update(Bank Bank);
        string GetBankName(int id);
    }
}
