using Microsoft.Extensions.DependencyInjection;
using RUTS.Data;
using RUTS.Models;
using RUTS.Repositories;
using RUTS.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace RUTS.ViewModel.Correspondents
{
    public class CreateCorrespondentViewModel : ViewModelBase
    {
        private int _Id = 0;
        private string _Name;
        private ComboBoxItem _Country ;
        private ICorrespondentRepository _CorrespondentRepository;

        public CreateCorrespondentViewModel()
        {
            _UnitOfWork = App.AppHost.Services.GetRequiredService<IUnitofwork>();
            CorrespondentRepository = _UnitOfWork.Correspondent;
            SaveCorrespondent       = new ViewModelCommand(ExecuteSaveCommand, CanExecuteSaveCommand);
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
        public ICorrespondentRepository CorrespondentRepository
        {
            get
            {
                return _CorrespondentRepository;
            }
            set
            {
                _CorrespondentRepository = value;
                OnPropertyChanged(nameof(CorrespondentRepository));
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
        public System.Windows.Controls.ComboBoxItem Country
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

        //Commands
        public ICommand SaveCorrespondent { get; }

        public void ExecuteSaveCommand(Object obj)
        {
            Correspondent _Correspondent;
            bool result = true;
            if (Id == 0)
            {
                _Correspondent = new Correspondent()
                {
                    Name = Name,
                    Country = Country.Content.ToString()
                };
                CorrespondentRepository.Add(_Correspondent);
                _UnitOfWork.Save();
            }
            else
            {
                _Correspondent = new Correspondent()
                {
                    Id = Id,
                    Name = Name,
                    Country = Country.Content.ToString()
                };
                result = CorrespondentRepository.Update(_Correspondent);
                _UnitOfWork.Save();
            }

            Visibility = "Visible";
            if (result)
            {
                if (Id == 0)
                {
                    Id = 0;
                    Name = "";
                    Country = null;
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

        public bool CanExecuteSaveCommand(Object obj)
        {
            bool canExecute;
            string countryValue = null;
            if (Country != null)
            {
                countryValue = Country.Content.ToString();
            }
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(countryValue))
                canExecute = false;
            else
                canExecute = true;
            return canExecute;
        }
    }
}
