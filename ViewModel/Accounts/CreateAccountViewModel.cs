using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Office.Interop.Excel;
using RUTS.CustomModels;
using RUTS.Data;
using RUTS.Models;
using RUTS.Repositories;
using RUTS.Repositories.IRepositories;
using RUTS.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RUTS.ViewModel.Accounts
{
    public class CreateAccountViewModel : ViewModelBase
    {
        private int _Id = 0;
        private string _AccountNumber;
        private string _Ballance;
        private string _CurrencyId;
        private ComboData _SelectedCurrency;
        private ComboData _SelectedCorrespondent;
        private IList<ComboData> _CorrespondentsData;
        private IList<ComboData> _CurrenciesData;
        private string _CorrespondentId;
        private IAccountRepository _AccountRepository;
        private ICorrespondentRepository _CorrespondentRepository;
        private ICurrencyRepository _CurrencyRepository;
        private ICorrespondentRepository CorrespondentRepository;
        private IEnumerable<Currency> _Currencies;
        private IEnumerable<Correspondent> _Correspondents;
        public CreateAccountViewModel()
        {
            _UnitOfWork             = App.AppHost.Services.GetRequiredService<IUnitofwork>();
            AccountRepository       = _UnitOfWork.Account;
            CorrespondentRepository = _UnitOfWork.Correspondent;
            CurrencyRepository      = _UnitOfWork.Currency;
            _Currencies             = CurrencyRepository.GetAll();
            CorrespondentsData      = CorrespondentRepository.GetComboData();
            CurrenciesData          = CurrencyRepository.GetComboData();
            SaveAccount             = new ViewModelCommand(ExecuteSaveCommand, CanExecuteSaveCommand);
            CloseToaster            = new ViewModelCommand(ExecuteCloseToasterCommand);
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
        public ComboData SelectedCorrespondent
        {
            get
            {
                return _SelectedCorrespondent;
            }
            set
            {
                _SelectedCorrespondent = value;
                OnPropertyChanged(nameof(SelectedCorrespondent));
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
        public string CorrespondentId
        {
            get
            {
                return _CorrespondentId;
            }
            set
            {
                _CorrespondentId = value;
                OnPropertyChanged(nameof(CorrespondentId));
            }
        }
        public ICurrencyRepository CurrencyRepository
        {
            get
            {
                return _CurrencyRepository;
            }
            set
            {
                _CurrencyRepository = value;
                OnPropertyChanged(nameof(CurrencyRepository));
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
        public IAccountRepository AccountRepository
        {
            get
            {
                return _AccountRepository;
            }
            set
            {
                _AccountRepository = value;
                OnPropertyChanged(nameof(AccountRepository));
            }
        }
        public string AccountNumber
        {
            get
            {
                return _AccountNumber;
            }
            set
            {
                _AccountNumber = value;
                OnPropertyChanged(nameof(AccountNumber));
            }
        }
        public string CurrencyId
        {
            get
            {
                return _CurrencyId;
            }
            set
            {
                _CurrencyId = value;
                OnPropertyChanged(nameof(Currency));
            }
        }
        public string Ballance
        {
            get
            {
                return _Ballance;
            }
            set
            {
                _Ballance = value;
                OnPropertyChanged(nameof(Ballance));
            }
        }

        //commands
        public ICommand SaveAccount { get; }

        private bool CanExecuteSaveCommand(object obj)
        {
            bool validateData;
            if (string.IsNullOrEmpty(Ballance) || string.IsNullOrEmpty(AccountNumber))
                validateData = false;
            else
                validateData = true;
            return validateData;
        }
        private void ExecuteSaveCommand(object obj)
        {
            Account Account;
            bool result = true;
            if (Id == 0)
            {
                Account = new Account()
                {
                    Ballance        = Ballance,
                    CorrespondentId = SelectedCorrespondent.Id,
                    CurrencyId      = SelectedCurrency.Id,
                    AccountNumber   = AccountNumber
                };
                AccountRepository.Add(Account);
                _UnitOfWork.Save();
            }
            else
            {
                Account = new Account()
                {
                    Id              = Id,
                    Ballance        = Ballance,
                    CorrespondentId = SelectedCorrespondent.Id,
                    CurrencyId      = SelectedCurrency.Id,
                    AccountNumber   = AccountNumber
                };
                result = AccountRepository.Update(Account);
                _UnitOfWork.Save();
            }

            Visibility = "Visible";
            if (result)
            {
                if (Id == 0)
                {
                    Ballance      = "";
                    CurrencyId    = "";
                    AccountNumber = "";
                    SelectedCorrespondent = null;
                    SelectedCurrency = null;
                    FeedbackMessage = "Added Successfully!";
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
