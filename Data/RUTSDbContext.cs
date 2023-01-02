using Microsoft.EntityFrameworkCore;
using RUTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RUTS.Data
{
    public class RUTSDbContext : DbContext
    {
        public RUTSDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Benificary> Beneficaries { get; set; }
        public DbSet<Correspondent> Correspondents { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<ResourcesItem> ResourceItem { get; set; }
        public DbSet<Bank> Banks { get; set; }
    }
}
