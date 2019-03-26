using PhysiciansReach.Models;
using System.Collections.Generic;

namespace PR.Business.Interfaces
{
    public interface IAdminBusiness
    {
        List<AdminModel> Get();

        AdminModel Get(int id);

        AdminModel Create(AdminModel userModel);

        AdminModel Update(AdminModel userModel);
    }
}
