using PR.Constants.Enums;
using PR.Data.Models;

namespace PR.Business.Utils
{
    public static class IntakeFormStatusFactory
    {
        public static IntakeFormStatus GetNext(IntakeForm intakeForm)
        {
            IntakeFormStatus nextStatus = intakeForm.Status;

            switch (intakeForm.Status)
            {
                case IntakeFormStatus.New:
                    nextStatus = IntakeFormStatus.Assigned;
                    break;
                case IntakeFormStatus.Assigned:
                    {
                        if (intakeForm.Signatures?.Count == 2)
                        {
                            nextStatus = IntakeFormStatus.Signed;
                        }
                    }
                    break;
                case IntakeFormStatus.Signed:
                    nextStatus = IntakeFormStatus.Emailed;
                    break;
                case IntakeFormStatus.Closed:
                    nextStatus = IntakeFormStatus.Closed;
                    break;
                default:
                    nextStatus = IntakeFormStatus.New;
                    break;
            }

            return nextStatus;
        }
    }
}
