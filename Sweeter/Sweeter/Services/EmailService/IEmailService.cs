using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Sweeter.Services.EmailService
{
    public interface IEmailService
    {
        void SendEmail(MailMessage email);
    }
}
