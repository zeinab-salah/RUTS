using RUTS.CustomModels;
using RUTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RUTS.Repositories.IRepositories
{
    public interface IResourceItemRepository : IRepository<ResourcesItem>
    {
        bool Update(ResourcesItem ResourceItem);
        List<ComboData> GetComboData();
        string GetResourceName(int id);
    }
}
