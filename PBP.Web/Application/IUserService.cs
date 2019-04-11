using PBP.Web.Models.Domain;

namespace PBP.Web.Application
{
    public interface IUserService
    {
        Account Login(string userName,string password);
        void Logout();
    }
}
