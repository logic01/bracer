﻿using PR.Constants.Enums;
using PR.Data.Models;
using PR.Models;

namespace PR.Business.Mappings
{
    public static class AdminMappings
    {
        public static AdminModel ToModel(this Admin entity)
        {
            var model = new AdminModel
            {
                UserAccount = entity.UserAccount.ToModel(),
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                CreatedOn = entity.CreatedOn,
                ModifiedOn = entity.ModifiedOn
            };

            return model;
        }

        public static Admin ToEntity(this AdminModel model)
        {
            var entity = new Admin
            {
                UserAccount = model.UserAccount.ToEntity(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                CreatedOn = model.CreatedOn,
                ModifiedOn = model.ModifiedOn
            };

            entity.UserAccount.Type = AccountType.Admin;

            return entity;
        }
    }
}
