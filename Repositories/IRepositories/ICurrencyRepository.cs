using RUTS.CustomModels;
using RUTS.Models;
using RUTS.ViewModel.Currencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RUTS.Repositories.IRepositories
{
    public interface ICurrencyRepository : IRepository<Currency>
    {
        bool Update(Currency currency);
        List<ComboData> GetComboData();
        List<ComboData> GetComboDataByCorrespondent(int Id);
        string GetCurrencyName(int id);
    }
}
