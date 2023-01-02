﻿using RUTS.CustomModels;
using RUTS.Data;
using RUTS.Models;
using RUTS.Repositories;
using RUTS.Repositories.IRepositories;
using RUTS.ViewModel.Accounts;
using RUTS.ViewModel.Banks;
using RUTS.ViewModel.Benificaries;
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

namespace RUTS.View.Banks;

public partial class Index : Window
{
    private readonly IUnitofwork _unitOfWork;
    private readonly RUTSDbContext _DbContext;
    private readonly RUTSDesignTimeDbContextFactory _DbContextFactory;
    public Index(RUTSDesignTimeDbContextFactory DbContextFactory, IUnitofwork unitofwork)
    {
        string[] args = { };
        _unitOfWork = unitofwork;
        _DbContextFactory = DbContextFactory;
        _DbContext = DbContextFactory.CreateDbContext(args);
        InitializeComponent();
        var window = this;
        window.IsVisibleChanged += (s, e) =>
        {
            if (window.IsVisible == false && window.IsLoaded)
            {
                var model = window.DataContext as BanksViewModel;
                var item = model.SelectedItem;
                Window EditView = new Create();
                SetViewModel(EditView, item, model);
                RUTS.ViewModel.ViewModelBase.CurrentPage = null;
                EditView.Show();
                window.Close();
            }
        };
    }
    public void SetViewModel(Window EditView, Bank item, BanksViewModel _vmodel)
    {
        if (_vmodel.EditWindow)
        {
            CreateBankViewModel model = new CreateBankViewModel(_DbContextFactory)
            {
                Id = item.Id,
                Name = item.Name,
            };
            EditView.DataContext = model;
        }
        else
        {
            EditView.DataContext = new CreateBankViewModel(_DbContextFactory);
        }
    }
}
