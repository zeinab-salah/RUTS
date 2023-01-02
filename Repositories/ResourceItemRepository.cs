using RUTS.CustomModels;
using RUTS.Data;
using RUTS.Models;
using RUTS.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RUTS.Repositories
{
    public class ResourceItemRepository: Repository<ResourcesItem>, IResourceItemRepository
    {
        private readonly RUTSDbContext _RUTSDbContextFactory;

        public ResourceItemRepository(RUTSDesignTimeDbContextFactory RUTSDbContextFactory) : base(RUTSDbContextFactory)
        {
        }

        public List<ComboData> GetComboData()
        {
            List<ComboData> list = _dbContext.ResourceItem
                                    .Where(u => u.Deleted_at == null)
                                    .Select(u => new ComboData() { Id = u.Id, Value = u.Name }).ToList();
            return list;
        }

        public string GetResourceName(int id)
        {
            return _dbContext.ResourceItem
                      .Where(u => u.Deleted_at == null && u.Id == id)
                      .Select(u => u.Name)
                      .ToString();
        }

        public bool Update(ResourcesItem ResourceItem)
        {
            var result = base._dbContext.Update(ResourceItem);
            return result == null ? false : true;
        }
    }
}
