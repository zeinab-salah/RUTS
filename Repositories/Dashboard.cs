using LiveChartsCore;
using LiveChartsCore.Defaults;
using Microsoft.EntityFrameworkCore;
using RUTS.Data;
using RUTS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RUTS.Repositories
{
    public class Dashboard : IDashboard
    {
        protected readonly RUTSDbContext _dbContext;


        public Dashboard(RUTSDesignTimeDbContextFactory db)
        {
            string[] args = { };
            _dbContext = db.CreateDbContext(args);
            //this.TransactionsSet = _dbContext.Set<Transaction>();
        }

        public ObservableCollection<DateTimePoint> GetBarChartData()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetLatestTrans()
        {
            throw new NotImplementedException();
        }

        public double[] GetLineData()
        {
            throw new NotImplementedException();
        }

        public ISeries[] GetPieChartData()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetStats()
        {
            string PendingCredits   = _dbContext.Set<Transaction>().Where(u => u.Deleted_at == null && u.Type == "Credit" && u.StatementDate == null).Sum(u => Convert.ToInt32(u.Amount.Replace(",",""))).ToString();
            string PendingDebits    = _dbContext.Set<Transaction>().Where(u => u.Deleted_at == null && u.Type == "Debit" && u.StatementDate == null).Sum(u => Convert.ToInt32(u.Amount.Replace(",", ""))).ToString();
            string CompletedCredits = _dbContext.Set<Transaction>().Where(u => u.Deleted_at == null && u.Type == "Credit" && u.StatementDate != null).Sum(u => Convert.ToInt32(u.Amount.Replace(",", ""))).ToString();
            string CompletedDebits  = _dbContext.Set<Transaction>().Where(u => u.Deleted_at == null && u.Type == "Debit" && u.StatementDate != null).Sum(u => Convert.ToInt32(u.Amount.Replace(",", ""))).ToString();

            return new List<string>() { PendingCredits, PendingDebits, CompletedCredits, CompletedDebits };
        }
    }
}
