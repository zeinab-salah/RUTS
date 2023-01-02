using RUTS.Models;
using RUTS.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using RUTS.Repositories.IRepositories;
using RUTS.Data;
using Microsoft.Extensions.DependencyInjection;

namespace RUTS.ViewModel.Correspondents
{
    public class CorrespondentsViewModel : ViewModelBase
    {
        private int _CurrentPage = 1;
        private int _NumberOfPages = 10;
        private int _SelectedRecored = 10; 
        public string _Name { get; set; }
        private bool _isViewVisible = true;
        private bool _DeleteItem = false;
        private bool _CreateWindow = false;
        private bool _EditWindow   = false;
        public string _Country { get; set; }
        private Correspondent _SelectedItem;
        public IEnumerable<Correspondent> _Data { get; set; }

        public List<Correspondent> _ListOfRecords;
        public ICorrespondentRepository CorrespondentRepository { get; set; }

        public CorrespondentsViewModel()
        {
            _UnitOfWork                     = App.AppHost.Services.GetRequiredService<IUnitofwork>();
            CorrespondentRepository         = _UnitOfWork.Correspondent;
            IEnumerable<Correspondent> rows;
            if (Search != null)
                rows = CorrespondentRepository.GetAll(u => u.Deleted_at == null && u.Name.ToLower().Contains(Search));
            else
                rows = CorrespondentRepository.GetAll(u => u.Deleted_at == null);
            _Data = rows;
            CreateCorrespondent = new ViewModelCommand(ExecuteCreateCorrespondentCommand);
            EditCorrespondent = new ViewModelCommand(ExecuteEditCorrespondentCommand);
            RemoveCorrespondent = new ViewModelCommand(ExecuteRemoveCorrespondentCommand);
            CloseToaster = new ViewModelCommand(ExecuteCloseToasterCommand);
            ConfirmDelete = new ViewModelCommand(ExecuteConfirmDeleteCommand);
            FirstCommand = new ViewModelCommand(ExecuteFirstCommand);
            PreviousCommand = new ViewModelCommand(ExecutePreviousCommand);
            NextCommand = new ViewModelCommand(ExecuteNextCommand);
            LastCommand = new ViewModelCommand(ExecuteLastCommand);
            FilterData = new ViewModelCommand(ExecuteFilterDataCommand);
            _ListOfRecords = new List<Correspondent>();
            UpdateCollections(Data.Take(SelectedRecord));
            UpdateRecordCount();
        }
        int RecordStartsFrom = 0;

        public List<Correspondent> ListOfRecords
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
        private void UpdateCollections(IEnumerable<Correspondent> enumerable)
        {
            List<Correspondent> listOfRecords2 = new List<Correspondent>();
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
        public IEnumerable<Correspondent> Data
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
        public Correspondent SelectedItem
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
        public string Country
        {
            get
            {
                return _Country;
            }
            set
            {
                _Country = value;
                OnPropertyChanged(nameof(Country));
            }
        }
        public ICommand EditCorrespondent { get; }
        public ICommand CreateCorrespondent { get; }
        public ICommand RemoveCorrespondent { get; }
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

        private void ExecuteEditCorrespondentCommand(object obj)
        {
            CreateWindow  = false;
            EditWindow    = true;
            IsViewVisible = false;
        }
        private void ExecuteCreateCorrespondentCommand(object obj)
        {
            CreateWindow  = true;
            EditWindow    = false;
            IsViewVisible = false;
        }
        private void ExecuteRemoveCorrespondentCommand(object obj)
        {
            if (null != SelectedItem)
            {
                Visibility = "Visible";
                FeedbackMessage = "Are you sure you want to delete this record?";
            }
        }
        private void ExecuteConfirmDeleteCommand(object obj)
        {
            Visibility = "Hidden";
            SelectedItem.Deleted_at = DateTime.Now;
            CorrespondentRepository.Remove(SelectedItem);
            _UnitOfWork.Save();
            ExecuteFilterDataCommand(obj);
        }
        private void ExecuteFilterDataCommand(object obj)
        {
            Data = CorrespondentRepository.GetAll(u => u.Deleted_at == null && u.Name.ToLower().Contains(Search.ToLower())); ;
            UpdateCollections(Data.Take(SelectedRecord));
            UpdateRecordCount();
        }
    }
}
