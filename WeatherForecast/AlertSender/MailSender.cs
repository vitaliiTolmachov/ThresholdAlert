using System.Net;
using System.Net.Mail;

namespace AlertSender
{
    internal class MailSender
    {
        private readonly SmtpClient _mailClient;
        private readonly MailAddress fromAddress = new("alerts.notifications.api@gmail.com", "John Doe");
        private readonly MailAddress toAddress = new("v.tolmachov@gmail.com", "Admin");
        const string fromPassword = "shvouznihplqgzle";
        const string subject = "Threshold reached";

        //TODO: Move to settings
        public MailSender()
        {
            _mailClient = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
        }

        public void SendAlert(string hostName)
        {
            using var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = $"You reached your threshold to host: {hostName}! Please contact your administrator"
            };
            _mailClient.Send(message);
        }
    }
}
