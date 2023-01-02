using Microsoft.Extensions.DependencyInjection;
using RUTS.Models;
using RUTS.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace RUTS.ViewModel.Banks
{
    public class BanksViewModel : ViewModelBase
    {
        //Fields
        private int _Id;
        private string _Name;
        private bool _CreateWindow = false;
        private bool _EditWindow = false;
        private bool _isViewVisible = true;
        private bool _GoToCreate = false;
        private bool _GoToEdit = false;
        private int _CurrentPage = 1;
        private int _NumberOfPages = 10;
        private int _SelectedRecored = 10;
        private Bank _SelectedItem;

        public IEnumerable<Bank> _Data { get; set; }

        public List<Bank> _ListOfRecords;

        //Constructor
        public BanksViewModel()
        {
            _UnitOfWork = App.AppHost.Services.GetRequiredService<IUnitofwork>();
            IEnumerable<Bank> rows;
            if (Search != null)
                rows = _UnitOfWork.Bank.GetAll(u => u.Deleted_at == null && (u.Name.ToLower().Contains(Search.ToLower())));
            else
                rows = _UnitOfWork.Bank.GetAll(u => u.Deleted_at == null);
            _Data = rows;
            EditBank = new ViewModelCommand(ExecuteEditBankCommand);
            CreateBank = new ViewModelCommand(ExecuteCreateBankCommand);
            RemoveBank = new ViewModelCommand(ExecuteRemoveBankCommand);
            CloseToaster = new ViewModelCommand(ExecuteCloseToasterCommand);
            ConfirmDelete = new ViewModelCommand(ExecuteConfirmDeleteCommand);
            FirstCommand = new ViewModelCommand(ExecuteFirstCommand);
            PreviousCommand = new ViewModelCommand(ExecutePreviousCommand);
            NextCommand = new ViewModelCommand(ExecuteNextCommand);
            LastCommand = new ViewModelCommand(ExecuteLastCommand);
            FilterData = new ViewModelCommand(ExecuteFilterDataCommand);
            _ListOfRecords = new List<Bank>();
            UpdateCollections(Data.Take(SelectedRecord));
            UpdateRecordCount();
        }

        int RecordStartsFrom = 0;

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

        public List<Bank> ListOfRecords
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
        private void UpdateCollections(IEnumerable<Bank> enumerable)
        {
            List<Bank> listOfRecords2 = new List<Bank>();
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

        public int Id
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
                OnPropertyChanged(nameof(Id));
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
        public IEnumerable<Bank> Data
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
        public bool GoToCreate
        {
            get
            {
                return _GoToCreate;
            }
            set
            {
                _GoToCreate = value;
                OnPropertyChanged(nameof(GoToCreate));
            }
        }
        public bool GoToEdit
        {
            get
            {
                return _GoToEdit;
            }
            set
            {
                _GoToEdit = value;
                OnPropertyChanged(nameof(GoToEdit));
            }
        }
        public Bank SelectedItem
        {
            get
            {
                return _SelectedItem;
            }
            set
            {
                _SelectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }
        private void ExecuteEditBankCommand(object obj)
        {
            CreateWindow = false;
            EditWindow = true;
            IsViewVisible = false;
        }
        private void ExecuteCreateBankCommand(object obj)
        {
            CreateWindow = true;
            EditWindow = false;
            IsViewVisible = false;
        }
        private void ExecuteRemoveBankCommand(object obj)
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
            _UnitOfWork.Bank.Remove(SelectedItem);
            _UnitOfWork.Save();
            ExecuteFilterDataCommand(obj);
        }

        private void ExecuteFilterDataCommand(object obj)
        {
            Data = _UnitOfWork.Bank.GetAll(u => u.Deleted_at == null && (u.Name.ToLower().Contains(Search.ToLower())));
            UpdateCollections(Data.Take(SelectedRecord));
            UpdateRecordCount();
        }

        //Commands
        public ICommand EditBank { get; }
        public ICommand CreateBank { get; }
        public ICommand RemoveBank { get; }
        public ICommand FirstCommand { get; }
        public ICommand PreviousCommand { get; }
        public ICommand NextCommand { get; }
        public ICommand LastCommand { get; }


    }
}
