using Messages;
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

        public void SendAlert(AlertMessage alertMessage)
        {
            var messageBody = $"Dear {alertMessage.UserFirstName} {alertMessage.UserLastName}, " +
                $"your API calss threshold for username {alertMessage.UserName} to host {alertMessage.RequestedHost} " +
                $"is reached {alertMessage.Percentage} percent of {alertMessage.CallLimit} call limit for your mounthly subscription." +
                $"\r\nPlease contact your administrator";

            using var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = messageBody
            };
            _mailClient.Send(message);
        }
    }
}
