using Core.Contracts;
using Core.Exceptions;
using System;
using System.Net.Mail;

namespace Core.UseCases
{
    public class EmailNotifier : INotifier
    {
        private readonly string _host;
        private readonly string _password;
        private readonly string _sender;

        public EmailNotifier(string sender, string password, string host)
        {
            _host = host;
            _password = password;
            _sender = sender;
        }

        /// <exception cref="NotifierException"></exception>
        public void Notify(string recepient, string message)
        {
            var smtpClient = new SmtpClient(_host)
            {
                Port = 587,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new System.Net.NetworkCredential(_sender, _password),
                EnableSsl = true
            };

            try
            {
                var mail = new MailMessage(_sender.Trim(), recepient.Trim())
                {
                    Subject = "Order confirmation",
                    Body = message
                };
                smtpClient.Send(mail);
            }
            catch (Exception)
            {
                throw new NotifierException("Unable to send confirmation mail");
            }
        }
    }
}