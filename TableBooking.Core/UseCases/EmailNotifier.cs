using Core.Contracts;
using Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Text;

namespace Core.UseCases
{
    public class EmailNotifier : INotifier
    {
        private string _host;
        private string _password;
        private string _sender;

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
                var mail = new MailMessage(_sender.Trim(), recepient.Trim());
                mail.Subject = "Order confirmation";
                mail.Body = message;
                smtpClient.Send(mail);
            }
            catch (Exception)
            {
                throw new NotifierException("Unable to send confirmation mail");
            }
        }
    }
}