using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using System.Windows.Forms;
using RUTS.Models;
using RUTS.Repositories;
using RUTS.Repositories.IRepositories;

namespace RUTS.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private string _Visibility = "Hidden";
        private string _Message;
        private string _Search = null;
        private bool _DeleteItem = false;
        private ICommand _ConfirmDelete;
        private ICommand _FilterData;
        protected IUnitofwork _UnitOfWork;
        protected static User AuthenticatedUser;
        protected string _ConfirmTransactionVisibility = "Hidden";
        private string _ConfirmVisibility = "Hidden";
        public static double _Height = 700;
        public static double _Width = 1300;
        public static string _CurrentPage = "Home";

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ICommand CloseToaster { get; set; }
        public ICommand FilterData
        {
            get
            {
                return _FilterData;
            }
            set
            {
                _FilterData = value;
                OnPropertyChanged(nameof(FilterData));
            }
        }
        public string Search
        {
            get
            {
                return _Search;
            }
            set
            {
                _Search = value;
                OnPropertyChanged(nameof(Search));
            }
        }
        public static string CurrentPage
        {
            get
            {
                return _CurrentPage;
            }
            set
            {
                _CurrentPage = value;
            }
        }
        public static double Width
        {
            get
            {
                return _Width;
            }
            set
            {
                _Width = value;
            }
        }
        public static double Height
        {
            get
            {
                return _Height;
            }
            set
            {
                _Height = value;
            }
        }
        protected bool DeleteItem
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
        public string ConfirmTransactionVisibility
        {
            get
            {
                return _ConfirmTransactionVisibility;
            }
            set
            {
                _ConfirmTransactionVisibility = value;
                OnPropertyChanged(nameof(ConfirmTransactionVisibility));
            }
        }
        public string Visibility
        {
            get
            {
                return _Visibility;
            }
            set
            {
                _Visibility = value;
                OnPropertyChanged(nameof(Visibility));
            }
        }

        public ICommand ConfirmDelete
        {
            get
            {
                return _ConfirmDelete;
            }
            set 
            {
                _ConfirmDelete = value;
                OnPropertyChanged(nameof(ConfirmDelete));
            }
        }
        protected void ExecuteCloseToasterCommand(object obj)
        {
            Visibility = "Hidden";
            ConfirmTransactionVisibility = "Hidden";
            ConfirmVisibility = "Hidden";
        }
        public string FeedbackMessage
        {
            get
            {
                return _Message;
            }
            set
            {
                _Message = value;
                OnPropertyChanged(nameof(FeedbackMessage));
            }
        }
        public string ConfirmVisibility
        {
            get
            {
                return _ConfirmVisibility;
            }
            set
            {
                _ConfirmVisibility = value;
                OnPropertyChanged(nameof(ConfirmVisibility));
            }
        }
    }
}
