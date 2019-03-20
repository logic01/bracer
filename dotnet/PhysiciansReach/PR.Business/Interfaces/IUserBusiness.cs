using PhysiciansReach.Models;
using System.Collections.Generic;

namespace PR.Business.Interfaces
{
    public interface IUserBusiness
    {
        List<UserAccountModel> Get(int[] userIds);

        UserAccountModel Get(int userId);

        UserAccountModel Create(UserAccountModel userModel);

        UserAccountModel Update(UserAccountModel userModel);
    }
}
