using Microsoft.EntityFrameworkCore;
using RUTS.Data;
using RUTS.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace RUTS.Repositories
{
    public class Unitofwork : IUnitofwork
    {
        private RUTSDbContext _dbContext;

        public Unitofwork(RUTSDesignTimeDbContextFactory designFactory)
        {
            string[] args = { };
            _dbContext = designFactory.CreateDbContext(args);
            Account = new AccountRepository(designFactory);
            Beneficary = new BeneficariesRepository(designFactory);
            Transaction = new TransactionRepository(designFactory);
            User = new UserRepository(designFactory);
            Correspondent = new CorrespondentRepository(designFactory);
            Currency = new CurrencyRepository(designFactory);
            ResourceItem = new ResourceItemRepository(designFactory);
            Bank = new BanksRepository(designFactory);
            Dashboard = new Dashboard(designFactory);
        }

        public IAccountRepository Account { get; set; }
        public IBeneficariesRepository Beneficary {get; set; }
        public ITransactionRepository Transaction { get; set; }
        public IUserRepository User {get; set;}
        public ICorrespondentRepository Correspondent { get; set; }
        public ICurrencyRepository Currency { get; set; }
        public IResourceItemRepository ResourceItem { get; set; }
        public IBanksRepository Bank { get; set; }
        public IDashboard Dashboard { get; set; }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
