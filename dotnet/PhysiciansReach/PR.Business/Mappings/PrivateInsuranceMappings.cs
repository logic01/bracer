using PR.Data.Models;
using PR.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PR.Business.Mappings
{
    public static class PrivateInsuranceMappings
    {
        public static PrivateInsuranceModel ToModel(this PrivateInsurance entity)
        {
            return new PrivateInsuranceModel
            {
                PrivateInsuranceId = entity.PrivateInsuranceId,
                Insurance = entity.Insurance,
                InsuranceId = entity.InsuranceId,
                Group = entity.Group,
                PCN = entity.PCN,
                Bin = entity.Bin,
                Street = entity.Street,
                City = entity.City,
                State = entity.State,
                Zip = entity.Zip,
                Phone = entity.Phone
            };
        }

        public static PrivateInsurance ToEntity(this PrivateInsuranceModel model)
        {
            return new PrivateInsurance
            {
                PrivateInsuranceId = model.PrivateInsuranceId,
                Insurance = model.Insurance,
                InsuranceId = model.InsuranceId,
                Group = model.Group,
                PCN = model.PCN,
                Bin = model.Bin,
                Street = model.Street,
                City = model.City,
                State = model.State,
                Zip = model.Zip,
                Phone = model.Phone
            };
        }
    }
}
