using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ReserveGymClasses
{
    public enum EmailStatus
    {
        Enabled,
        OnlyToMe,
        Disabled
    }

    public static class Email
    {
        private static readonly object lockObj = new object();

        private static Task SendEmailAsync(string email, string subject, string body)
        {
            return SendEmailAsync(new[] { email }, subject, body);
        }

        public static Task SendEmailAsync(IEnumerable<string> rawTo, string subject, string body)
        {
            var to = rawTo.OrderBy(email => email).Distinct().ToList();
            
            lock (lockObj)
            {
                Console.WriteLine("--------------------------------------------------------------");
                Console.WriteLine("To: {0}", string.Join(",", to));
                Console.WriteLine("Subject: {0}", subject);
                Console.WriteLine("Body:");
                Console.WriteLine(body?.Replace("<br />", "\n")?.Replace("<b>", "")?.Replace("</b>", "")?.Replace("&nbsp;", ""));
                Console.WriteLine("--------------------------------------------------------------");
            }

            if (Program.EmailStatus != EmailStatus.Disabled)
            {
                if (Program.EmailStatus == EmailStatus.OnlyToMe)
                {
                    to = new List<string> { "robkeim@gmail.com" };
                }

                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("robkeim@gmail.com", ConfigurationManager.AppSettings["GmailPassword"]),
                    EnableSsl = true
                };

                var message = new MailMessage
                {
                    From = new MailAddress("robkeim@gmail.com"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                foreach (var email in to)
                {
                    message.To.Add(new MailAddress(email));
                }

                return client.SendMailAsync(message);
            }
            else
            {
                Console.WriteLine("Email sending disabled");
            }

            return Task.CompletedTask;
        }
    }
}
