

using RUTS.Models;
using System.Net;

namespace RUTS.Authentication
{
    public interface IAuthenticate
    {
        User Authenticate(NetworkCredential credentials);
    }
}
