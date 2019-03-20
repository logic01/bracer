using PhysiciansReach.Models;
using PR.Data.Models;

namespace PR.Business.Mappings
{
    public static class UserMappings
    {
        public static UserModel ToModel(this User entity)
        {
            var model = new UserModel
            {
                UserId = entity.UserId,
                UserName = entity.UserName,
                Password = entity.Password,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                PhoneNumber = entity.PhoneNumber,
                ContactFirstName = entity.ContactFirstName,
                ContactLastName = entity.ContactLastName,
                CreatedOn = entity.CreatedOn,
                ModifiedOn = entity.ModifiedOn
            };

            return model;
        }

        public static User ToEntity(this UserModel model)
        {
            var entity = new User
            {
                UserId = model.UserId,
                UserName = model.UserName,
                Password = model.Password,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                ContactFirstName = model.ContactFirstName,
                ContactLastName = model.ContactLastName,
                CreatedOn = model.CreatedOn,
                ModifiedOn = model.ModifiedOn
            };

            return entity;
        }
    }
}
