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
                FirstName = entity.FirstName,
                MiddleName = entity.MiddleName,
                LastName = entity.LastName,
                PhoneNumber = entity.PhoneNumber,
                DateOfBirth = entity.DateOfBirth,
                IsDme = entity.IsDme,
                MainPainArea = entity.MainPainArea,
                SecondPainArea = entity.SecondPainArea,
                HadBraceBefore = entity.HadBraceBefore,
                PainCream = entity.PainCream,
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


        /// <summary>
        /// Takes an EF Core Entity and maps the model to it
        /// </summary>
        /// <param name="model"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static void MapFromModel(this Patient entity, PatientModel model)
        {
            // don't map primary key
            //entity.PatientId = model.PatientId;
            entity.AgentId = model.AgentId;
            entity.Language = model.Language;
            entity.Sex = model.Sex;
            entity.BestTimeToCallBack = model.BestTimeToCallBack;
            entity.Therapy = model.Therapy;
            entity.Insurance = model.Insurance;
            entity.FirstName = model.FirstName;
            entity.MiddleName = model.MiddleName;
            entity.LastName = model.LastName;
            entity.PhoneNumber = model.PhoneNumber;
            entity.DateOfBirth = model.DateOfBirth;
            entity.IsDme = model.IsDme;
            entity.MainPainArea = model.MainPainArea;
            entity.SecondPainArea = model.SecondPainArea;
            entity.HadBraceBefore = model.HadBraceBefore;
            entity.PainCream = model.PainCream;
            entity.Medications = model.Medications;
            entity.Notes = model.Notes;
            entity.OtherProducts = model.OtherProducts;
            entity.PhysiciansName = model.PhysiciansName;
            entity.PhysiciansPhoneNumber = model.PhysiciansPhoneNumber;
            entity.CreatedOn = model.CreatedOn;
            entity.ModifiedOn = model.ModifiedOn;
            entity.Address = model.Address.ToEntity();
            entity.PhysiciansAddress = model.PhysiciansAddress?.ToEntity();
            entity.PhysiciansAddressId = model.PhysiciansAddress?.AddressId;
            entity.Medicare = model.Medicare?.ToEntity();
            entity.PrivateInsurance = model.PrivateInsurance?.ToEntity();
            entity.Waist = model.Waist;
            entity.Weight = model.Weight;
            entity.Height = model.Height;
            entity.ShoeSize = model.ShoeSize;
            entity.Allergies = model.Allergies;
        }

    }
}
