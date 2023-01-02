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

namespace RUTS.ViewModel.Benificaries
{
    public class CreateBeneficaryViewModel : ViewModelBase
    {
        //Fields
        private int _Id;
        private string _Name;
        private IBeneficariesRepository _BeneficariesRepository;

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
        public IBeneficariesRepository BeneficariesRepository
        {
            get
            {
                return _BeneficariesRepository;
            }
            set
            {
                _BeneficariesRepository = value;
                OnPropertyChanged(nameof(BeneficariesRepository));
            }
        }

        //Commands
        public ICommand SaveBeneficary { get; }

        //constructor
        public CreateBeneficaryViewModel(RUTSDesignTimeDbContextFactory dbContextFactory)
        {
            _UnitOfWork            = App.AppHost.Services.GetRequiredService<IUnitofwork>();
            BeneficariesRepository = _UnitOfWork.Beneficary;
            CloseToaster           = new ViewModelCommand(ExecuteCloseToasterCommand);
            SaveBeneficary         = new ViewModelCommand(ExecuteSaveCommand, CanExecuteSaveCommand);
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
            Benificary benificary;
            bool result = true;
            if (Id == 0)
            {
                benificary = new Benificary()
                {
                    Name = Name
                };
                BeneficariesRepository.Add(benificary);
                _UnitOfWork.Save();
            }
            else
            {
                benificary = new Benificary()
                {
                    Id = Id,
                    Name = Name
                };
                result = BeneficariesRepository.Update(benificary);
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
