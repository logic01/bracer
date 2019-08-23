using PR.Models;
namespace PR.Business.Interfaces
{
    public interface IAuthorizationBusiness
    {
        UserAccountModel Login(UserAccountModel userAccountModel);
    }
}
