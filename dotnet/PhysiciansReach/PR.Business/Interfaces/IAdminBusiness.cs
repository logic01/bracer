using PhysiciansReach.Models;
using System.Collections.Generic;

namespace PR.Business.Interfaces
{
    public interface IAdminBusiness
    {
        List<AdminModel> Get(int[] ids);

        AdminModel Get(int id);

        AdminModel Create(AdminModel userModel);

        AdminModel Update(AdminModel userModel);
    }
}
