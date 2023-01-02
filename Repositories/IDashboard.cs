using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveChartsCore;
using LiveChartsCore.Defaults;

namespace RUTS.Repositories
{
    public interface IDashboard
    {
        IEnumerable<string> GetStats();
        IEnumerable<string> GetLatestTrans();
        double[] GetLineData();
        ISeries[] GetPieChartData();
        ObservableCollection<DateTimePoint> GetBarChartData();
    }
}
