using PhysiciansReach.Models;

namespace PR.Business.Interfaces
{
    public interface ILoginBusiness
    {
        UserAccountModel Login(UserAccountModel userAccountModel);
    }
}
