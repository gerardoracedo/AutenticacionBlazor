using AutenticacionBlazor.Shared.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Shared.Servicios
{
    public class SendMail : ISendMail
    {
        private readonly MailSettings _mailConfig;
        public SendMail(MailSettings mailConfig)
        {
            _mailConfig = mailConfig;
        }
        public async Task SendEmailAsync(string ToEmail, string Subject, string HTMLBody)
        {

            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient(_mailConfig.SmtpServer);
            message.From = new MailAddress(_mailConfig.From);
            message.To.Add(new MailAddress(ToEmail));
            message.Subject = Subject;
            message.IsBodyHtml = true;
            message.Body = HTMLBody;
            smtp.Port = _mailConfig.Port;            
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(_mailConfig.UserName, _mailConfig.Password);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            try
            {
                await smtp.SendMailAsync(message);
            }
            catch (Exception e)
            {

                throw e;
            }
        }        
    }
}

