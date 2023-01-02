using RUTS.CustomModels;
using RUTS.Data;
using RUTS.Models;
using RUTS.Repositories;
using RUTS.Repositories.IRepositories;
using RUTS.View.Currencies;
using RUTS.ViewModel.Accounts;
using RUTS.ViewModel.Correspondents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RUTS.View.Accounts;
public partial class Index : Window
{
    private readonly IUnitofwork _unitofwork;
    private readonly RUTSDbContext _RUTSDbContext;
    private readonly RUTSDesignTimeDbContextFactory _RUTSDbContextFactory;
    public Index(RUTSDesignTimeDbContextFactory RUTSDbContextFactory, IUnitofwork unitofwork)
    {
        string[] args         = { };
        _RUTSDbContext        = RUTSDbContextFactory.CreateDbContext(args);
        _RUTSDbContextFactory = RUTSDbContextFactory;
        _unitofwork           = unitofwork;
        InitializeComponent();
        var window = this;
        window.IsVisibleChanged += (s, e) =>
        {
            if (window.IsVisible == false && window.IsLoaded)
            {
                AccountsViewModel model = window.DataContext as AccountsViewModel;
                Account item            = model.SelectedItem;
                Window EditView         = new Create();
                RUTS.ViewModel.ViewModelBase.CurrentPage = null;
                SetViewModel(EditView, item, model);
                EditView.Show();
                window.Close();
            }
        };
    }
    public void SetViewModel(Window EditView, Account item, AccountsViewModel _vmodel)
    {
        if (_vmodel.EditWindow)
        {
            CreateAccountViewModel model = new CreateAccountViewModel()
            {
                Id = item.Id,
                AccountNumber = item.AccountNumber,
                Ballance = item.Ballance,
                SelectedCorrespondent = new ComboData()
                {
                    Id    = item.CorrespondentId,
                    Value = "ADIB"
                },
                SelectedCurrency = new ComboData()
                {
                    Id    = item.CurrencyId,
                    Value = "Dollar"
                }
            };
            model.SelectedCorrespondent.Value = model.CurrencyRepository.GetCurrencyName(item.CorrespondentId);
            EditView.DataContext = model;
        }
        else
        {
            EditView.DataContext = new CreateAccountViewModel();
        }
    }
}
