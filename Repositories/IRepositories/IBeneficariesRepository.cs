using RUTS.CustomModels;
using RUTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RUTS.Repositories.IRepositories
{
    public interface IBeneficariesRepository : IRepository<Benificary>
    {
        List<ComboData> GetComboData();
        bool Update(Benificary Benificary);
        string GetBeneficaryName(int id);
    }
}
