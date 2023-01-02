using LiveChartsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.Defaults;
using System.Collections.ObjectModel;
using RUTS.Repositories;
using Microsoft.Extensions.DependencyInjection;
using RUTS.Repositories.IRepositories;

namespace RUTS.ViewModel;

public class DashboardViewModel : ViewModelBase
{
    //Fields 
    private string _CorrespondentAccounts;
    private string _Beneficaries;
    private string _PendingCredits;
    private string _PendingDebits;
    private string _CompletedCredits;
    private string _CompletedDebits;
    private IDashboard DashboardRepository;
    private IUnitofwork _UnitOfWork;

    //setters and getters
    public string CorrespondentAccounts{
        get
        {
            return _CorrespondentAccounts;
        }
        set
        {
            _CorrespondentAccounts = CorrespondentAccounts;
            OnPropertyChanged(nameof(CorrespondentAccounts));
        }
    }
    public string Beneficaries{
        get
        {
            return _Beneficaries;
        }
        set
        {
            _Beneficaries = value;
            OnPropertyChanged(nameof(Beneficaries));
        }
    }
    public string PendingCredits{
        get
        {
            return _PendingCredits;
        }
        set
        {
            _PendingCredits = value;
            OnPropertyChanged(nameof(PendingCredits));
        }
    }
    public string PendingDebits{
        get
        {
            return _PendingDebits;
        }
        set
        {
            _PendingDebits = value;
            OnPropertyChanged(nameof(PendingDebits));
        }
    }
    public string CompletedCredits
    {
        get
        {
            return _CompletedCredits;
        }
        set
        {
            _CompletedCredits = value;
            OnPropertyChanged(nameof(CompletedCredits));
        }
    }
    public string CompletedDebits
    {
        get
        {
            return _CompletedDebits;
        }
        set
        {
            _CompletedDebits = value;
            OnPropertyChanged(nameof(CompletedDebits));
        }
    }
    //-> Commands
    public ICommand GoToCommand { get; }
    public DashboardViewModel()
    {
        _UnitOfWork         = App.AppHost.Services.GetRequiredService<IUnitofwork>();
        DashboardRepository = _UnitOfWork.Dashboard;
        var Stats           = DashboardRepository.GetStats();
        PendingCredits      = Stats.ElementAt(0);
        PendingDebits       = Stats.ElementAt(1);
        CompletedCredits    = Stats.ElementAt(2);
        CompletedDebits     = Stats.ElementAt(3);
        GoToCommand         = new ViewModelCommand(ExecuteGoToCommand);
    }
    //charts
    public ISeries[] Series { get; set; }
        = new ISeries[]
        {
            new LineSeries<double>
            {
                Values = new double[] { 2, 1, 3, 5, 3, 4, 6 },
                Fill = null
            }
        };

    public ISeries[] PieChartData { get; set; }
        = new ISeries[]
        {
            new PieSeries<double> { Values = new double[] { 2 } , Name = "any name"},
            new PieSeries<double> { Values = new double[] { 4 } },
            new PieSeries<double> { Values = new double[] { 1 } },
            new PieSeries<double> { Values = new double[] { 4 } },
            new PieSeries<double> { Values = new double[] { 3 } }
        };
    public ISeries[] BarChartData { get; set; } =
    {
        new ColumnSeries<DateTimePoint>
        {
            TooltipLabelFormatter = (chartPoint) =>
                $"{new DateTime((long) chartPoint.SecondaryValue):MMMM dd}: {chartPoint.PrimaryValue}",
            Values = new ObservableCollection<DateTimePoint>
            {
                new DateTimePoint(new DateTime(2021, 1, 1), 3),
                // notice we are missing the day new DateTime(2021, 1, 2)
                new DateTimePoint(new DateTime(2021, 1, 3), 6),
                new DateTimePoint(new DateTime(2021, 1, 4), 5),
                new DateTimePoint(new DateTime(2021, 1, 5), 3),
                new DateTimePoint(new DateTime(2021, 1, 6), 5),
                new DateTimePoint(new DateTime(2021, 1, 7), 8),
                new DateTimePoint(new DateTime(2021, 1, 8), 6)
            }
        }
    };
    public Axis[] XAxes { get; set; } =
    {
        new Axis
        {
            Labeler = value => new DateTime((long) value).ToString("MMMM dd"),
            LabelsRotation = 80,

            // when using a date time type, let the library know your unit 
            UnitWidth = TimeSpan.FromDays(1).Ticks, 

            // if the difference between our points is in hours then we would:
            // UnitWidth = TimeSpan.FromHours(1).Ticks,

            // since all the months and years have a different number of days
            // we can use the average, it would not cause any visible error in the user interface
            // Months: TimeSpan.FromDays(30.4375).Ticks
            // Years: TimeSpan.FromDays(365.25).Ticks

            // The MinStep property forces the separator to be greater than 1 day.
            MinStep = TimeSpan.FromDays(1).Ticks
        }
    };
    //charts
    public void ExecuteGoToCommand(Object obj)
    {
        var any = obj;
    }
}
