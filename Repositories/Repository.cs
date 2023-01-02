using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using Microsoft.Office.Interop.Excel;
using RUTS.Data;
using RUTS.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace RUTS.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly RUTSDbContext _dbContext;

        public DbSet<T> Set { get; set; }

        public Repository(RUTSDesignTimeDbContextFactory db)
        {
            string[] args = { };
            _dbContext = db.CreateDbContext(args);
            this.Set = _dbContext.Set<T>();
        }


        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProps = null)
        {
            IQueryable<T> query = Set;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProps != null)
            {
                foreach (var includeProp in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.ToList();
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            Set.RemoveRange(entities);
        }

        void IRepository<T>.Add(T entity)
        {
            _dbContext.Add(entity);
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProps = null)
        {
            IQueryable<T> query = Set;
            query = query.Where(filter);
            if (includeProps != null)
            {
                foreach (var includeProp in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.FirstOrDefault();
        }

        public void Remove(T entity)
        {
            _dbContext.Update(entity);
        }
    }
}