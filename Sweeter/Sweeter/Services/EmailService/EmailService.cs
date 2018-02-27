using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Sweeter.Services.EmailService
{
    public class EmailService:IEmailService
    {
        private readonly IEmailConfiguration _emailConfiguration;

        public EmailService(IEmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }

        public void SendEmail(MailMessage emailMessage)
        {
            SmtpClient client = new SmtpClient(_emailConfiguration.SmtpServer,_emailConfiguration.SmtpPort);
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);
            client.EnableSsl = true;
            emailMessage.From = new MailAddress(_emailConfiguration.SmtpUsername, "Jay mailing service");
            client.Send(emailMessage);
        }
    }
}
