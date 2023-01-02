using Microsoft.Extensions.DependencyInjection;
using RUTS.Authentication;
using RUTS.Data;
using RUTS.Repositories;
using RUTS.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RUTS.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        //Fields 
        private string _username;
        private SecureString _password;
        private string _errorMessage;
        private bool _isViewVisible = true;
        private IAuthenticate authenticateUser;

        public string Username
        {
            get
            {
                return _username;
            }
            set 
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public SecureString Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
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

        //-> Commands
        public ICommand LoginCommand { get; }
        public ICommand RecoverPasswordCommand { get; }
        public ICommand ShowPasswordCommand { get; }
        public ICommand RememberPasswordCommand { get; }

        //Constructor
        public LoginViewModel()
        {
            RUTSDesignTimeDbContextFactory dbContextFactory = App.AppHost.Services.GetRequiredService<RUTSDesignTimeDbContextFactory>();
            authenticateUser = new AuthenticateUser();
            LoginCommand           = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            RecoverPasswordCommand = new ViewModelCommand(p => ExecuteRecoverPasswordCommand("", "") );
        }

        private bool CanExecuteLoginCommand(object obj)
        {
            bool validateData;
            if (string.IsNullOrEmpty(Username) || Username.Length < 3
               || Password == null || Password.Length < 3)
                validateData = false;
            else
                validateData = true;
            return validateData;
        }
        private void ExecuteLoginCommand(object obj)
        {
            var userResult = authenticateUser.Authenticate(new System.Net.NetworkCredential(Username, Password));
            if (userResult != null)
            {
                App.AuthenticatedUser = userResult;
                Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(Username), null);
                IsViewVisible = false;
            }
            else
            {
                ErrorMessage = "Invalid username of password";

            }
        }
        private void ExecuteRecoverPasswordCommand(string username, string email)
        {
            throw new NotImplementedException();
        }
    }
}
