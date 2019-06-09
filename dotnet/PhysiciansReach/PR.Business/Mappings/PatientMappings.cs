using PR.Data.Models;
using PR.Models;

namespace PR.Business.Mappings
{
    public static class PatientMappings
    {
        public static PatientModel ToModel(this Patient entity)
        {
            var model = new PatientModel
            {
                PatientId = entity.PatientId,
                AgentId = entity.AgentId,
                Language = entity.Language,
                Sex = entity.Sex,
                BestTimeToCallBack = entity.BestTimeToCallBack,
                Therapy = entity.Therapy,
                Insurance = entity.Insurance,
                Pharmacy = entity.Pharmacy,
                FirstName = entity.FirstName,
                MiddleName = entity.MiddleName,
                LastName = entity.LastName,
                PhoneNumber = entity.PhoneNumber,
                DateOfBirth = entity.DateOfBirth,
                CallBackImmediately = entity.CallBackImmediately,
                IsDme = entity.IsDme,
                Medications = entity.Medications,
                Notes = entity.Notes,
                OtherProducts = entity.OtherProducts,
                PhysiciansName = entity.PhysiciansName,
                PhysiciansPhoneNumber = entity.PhysiciansPhoneNumber,
                CreatedOn = entity.CreatedOn,
                ModifiedOn = entity.ModifiedOn,
                Address = entity.Address.ToModel(),
                PhysiciansAddress = entity.PhysiciansAddress?.ToModel(),
                PrivateInsurance = entity.PrivateInsurance?.ToModel(),
                Medicare = entity.Medicare?.ToModel(),
                Waist = entity.Waist,
                Weight = entity.Weight,
                Height = entity.Height,
                ShoeSize = entity.ShoeSize,
                Allergies = entity.Allergies

            };

            return model;
        }

        public static Patient ToEntity(this PatientModel model)
        {
            var entity = new Patient
            {
                PatientId = model.PatientId,
                AgentId = model.AgentId,
                Language = model.Language,
                Sex = model.Sex,
                BestTimeToCallBack = model.BestTimeToCallBack,
                Therapy = model.Therapy,
                Insurance = model.Insurance,
                Pharmacy = model.Pharmacy,
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                DateOfBirth = model.DateOfBirth,
                CallBackImmediately = model.CallBackImmediately,
                IsDme = model.IsDme,
                Medications = model.Medications,
                Notes = model.Notes,
                OtherProducts = model.OtherProducts,
                PhysiciansName = model.PhysiciansName,
                PhysiciansPhoneNumber = model.PhysiciansPhoneNumber,
                CreatedOn = model.CreatedOn,
                ModifiedOn = model.ModifiedOn,
                Address = model.Address.ToEntity(),
                PhysiciansAddress = model.PhysiciansAddress?.ToEntity(),
                PhysiciansAddressId = model.PhysiciansAddress?.AddressId,
                Medicare = model.Medicare?.ToEntity(),
                PrivateInsurance = model.PrivateInsurance?.ToEntity(),
                Waist = model.Waist,
                Weight = model.Weight,
                Height = model.Height,
                ShoeSize = model.ShoeSize,
                Allergies = model.Allergies
            };

            return entity;
        }


    }
}
