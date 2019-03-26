using PhysiciansReach.Models;
using System.Collections.Generic;

namespace PR.Business.Interfaces
{
    public interface IPhysicianBusiness
    {
        List<PhysicianModel> Get();

        PhysicianModel Get(int id);

        PhysicianModel Create(PhysicianModel physicianModel);

        PhysicianModel Update(PhysicianModel physicianModel);
    }
}
