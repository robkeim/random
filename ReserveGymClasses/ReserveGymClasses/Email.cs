﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;

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

        public static void SendEmail(string recipient, string subject, string body, string[] screenshotPaths = null)
        {
            SendEmail(new[] { recipient }, subject, body, screenshotPaths);
        }

        public static void SendEmail(IEnumerable<string> recipientsRaw, string subject, string body, string[] screenshotPaths = null)
        {
            var recipients = recipientsRaw.OrderBy(email => email).Distinct().ToList();
            
            lock (lockObj)
            {
                Console.WriteLine("--------------------------------------------------------------");
                Console.WriteLine("To: {0}", string.Join(",", recipients));
                Console.WriteLine("Subject: {0}", subject);
                Console.WriteLine("Body:");
                Console.WriteLine(body?.Replace("<br />", "\n")?.Replace("<b>", "")?.Replace("</b>", "")?.Replace("&nbsp;", ""));
                Console.WriteLine("--------------------------------------------------------------");
            }

            if (Program.EmailStatus != EmailStatus.Disabled)
            {
                body = body.Replace("\n", "<br />").Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;");

                if (Program.EmailStatus == EmailStatus.OnlyToMe)
                {
                    recipients = new List<string> { "robkeim@gmail.com" };
                }

                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("robkeim@gmail.com", ConfigurationManager.AppSettings["GmailPassword"]),
                    EnableSsl = true
                };

                var attachments = new List<Attachment>();

                using (client)
                {
                    var message = new MailMessage
                    {
                        From = new MailAddress("robkeim@gmail.com"),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true
                    };

                    if (screenshotPaths != null)
                    {
                        foreach (var path in screenshotPaths)
                        {
                            var attachment = new Attachment(path);
                            attachments.Add(attachment);

                            message.Attachments.Add(attachment);
                        };
                    }

                    foreach (var email in recipients)
                    {
                        message.To.Add(new MailAddress(email));
                    }

                    try
                    {
                        if (!client.SendMailAsync(message).Wait((int)TimeSpan.FromSeconds(30).TotalMilliseconds))
                        {
                            throw new TimeoutException("Sending email didn't complete");
                        }
                    }
                    finally
                    {
                        foreach (var attachment in attachments)
                        {
                            attachment.Dispose();
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Email sending disabled");
            }
        }
    }
}
