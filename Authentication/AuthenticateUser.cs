using Microsoft.Extensions.DependencyInjection;
using RUTS.Models;
using RUTS.Repositories;
using RUTS.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RUTS.Authentication
{
    public class AuthenticateUser : IAuthenticate
    {
        private IUnitofwork _UnitOfWork;

        public AuthenticateUser()
        {
            _UnitOfWork = App.AppHost.Services.GetRequiredService<IUnitofwork>();
        }

        User IAuthenticate.Authenticate(NetworkCredential credentials)
        {
            User user = _UnitOfWork.User.GetFirstOrDefault(u => u.Email == credentials.UserName && u.Password == credentials.Password);
            return user;
        }
    }
}
