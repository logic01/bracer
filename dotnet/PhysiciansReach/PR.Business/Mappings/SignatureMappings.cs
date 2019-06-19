using PR.Data.Models;
using PR.Models;
using System;
using System.Text.RegularExpressions;

namespace PR.Business.Mappings
{
    public static class SignatureMappings
    {
        public static void MapFromModel(this Signature entity, SignatureModel model)
        {
            entity.SignatureId = model.SignatureId;
            entity.IpAddress = model.IpAddress;
            entity.Type = model.Type;
            entity.CreatedOn = model.CreatedOn;
            entity.ModifiedOn = model.ModifiedOn;

            var base64Data = Regex.Match(model.Content, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
            var signatureBytes = Convert.FromBase64String(base64Data);

            entity.Content = signatureBytes;
        }

        public static SignatureModel ToModel(this Signature entity)
        {
            return new SignatureModel
            {
                SignatureId = entity.SignatureId,
                IpAddress = entity.IpAddress,
                Type = entity.Type,
                ContentBytes = entity.Content,
                CreatedOn = entity.CreatedOn,
                ModifiedOn = entity.ModifiedOn
            };
        }

    }
}
