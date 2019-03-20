using PhysiciansReach.Models;
using System.Collections.Generic;

namespace PR.Business.Interfaces
{
    public interface IUserBusiness
    {
        List<UserModel> Get(int[] userIds);

        UserModel Get(int userId);

        UserModel Create(UserModel userModel);

        UserModel Update(UserModel userModel);
    }
}
