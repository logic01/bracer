using PR.Models;
using System.Collections.Generic;

namespace PR.Business.Interfaces
{
    public interface IPhysicianBusiness
    {
        PhysicianModel Get(int id);

        List<PhysicianModel> GetAll();

        List<PhysicianModel> Get(int[] ids);

        PhysicianModel Create(PhysicianModel physicianModel);

        PhysicianModel Update(PhysicianModel physicianModel);
    }
}
