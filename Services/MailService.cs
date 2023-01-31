using System.Net.Mail;
using System.Net;
using System.Text;

namespace Sklep_MVC_Projekt.Services
{
    public class MailService
    {
        private readonly CustomerService _customerService;
        private readonly ProductService _productService;
        public MailService(CustomerService customerService, ProductService productService)
        {
            _customerService = customerService;
            _productService = productService;
        }
        public void SendEmail(string body, string subject, string emailAddress, string name)
        {
            string email = "mvcshopemailer@gmail.com";
            string password = "zkrfzssmtuvrblua";

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

        public void SendNewsletterNewAndPromo()
        {
            var products = _productService.GetNewAndPromoProducts();
            StringBuilder sb = new StringBuilder();
            sb.Append("<h1>ZOBACZ PROMOCJE I NOWOŚCI</h1><br /><ul>");
            products.ForEach(p => { sb.Append($"<li>{p.ProductName} - {p.Price}</li>"); });
            sb.Append("</ul>");

            var customers = _customerService.NewsletterCustomers();

            foreach (var customer in customers)
            {
                SendEmail(sb.ToString(), "Newsletter", customer.Email, $"{customer.FirstName} {customer.LastName}");
            }
        }
    }
}
