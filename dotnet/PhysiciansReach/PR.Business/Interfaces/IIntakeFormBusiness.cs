using PR.Models;
using System.Collections.Generic;
namespace PR.Business.Interfaces
{
    public interface IIntakeFormBusiness
    {
        List<IntakeFormModel> Get();

        IntakeFormModel Get(int id);

        IntakeFormModel Create(IntakeFormModel intakeFormModel);

        IntakeFormModel Update(IntakeFormModel intakeFormModel);

        IList<IntakeFormFullModel> GetFullIntakeForms(IEnumerable<int> ids);
    }
}
