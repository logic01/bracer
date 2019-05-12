using PR.Models;
using System.Collections.Generic;

namespace PR.Export
{
    public interface IIntakeFormExporter
    {
        byte[] CreateNewIntakeForm(IList<IntakeFormFullModel> intakeForms);
    }
}
