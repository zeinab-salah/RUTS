using Caliburn.Micro;
using GalaSoft.MvvmLight.Command;
using Microsoft.Extensions.DependencyInjection;
using RUTS.Data;
using RUTS.Models;
using RUTS.Repositories;
using RUTS.Repositories.IRepositories;
using RUTS.View;
using RUTS.View.Currencies;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RUTS.ViewModel.Currencies
{
    public class CurrenciesViewModel : ViewModelBase
    {
        private int _CurrentPage = 1;
        private int _NumberOfPages = 10;
        private int _SelectedRecored = 10;
        public string _Name { get; set; }
        private bool _isViewVisible = true;
        private bool _DeleteItem = false;
        private bool _CreateWindow = false;
        private bool _EditWindow = false;
        private IUnitofwork _UnitOfWork;
        public bool _IsChecked { get; set; }
        public string _DollarExchangeRate { get; set; }
        public string _AEDExchangeRate { get; set; }
        private Currency _SelectedItem;
        public IEnumerable<Currency> _Data { get; set; }

        public List<Currency> _ListOfRecords;
        public ICurrencyRepository CurrencyRepository { get; set; }

        public CurrenciesViewModel()
        {
            _UnitOfWork = App.AppHost.Services.GetRequiredService<IUnitofwork>();
            CurrencyRepository = _UnitOfWork.Currency;
            IEnumerable<Currency> rows;
            if (Search != null)
                rows = CurrencyRepository.GetAll(u => u.Deleted_at == null && u.Name.ToLower().Contains(Search.ToLower()));
            else
                rows = CurrencyRepository.GetAll(u => u.Deleted_at == null);
            Data = rows;
            EditCurrency   = new ViewModelCommand(ExecuteEditCurrencyCommand);
            RemoveCurrency = new ViewModelCommand(ExecuteRemoveCurrencyCommand);
            CloseToaster   = new ViewModelCommand(ExecuteCloseToasterCommand);
            ConfirmDelete  = new ViewModelCommand(ExecuteConfirmDeleteCommand);
            CreateCurrency = new ViewModelCommand(ExecuteCreateCurrencyCommand);
            FirstCommand = new ViewModelCommand(ExecuteFirstCommand);
            PreviousCommand = new ViewModelCommand(ExecutePreviousCommand);
            NextCommand = new ViewModelCommand(ExecuteNextCommand);
            LastCommand = new ViewModelCommand(ExecuteLastCommand);
            FilterData = new ViewModelCommand(ExecuteFilterDataCommand);
            _ListOfRecords = new List<Currency>();
            UpdateCollections(Data.Take(SelectedRecord));
            UpdateRecordCount();
        }


        int RecordStartsFrom = 0;

        public List<Currency> ListOfRecords
        {
            get
            {
                return _ListOfRecords;
            }
            set
            {
                _ListOfRecords = value;
                OnPropertyChanged(nameof(ListOfRecords));
                UpdateEnableState();
            }
        }

        public int CurrentPage
        {
            get
            {
                return _CurrentPage;
            }
            set
            {
                _CurrentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
                UpdateEnableState();
            }
        }

        private void UpdateEnableState()
        {
            IsFirstEnable = CurrentPage > 1;
            IsPreviouseEnable = CurrentPage > 1;
            IsNextEnable = CurrentPage < NumberOfPages;
            IsLastEnable = CurrentPage < NumberOfPages;
        }

        public int NumberOfPages
        {
            get
            {
                return _NumberOfPages;
            }
            set
            {
                _NumberOfPages = value;
                OnPropertyChanged(nameof(NumberOfPages));
                UpdateEnableState();
            }
        }
        public int SelectedRecord
        {
            get
            {
                return _SelectedRecored;
            }
            set
            {
                _SelectedRecored = value;
                OnPropertyChanged(nameof(SelectedRecord));
                UpdateRecordCount();
            }
        }

        private void UpdateRecordCount()
        {
            int reminder = Data.Count() % SelectedRecord;
            int result = Data.Count() / SelectedRecord;
            NumberOfPages = reminder > 0 ? result + 1 : result;
            NumberOfPages = NumberOfPages == 0 ? 1 : NumberOfPages;
            UpdateCollections(Data.Take(SelectedRecord));
            CurrentPage = 1;
        }
        private void UpdateCollections(IEnumerable<Currency> enumerable)
        {
            List<Currency> listOfRecords2 = new List<Currency>();
            foreach (var item in enumerable)
            {
                listOfRecords2.Add(item);
            }
            ListOfRecords = listOfRecords2;
        }
        private bool _IsFirstEnable;

        public bool IsFirstEnable
        {
            get
            {
                return _IsFirstEnable;
            }
            set
            {
                _IsFirstEnable = value;
                OnPropertyChanged(nameof(IsFirstEnable));

            }
        }
        private bool _IsPreviouseEnable;

        public bool IsPreviouseEnable
        {
            get
            {
                return _IsPreviouseEnable;
            }
            set
            {
                _IsPreviouseEnable = value;
                OnPropertyChanged(nameof(IsPreviouseEnable));

            }
        }

        private bool _IsNextEnable;

        public bool IsNextEnable
        {
            get
            {
                return _IsNextEnable;
            }
            set
            {
                _IsNextEnable = value;
                OnPropertyChanged(nameof(IsNextEnable));

            }
        }
        private bool _IsLastEnable;

        public bool IsLastEnable
        {
            get
            {
                return _IsLastEnable;
            }
            set
            {
                _IsLastEnable = value;
                OnPropertyChanged(nameof(IsLastEnable));

            }
        }


        public bool CreateWindow
        {
            get
            {
                return _CreateWindow;
            }
            set
            {
                _CreateWindow = value;
                OnPropertyChanged(nameof(CreateWindow));
            }
        }
        public bool EditWindow
        {
            get
            {
                return _EditWindow;
            }
            set
            {
                _EditWindow = value;
                OnPropertyChanged(nameof(EditWindow));
            }
        }
        public bool IsViewVisible
        {
            get
            {
                return _isViewVisible;
            }
            set
            {
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }
        public bool DeleteItem
        {
            get
            {
                return _DeleteItem;
            }
            set
            {
                _DeleteItem = value;
                OnPropertyChanged(nameof(DeleteItem));
            }
        }
        public IEnumerable<Currency> Data
        {
            get
            {
                return _Data;
            }
            set
            {
                _Data = value;
                OnPropertyChanged(nameof(Data));
            }
        }
        public Currency SelectedItem
        {
            get
            {
                return _SelectedItem;
            }
            set
            {
                _SelectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string AEDExchangeRate
        {
            get
            {
                return _AEDExchangeRate;
            }
            set
            {
                _AEDExchangeRate = value;
                OnPropertyChanged(nameof(AEDExchangeRate));
            }
        }
        public string DollarExchangeRate
        {
            get
            {
                return _DollarExchangeRate;
            }
            set
            {
                _DollarExchangeRate = value;
                OnPropertyChanged(nameof(DollarExchangeRate));
            }
        }
        public bool IsChecked
        {
            get
            {
                return _IsChecked;
            }
            set
            {
                _IsChecked = value;
                OnPropertyChanged(nameof(IsChecked));
            }
        }
        public ICommand EditCurrency { get; }
        public ICommand RemoveCurrency { get; }
        public ICommand CreateCurrency { get; }
        public ICommand FirstCommand { get; }
        public ICommand PreviousCommand { get; }
        public ICommand NextCommand { get; }
        public ICommand LastCommand { get; }

        private void ExecuteLastCommand(object obj)
        {
            UpdateCollections(Data.Skip(Data.Count() - SelectedRecord));
            CurrentPage = NumberOfPages;
            UpdateEnableState();
        }

        private void ExecuteNextCommand(object obj)
        {
            RecordStartsFrom = CurrentPage * SelectedRecord;
            var RecordsToShow = Data.Skip(RecordStartsFrom).Take(SelectedRecord);
            UpdateCollections(RecordsToShow);
            CurrentPage++;
            UpdateEnableState();
        }

        private void ExecutePreviousCommand(object obj)
        {
            CurrentPage--;
            RecordStartsFrom = Data.Count() - SelectedRecord * (NumberOfPages - (CurrentPage - 1));
            var ReordsToShow = Data.Skip(RecordStartsFrom).Take(SelectedRecord);
            UpdateCollections(ReordsToShow);
            UpdateEnableState();
        }

        private void ExecuteFirstCommand(object obj)
        {
            UpdateCollections(Data.Take(SelectedRecord));
            CurrentPage = 1;
            UpdateEnableState();
        }


        private void ExecuteCreateCurrencyCommand(object obj)
        {
            CreateWindow = true;
            EditWindow = false;
            IsViewVisible = false;
        }
        private void ExecuteEditCurrencyCommand(object obj)
        {
            CreateWindow = false;
            EditWindow = true;
            IsViewVisible = false;
        }
        private void ExecuteRemoveCurrencyCommand(object obj)
        {
            if (null != SelectedItem)
            {
                Visibility = "Visible";
                FeedbackMessage = "Are you sure you want to delete this record?";
                //Data = Data.Where(u => u.Id != SelectedItem.Id).ToList();
            }
        }
        private void ExecuteConfirmDeleteCommand(object obj)
        {
            Visibility = "Hidden";
            SelectedItem.Deleted_at = DateTime.Now;
            CurrencyRepository.Remove(SelectedItem);
            _UnitOfWork.Save();
            ExecuteFilterDataCommand(obj);
        }
        private void ExecuteFilterDataCommand(object obj)
        {
            Data = CurrencyRepository.GetAll(u => u.Deleted_at == null && u.Name.ToLower().Contains(Search.ToLower()));
            UpdateCollections(Data.Take(SelectedRecord));
            UpdateRecordCount();
        }
    }
}
