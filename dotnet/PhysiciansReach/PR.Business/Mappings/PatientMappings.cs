using PR.Data.Models;
using PR.Models;
using System;

namespace PR.Business.Mappings
{
    public static class PatientMappings
    {
        public static PatientModel ToModel(this Patient entity)
        {
            PatientModel model = new PatientModel
            {
                FirstName = entity.FirstName,
                MiddleName = entity.MiddleName,
                LastName = entity.LastName,
                PhoneNumber = entity.PhoneNumber,
                DateOfBirth = entity.DateOfBirth,
                CallBackImmediately = entity.CallBackImmediately,
                BestTimeToCallBack = entity.BestTimeToCallBack.ToModel(),
                Sex = entity.Sex.ToModel(),
                Language = entity.Language.ToModel(),
                CreatedOn = entity.CreatedOn,
                ModifiedOn = entity.ModifiedOn,
                Address = entity.Address.ToModel()
            };

            return model;
        }

        public static Patient ToEntity(this PatientModel model)
        {
            Patient entity = new Patient
            {
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                DateOfBirth = model.DateOfBirth,
                CallBackImmediately = model.CallBackImmediately,
                BestTimeToCallBack = model.BestTimeToCallBack.ToEntity(),
                Sex = model.Sex.ToEntity(),
                Language = model.Language.ToEntity(),
                CreatedOn = model.CreatedOn,
                ModifiedOn = model.ModifiedOn,
                Address = model.Address.ToEntity()
            };

            return entity;
        }

        public static PR.Models.Enum.CallbackTime ToModel(this PR.Data.Models.Patient.CallbackTime entity)
        {
            switch (entity)
            {
                case PR.Data.Models.Patient.CallbackTime.Afternoon:
                    {
                        return PR.Models.Enum.CallbackTime.Afternoon;
                    }
                case PR.Data.Models.Patient.CallbackTime.Evening:
                    {
                        return PR.Models.Enum.CallbackTime.Evening;
                    }
                case PR.Data.Models.Patient.CallbackTime.Morning:
                    {
                        return PR.Models.Enum.CallbackTime.Morning;
                    }
            }

            throw new Exception("Invalid CallbackTime Type");
        }

        public static PR.Models.Enum.SexType ToModel(this PR.Data.Models.Patient.SexType entity)
        {
            switch (entity)
            {
                case PR.Data.Models.Patient.SexType.Female:
                    {
                        return PR.Models.Enum.SexType.Female;
                    }
                case PR.Data.Models.Patient.SexType.Male:
                    {
                        return PR.Models.Enum.SexType.Male;
                    }
            }

            throw new Exception("Invalid Sex Type");
        }

        public static PR.Models.Enum.LanguageType ToModel(this PR.Data.Models.Patient.LanguageType entity)
        {
            switch (entity)
            {
                case PR.Data.Models.Patient.LanguageType.English:
                    {
                        return PR.Models.Enum.LanguageType.English;
                    }
                case PR.Data.Models.Patient.LanguageType.Spanish:
                    {
                        return PR.Models.Enum.LanguageType.Spanish;
                    }
            }

            throw new Exception("Invalid Language Type");
        }

        public static PR.Data.Models.Patient.CallbackTime ToEntity(this PR.Models.Enum.CallbackTime model)
        {
            switch (model)
            {
                case PR.Models.Enum.CallbackTime.Afternoon:
                    {
                        return PR.Data.Models.Patient.CallbackTime.Afternoon;
                    }
                case PR.Models.Enum.CallbackTime.Evening:
                    {
                        return PR.Data.Models.Patient.CallbackTime.Evening;
                    }
                case PR.Models.Enum.CallbackTime.Morning:
                    {
                        return PR.Data.Models.Patient.CallbackTime.Morning;
                    }
            }

            throw new Exception("Invalid CallbackTime Type");
        }

        public static PR.Data.Models.Patient.SexType ToEntity(this PR.Models.Enum.SexType model)
        {
            switch (model)
            {
                case PR.Models.Enum.SexType.Female:
                    {
                        return PR.Data.Models.Patient.SexType.Female;
                    }
                case PR.Models.Enum.SexType.Male:
                    {
                        return PR.Data.Models.Patient.SexType.Male;
                    }
            }

            throw new Exception("Invalid Sex Type");
        }

        public static PR.Data.Models.Patient.LanguageType ToEntity(this PR.Models.Enum.LanguageType model)
        {
            switch (model)
            {
                case PR.Models.Enum.LanguageType.English:
                    {
                        return PR.Data.Models.Patient.LanguageType.English;
                    }
                case PR.Models.Enum.LanguageType.Spanish:
                    {
                        return PR.Data.Models.Patient.LanguageType.Spanish;
                    }
            }

            throw new Exception("Invalid Language Type");
        }

    }
}
