using RUTS.CustomModels;
using RUTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RUTS.Repositories.IRepositories
{
    public interface ICorrespondentRepository : IRepository<Correspondent>
    {
        bool Update(Correspondent correspondent);
        List<ComboData> GetComboData();
        string GetCorrespondentName(int id);
    }
}
