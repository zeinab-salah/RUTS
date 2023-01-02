using RUTS.Data;
using RUTS.Models;
using RUTS.Repositories;
using RUTS.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RUTS.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        private UserAccountModel _currentUser;
        private IUserRepository _userRepository;

        public HomeViewModel(RUTSDesignTimeDbContextFactory dbContextFactory)
        {
            _userRepository = new Unitofwork(dbContextFactory).User;
            LoadCurrentUserData();
        }

        private void LoadCurrentUserData()
        {
            //var user = UserRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
        }

        public UserAccountModel CurrentUser
        {
            get
            {
               return _currentUser;
            }
            set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }
    }
}
