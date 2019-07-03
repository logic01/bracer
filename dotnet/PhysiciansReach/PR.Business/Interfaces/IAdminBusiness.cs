using PR.Models;
using System.Collections.Generic;

namespace PR.Business.Interfaces
{
    public interface IAdminBusiness
    {
        List<AdminModel> Get();

        AdminModel Get(int id);

        AdminModel Create(AdminModel adminModel);

        int Update(AdminModel adminModel);
    }
}
