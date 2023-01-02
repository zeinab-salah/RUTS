using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RUTS.Repositories.IRepositories
{
 
        public interface IUnitofwork
        {
            IAccountRepository Account { get; set; }
            IBeneficariesRepository Beneficary { get; set; }
            ITransactionRepository Transaction { get; set; }
            IUserRepository User { get; set; }
            ICorrespondentRepository Correspondent { get; set; }
            ICurrencyRepository Currency { get; set; }
            IResourceItemRepository ResourceItem { get; set; }
            IBanksRepository Bank { get; set; }
            IDashboard       Dashboard { get; set; }
            

        void Save();
        }
    
}
