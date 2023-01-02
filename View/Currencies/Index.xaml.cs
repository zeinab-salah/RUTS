using RUTS.Data;
using RUTS.Models;
using RUTS.ViewModel.Correspondents;
using RUTS.ViewModel.Currencies;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Resources;
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

namespace RUTS.View.Currencies
{
    public partial class Index : Window
    {
        private CurrenciesViewModel Currencies;
        private readonly RUTSDesignTimeDbContextFactory _dbcontext;
        public Index(RUTSDesignTimeDbContextFactory dbcontext)
        {
            _dbcontext = dbcontext;
            InitializeComponent();
            var window = this;
            window.IsVisibleChanged += (s, e) =>
            {
                if (window.IsVisible == false && window.IsLoaded)
                {
                    CurrenciesViewModel model = window.DataContext as CurrenciesViewModel;
                    Currency item  = model.SelectedItem;
                    Window EditView = new Create();
                    SetViewModel(EditView, item, model);
                    RUTS.ViewModel.ViewModelBase.CurrentPage = null;
                    EditView.Show();
                    window.Close();
                }
            };
        }
        public void SetViewModel(Window EditView, Currency item, CurrenciesViewModel _vmodel)
        {
            if (_vmodel.EditWindow)
            {
                EditView.DataContext = new CreateViewModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    DollarExchangeRate = item.DollarExchangeRate,
                };
            }
            else
            {
                EditView.DataContext = new CreateViewModel();
            }
        }
    }
}
