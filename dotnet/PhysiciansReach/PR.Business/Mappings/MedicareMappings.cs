using PR.Data.Models;
using PR.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PR.Business.Mappings
{
    public static class MedicareMappings
    {
        public static MedicareModel ToModel(this Medicare entity)
        {
            return new MedicareModel
            {
                MedicareId = entity.MedicareId,
                MemberId = entity.MemberId,
                PatientGroup = entity.PatientGroup,
                Pcn = entity.Pcn,
                SubscriberNumber = entity.SubscriberNumber,
                SecondaryCarrier = entity.SecondaryCarrier,
                SecondarySubscriberNumber = entity.SecondarySubscriberNumber
            };
        }

        public static Medicare ToEntity(this MedicareModel model)
        {
            return new Medicare
            {
                MedicareId = model.MedicareId,
                MemberId = model.MemberId,
                PatientGroup = model.PatientGroup,
                Pcn = model.Pcn,
                SubscriberNumber = model.SubscriberNumber,
                SecondaryCarrier = model.SecondaryCarrier,
                SecondarySubscriberNumber = model.SecondarySubscriberNumber
            };
        }
    }
}
