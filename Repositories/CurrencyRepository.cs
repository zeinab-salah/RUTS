using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
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
    public class CurrencyRepository : Repository<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(RUTSDesignTimeDbContextFactory db) : base(db)
        {
        }

        public List<ComboData> GetComboData()
        {
            List<ComboData> list = _dbContext.Currencies
                                    .Where(u => u.Deleted_at == null)
                                    .Select(u => new ComboData() { Id = u.Id, Value = u.Name }).ToList();
            return list;
        }

        public List<ComboData> GetComboDataByCorrespondent(int Id)
        {
            var accounts = _dbContext.Accounts;
            var currencies = _dbContext.Currencies;

            List<ComboData> list = (from e in accounts
                               join o in currencies on e.CurrencyId equals o.Id
                               where e.CorrespondentId == Id
                               select new ComboData()
                               {
                                   Value = o.Name,
                                   Id = o.Id
                               }).ToList();

            return list;
        }

        public string GetCurrencyName(int id)
        {
            return _dbContext.Currencies
                      .Where(u => u.Deleted_at == null && u.Id == id)
                      .Select(u => u.Name)
                      .ToString();
        }

        public bool Update(Currency currency)
        {
            var result = base._dbContext.Update(currency);
            return result == null ? false : true;
        }
    }
}
