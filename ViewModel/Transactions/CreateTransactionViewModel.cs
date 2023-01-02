 using Microsoft.Extensions.DependencyInjection;
using RUTS.CustomModels;
using RUTS.Models;
using RUTS.Repositories;
using RUTS.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RUTS.ViewModel.Transactions
{
    public class CreateTransactionViewModel : ViewModelBase
    {
        //Fields
        private Transaction _Transaction;
        private IUnitofwork _UnitOfWork;
        private ComboData _SelectedCurrency;
        private ComboData _SelectedCorrespondent;
        private ComboData _SelectedBeneficary;
        private ComboData _SelectedAccount;
        private ComboData _SelectedBank;
        private ComboData _SelectedResource;
        private IList<ComboData> _BanksData;
        private IList<ComboData> _CorrespondentsData;
        private IList<ComboData> _CurrenciesData;
        private IList<ComboData> _BeneficariesData;
        private IList<ComboData> _ResourcesData;
        private string _EnableCurrency          = "false";
        private string _VisibilityBasedOnTypeBR = "Collapsed";
        private string _VisibilityBasedOnType   = "Collapsed";
        private System.Windows.Controls.ComboBoxItem _Type;

        public System.Windows.Controls.ComboBoxItem Type
        {
            get {
                return _Type; 
            }
            set
            {
                _Type = value;
                if (value != null)
                {
                    if (value.Content.ToString() == "Credit")
                    {
                        VisibilityBasedOnType = "Collapsed";
                        VisibilityBasedOnTypeBR = "Visible";
                    }
                    else
                    {
                        VisibilityBasedOnType = "Visible";
                        VisibilityBasedOnTypeBR = "Collapsed";
                    }
                }
                OnPropertyChanged(nameof(Type));
            }
        }
        public string EnableCurrency
        {
            get
            {
                return _EnableCurrency;
            }
            set
            {
                _EnableCurrency = value;
                OnPropertyChanged(nameof(EnableCurrency));
            }
        }
        public string VisibilityBasedOnTypeBR
        {
            get
            {
                return _VisibilityBasedOnTypeBR;
            }
            set
            {
                _VisibilityBasedOnTypeBR = value;
                OnPropertyChanged(nameof(VisibilityBasedOnTypeBR));
            }
        }
        public string VisibilityBasedOnType
        {
            get
            {
                return _VisibilityBasedOnType;
            }
            set
            {
                _VisibilityBasedOnType = value;
                OnPropertyChanged(nameof(VisibilityBasedOnType));
            }
        }
        public IUnitofwork UnitOfWork
        {
            get
            {
                return _UnitOfWork;
            }
            set
            {
                _UnitOfWork = value;
                OnPropertyChanged(nameof(UnitOfWork));
            }
        }
        public Transaction Transaction
        {
            get
            {
                return _Transaction;
            }
            set
            {
                _Transaction = value;
                OnPropertyChanged(nameof(Transaction));
            }
        }
        public ComboData SelectedResource
        {
            get
            {
                return _SelectedResource;
            }
            set
            {
                _SelectedResource = value;
                OnPropertyChanged(nameof(SelectedResource));
            }
        }
        public ComboData SelectedBank
        {
            get
            {
                return _SelectedBank;
            }
            set
            {
                _SelectedBank = value;
                OnPropertyChanged(nameof(SelectedBank));
            }
        }
        public ComboData SelectedAccount
        {
            get
            {
                return _SelectedAccount;
            }
            set
            {
                _SelectedAccount = value;
                OnPropertyChanged(nameof(SelectedAccount));
            }
        }
        public ComboData SelectedCurrency
        {
            get
            {
                return _SelectedCurrency;
            }
            set
            {
                _SelectedCurrency = value;
                OnPropertyChanged(nameof(SelectedCurrency));   
            }
        }
        public ComboData SelectedCorrespondent
        {
            get
            {
                return _SelectedCorrespondent;
            }
            set
            {
                EnableCurrency = "true";
                _SelectedCorrespondent = value;
                CurrenciesData = SelectedCorrespondent != null ? _UnitOfWork.Currency.GetComboDataByCorrespondent(SelectedCorrespondent.Id) : null;
                OnPropertyChanged(nameof(SelectedCorrespondent));
            }
        }
        public ComboData SelectedBeneficary
        {
            get
            {
                return _SelectedBeneficary;
            }
            set
            {
                _SelectedBeneficary = value;
                OnPropertyChanged(nameof(SelectedBeneficary));
            }
        }

        //constructor
        public CreateTransactionViewModel()
        {
            _UnitOfWork = App.AppHost.Services.GetRequiredService<IUnitofwork>();
            CorrespondentsData = _UnitOfWork.Correspondent.GetComboData();
            CurrenciesData = _UnitOfWork.Currency.GetComboData();
            BeneficariesData = _UnitOfWork.Beneficary.GetComboData();
            BanksData  = _UnitOfWork.Bank.GetComboData();
            ResourcesData = _UnitOfWork.ResourceItem.GetComboData();
            SaveTransaction = new ViewModelCommand(ExecuteSaveCommand, CanExecuteSaveCommand);
            CloseToaster = new ViewModelCommand(ExecuteCloseToasterCommand);
        }

        public ICommand SaveTransaction { get; }

        private bool CanExecuteSaveCommand(object obj)
        {
            bool validateData=true;
            if (string.IsNullOrEmpty(Transaction.Amount) || SelectedCurrency == null || SelectedCorrespondent == null || (Type == null || (Type.Content.ToString() == "Debit" && SelectedBeneficary == null) || (Type.Content.ToString() == "Credit" && (SelectedBank == null || SelectedResource == null))))
                validateData = false;
            else
                validateData = true;
            return validateData;
        }
        public IList<ComboData> BanksData
        {
            get
            {
                return _BanksData;
            }
            set
            {
                _BanksData = value;
                OnPropertyChanged(nameof(BanksData));
            }
        }
        public IList<ComboData> ResourcesData
        {
            get
            {
                return _ResourcesData;
            }
            set
            {
                _ResourcesData = value;
                OnPropertyChanged(nameof(ResourcesData));
            }
        }
        public IList<ComboData> BeneficariesData
        {
            get
            {
                return _BeneficariesData;
            }
            set
            {
                _BeneficariesData = value;
                OnPropertyChanged(nameof(BeneficariesData));
            }
        }
        public IList<ComboData> CurrenciesData
        {
            get
            {
                return _CurrenciesData;
            }
            set
            {
                _CurrenciesData = value;
                OnPropertyChanged(nameof(CurrenciesData));
            }
        }
        public IList<ComboData> CorrespondentsData
        {
            get
            {
                return _CorrespondentsData;
            }
            set
            {
                _CorrespondentsData = value;
                OnPropertyChanged(nameof(CorrespondentsData));
            }
        }
        private void ExecuteSaveCommand(object obj)
        {
            bool result = true;
            int Id      = Transaction.Id;
            if (Transaction.Id == 0)
            {
                Transaction = new Transaction()
                {
                    CorrespondentId = SelectedCorrespondent.Id,
                    CurrencyId      = SelectedCurrency.Id,
                    Amount          = Transaction.Amount,
                    Type            = Type.Content.ToString(),
                    BankId          = Type.Content.ToString() == "Credit" ? SelectedBank.Id : null,
                    BenificaryId    = Type.Content.ToString() == "Debit" ? SelectedBeneficary.Id : null,
                    UserId          = App.AuthenticatedUser.Id,
                    ResourcesItemId = Type.Content.ToString() == "Credit" ? SelectedResource.Id : null,
                    Narration       = Transaction.Narration,
                    StatementDate   = Transaction.StatementDate
                };
                UnitOfWork.Transaction.Add(Transaction);
                _UnitOfWork.Save();
            }
            else
            {
                Id          = Transaction.Id;
                Transaction = new Transaction()
                {
                    Id              = Transaction.Id,
                    CorrespondentId = SelectedCorrespondent.Id,
                    CurrencyId      = SelectedCurrency.Id,
                    Amount          = Transaction.Amount,
                    Type            = Transaction.Type,
                    BankId          = Type.Content.ToString() == "Credit" ? SelectedBank.Id : null,
                    BenificaryId    = Type.Content.ToString() == "Debit" ? SelectedBeneficary.Id : null,
                    UserId          = App.AuthenticatedUser.Id,
                    ResourcesItemId = Type.Content.ToString() == "Credit" ? SelectedResource.Id : null,
                    Narration       = Transaction.Narration,
                    StatementDate   = Transaction.StatementDate
                };
                result = UnitOfWork.Transaction.Update(Transaction);
                _UnitOfWork.Save();
            }

            Visibility = "Visible";
            if (result)
            {
                if (Id == 0)
                {
        Transaction = new Transaction();
                    SelectedAccount       = null;
                    SelectedBank          = null;
                    SelectedBeneficary    = null;
                    SelectedCorrespondent = null;
                    SelectedCurrency      = null;
                    SelectedResource      = null;
                    Type                  = null;
                    FeedbackMessage       = "Added Successfully!";
                }
                else
                {
                    FeedbackMessage = "Updated Successfully!";
                }
            }
            else
            {
                FeedbackMessage = "Something went wrong";
            }
        }
    }
}
