using PR.Business.Business;
using PR.Data.Models;
using PR.Models;
using System;
using static PR.Data.Models.UserAccount;

namespace PR.Business.Mappings
{
    public static class UserMappings
    {
        public static UserAccountModel ToModel(this UserAccount entity)
        {
            // password intentionally not mapped
            var model = new UserAccountModel
            {
                UserAccountId = entity.UserAccountId,
                UserName = entity.UserName,
                Type = entity.Type.ToModel(),
                Active = entity.Active,
                CreatedOn = entity.CreatedOn,
                ModifiedOn = entity.ModifiedOn
            };


            return model;
        }

        public static UserAccount ToEntity(this UserAccountModel model)
        {
            var hash = new PasswordHash(model.Password);

            byte[] hashBytes = hash.ToArray();

            var entity = new UserAccount
            {
                UserAccountId = model.UserAccountId,
                UserName = model.UserName,
                Password = hashBytes,
                Active = model.Active,
                CreatedOn = model.CreatedOn,
                ModifiedOn = model.ModifiedOn
            };

            return entity;
        }

        public static PR.Models.Enum.AccountType ToModel(this PR.Data.Models.UserAccount.AccountType entity)
        {
            switch (entity)
            {
                case AccountType.Admin:
                    {
                        return Models.Enum.AccountType.Admin;
                    }
                case AccountType.Agent:
                    {
                        return Models.Enum.AccountType.Agent;
                    }
                case AccountType.Physician:
                    {
                        return Models.Enum.AccountType.Physician;
                    }
            }

            throw new Exception("Invalid Account Type");
        }
    }
}
