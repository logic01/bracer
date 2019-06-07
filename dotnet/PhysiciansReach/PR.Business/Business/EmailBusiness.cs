using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PR.Business.Interfaces;
using PR.Constants.Configurations;
using PR.Data.Models;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace PR.Business.Business
{
    public class EmailBusiness : IEmailBusiness
    {
        private readonly DataContext _context;
        private readonly SmtpSettings _settings;

        public EmailBusiness(DataContext context, IOptions<SmtpSettings> settings)
        {
            _context = context;
            _settings = settings.Value;
        }

        public bool SendEmail(int documentId, string emailAddress)
        {
        /*    Document document = _context.Document
                .Include(d => d.Physician)
                .FirstOrDefault(d => d.DocumentId == documentId);

            Physician physician = document.Physician;

            var message = new MailMessage();
            message.From = new MailAddress(_settings.User);

            message.To.Add(new MailAddress(emailAddress));

            message.Subject = "your subject";
            message.Body = "content of your email";

            var client = new SmtpClient();
            client.Host = "relay-hosting.secureserver.net";
            client.Port = 25;
            client.Send(message);


            var smtp = new SmtpClient
            {
                EnableSsl = false,
                UseDefaultCredentials = false,
                Host = _settings.Server,
                Port = _settings.Port,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(_settings.User, _settings.Password)
            };

           // var data = new Attachment("PATH_TO_YOUR_FILE", MediaTypeNames.Application.Octet);

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = "Physicians Reach",
                Body = "Please see attached"
            })
            {
               // message.Attachments.Add(data);
                smtp.Send(message);
            }
            */
            return true;
        }
    }
}
