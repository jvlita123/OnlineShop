using System.Net.Mail;
using System.Net;

namespace Sklep_MVC_Projekt.Services
{
    public class MailService
    {
        public void SendEmail(string body, string subject, string emailAddress, string name)
        {
            string email = "mvcshopemailer@gmail.com";
            string password = "lubieplacki";

            SmtpClient client = new SmtpClient()
            {
                Port = 587,
                Host = "smtp.gmail.com",
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential()
                {
                    UserName = email,
                    Password = password
                }
            };

            MailAddress fromMailAddress = new MailAddress(email, "Sklepik");
            MailAddress toMailAddress = new MailAddress(emailAddress, name);

            MailMessage message = new MailMessage()
            {
                From = fromMailAddress,
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            message.To.Add(toMailAddress);

            try
            {
                client.Send(message);
                Console.WriteLine("Wiadomość poszła w świat");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
