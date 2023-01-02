using RUTS.Data;
using RUTS.Models;
using RUTS.Repositories.IRepositories;
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

namespace RUTS.View.Correspondents;

public partial class Index : Window
{
    private readonly IUnitofwork _unitOfWOrk;
    private readonly RUTSDbContext _RUTSDbContext;
    private readonly RUTSDesignTimeDbContextFactory _RUTSDbContextFactory;
    public Index(RUTSDesignTimeDbContextFactory RUTSDbContextFactory, IUnitofwork unitofwork)
    {
        string[] args = { };
        _unitOfWOrk = unitofwork;
        _RUTSDbContextFactory = RUTSDbContextFactory;
        _RUTSDbContext = RUTSDbContextFactory.CreateDbContext(args);
        InitializeComponent();
        var window = this;
        window.IsVisibleChanged += (s, e) =>
        {
            if (window.IsVisible == false && window.IsLoaded)
            {
                CorrespondentsViewModel model = window.DataContext as CorrespondentsViewModel;
                Correspondent item            = model.SelectedItem;
                Window EditView               = new Create();
                RUTS.ViewModel.ViewModelBase.CurrentPage = null;
                SetViewModel(EditView, item, model);
                EditView.Show();
                window.Close();
            }
        };
    }

    public void SetViewModel(Window EditView, Correspondent item, CorrespondentsViewModel _vmodel)
    {
        if (_vmodel.EditWindow)
        {
            var comboBoxItem = new System.Windows.Controls.ComboBoxItem();
            comboBoxItem.Content = item.Country;
            CreateCorrespondentViewModel model = new CreateCorrespondentViewModel()
            {
                Id = item.Id,
                Name = item.Name,
                Country = comboBoxItem
            };
            EditView.DataContext = model;
        }
        else
        {
            EditView.DataContext = new CreateCorrespondentViewModel();
        }
    }
}
