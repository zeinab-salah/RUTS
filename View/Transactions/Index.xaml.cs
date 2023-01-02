using GalaSoft.MvvmLight;
using RUTS.CustomModels;
using RUTS.Data;
using RUTS.Models;
using RUTS.Repositories.IRepositories;
using RUTS.ViewModel.Accounts;
using RUTS.ViewModel.Transactions;
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

namespace RUTS.View.Transactions
{
    /// <summary>
    /// Interaction logic for Index.xaml
    /// </summary>
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
                    TransactionsViewModel model = window.DataContext as TransactionsViewModel;
                    Transaction item = model.SelectedItem;
                    if (item == null && model.CreateWindow == false) 
                        return;
                    Window EditView = new Create();
                    SetViewModel(EditView, item, model);
                    RUTS.ViewModel.ViewModelBase.CurrentPage = null;
                    EditView.Show();
                    try
                    {
                        window.Close();
                    }
                    catch (Exception ex) { }
                }
            };
        }
        public void SetViewModel(Window EditView, Transaction item, TransactionsViewModel _vmodel)
        {
            if (_vmodel.EditWindow)
            {
                CreateTransactionViewModel model = new CreateTransactionViewModel()
                {
                    Transaction = new Transaction()
                    {
                        Id = item.Id,
                        Amount = item.Amount,
                        Narration = item.Narration
                    }
                };
                EditView.DataContext = model;
            }
            else
            {
                EditView.DataContext = new CreateTransactionViewModel()
                {
                    Transaction = new Transaction()
                    {
                        Id = 0
                    }
                };
            }
        }
    }
}
