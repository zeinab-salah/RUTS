using Microsoft.Extensions.DependencyInjection;
using RUTS.Data;
using RUTS.Models;
using RUTS.Repositories;
using RUTS.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RUTS.ViewModel.Banks
{
    public class CreateBankViewModel : ViewModelBase
    {
        //Fields
        private int _Id;
        private string _Name;
        private IUnitofwork _UnitOfWork;

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

        //Commands
        public ICommand SaveBank { get; }

        //constructor
        public CreateBankViewModel(RUTSDesignTimeDbContextFactory dbContextFactory)
        {
            _UnitOfWork     = App.AppHost.Services.GetRequiredService<IUnitofwork>();
            CloseToaster    = new ViewModelCommand(ExecuteCloseToasterCommand);
            SaveBank        = new ViewModelCommand(ExecuteSaveCommand, CanExecuteSaveCommand);
        }
        private bool CanExecuteSaveCommand(object obj)
        {
            bool validateData;
            if (string.IsNullOrEmpty(Name))
                validateData = false;
            else
                validateData = true;
            return validateData;
        }
        private void ExecuteSaveCommand(object obj)
        {
            Bank bank;
            bool result = true;
            if (Id == 0)
            {
                bank = new Bank()
                {
                    Name = Name
                };
                _UnitOfWork.Bank.Add(bank);
                _UnitOfWork.Save();
            }
            else
            {
                bank = new Bank()
                {
                    Id = Id,
                    Name = Name
                };
                result = _UnitOfWork.Bank.Update(bank);
                _UnitOfWork.Save();
            }

            Visibility = "Visible";
            if (result)
            {
                if (Id == 0)
                {
                    Name = "";
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
