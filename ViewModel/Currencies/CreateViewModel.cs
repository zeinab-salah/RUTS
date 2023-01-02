using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RUTS.Data;
using RUTS.Models;
using RUTS.Repositories;
using RUTS.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace RUTS.ViewModel.Currencies
{
    public class CreateViewModel: ViewModelBase
    {
        //Fields
        private int _Id = 0;
        private string _Name;
        private string _DollarExchangeRate;
        private ICurrencyRepository currencyRepository;

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
        public string Name { get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                OnPropertyChanged(nameof(Name));
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

        //Commands
        public ICommand SaveCurrency { get; }

        //constructor
        public CreateViewModel()
        {
            _UnitOfWork = App.AppHost.Services.GetRequiredService<IUnitofwork>();
            currencyRepository = _UnitOfWork.Currency;
            SaveCurrency = new ViewModelCommand(ExecuteSaveCommand, CanExecuteSaveCommand);
            CloseToaster = new ViewModelCommand(ExecuteCloseToasterCommand);
        }
        private bool CanExecuteSaveCommand(object obj)
        {
            bool validateData;
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(DollarExchangeRate))
                validateData = false;
            else
                validateData = true;
            return validateData;
        }
        private void ExecuteSaveCommand(object obj)
        {
            Currency currency;
            bool result = true;
            if (Id == 0)
            {
                currency = new Currency()
                {
                    Name = Name,
                    DollarExchangeRate = DollarExchangeRate,
                };
                currencyRepository.Add(currency);
                _UnitOfWork.Save();
            }
            else
            {
                currency = new Currency()
                {
                    Id                 = Id,
                    Name               = Name,
                    DollarExchangeRate = DollarExchangeRate,
                };
                result = currencyRepository.Update(currency);
                _UnitOfWork.Save();
            }

            Visibility = "Visible";
            if (result)
            {
                if (Id == 0)
                {
                    Name = "";
                    DollarExchangeRate = "";
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
