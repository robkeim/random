//-----------------------------------------------------------------------
// <copyright file="Helpers.cs" company="RobKeim">
//     Rob Keim Corporation
// </copyright>
//-----------------------------------------------------------------------
namespace Robkeim.BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Mail;
    using Robkeim.Models;

    /// <summary>
    /// This is a helpers functions class
    /// </summary>
    public static class Helpers
    {
        /// <summary>
        /// Email addresses for each family member
        /// </summary>
        private static Dictionary<Participant, string> EmailAddresses = new Dictionary<Participant, string>()
        {
            // All participates listed here
            { Participant.Rob_Keim, "myemail@gmail.com" },
        };

        /// <summary>
        /// Get the email addresses for a list of participants
        /// </summary>
        /// <param name="participants">The participants to get the email addresses for</param>
        /// <returns>List of email addresses</returns>
        public static List<string> GetEmailAddresses(Participant[] participants)
        {
            List<string> emailAddresses = new List<string>();

            foreach (var participant in participants)
            {
                emailAddresses.Add(GetEmailAddress(participant));
            }

            return emailAddresses;
        }

        /// <summary>
        /// Get the email address for a participant
        /// </summary>
        /// <param name="participant">The participant to get the email address for</param>
        /// <returns>Email address</returns>
        public static string GetEmailAddress(Participant participant)
        {
            return EmailAddresses[participant];
        }

        /// <summary>
        /// Gets the enum value for the person from their name
        /// </summary>
        /// <param name="name">The person's name</param>
        /// <returns>The person's id in the enum</returns>
        public static int GetEnumValueFromName(string name)
        {
            name = name.Replace(' ', '_');
            return (int)Enum.Parse(typeof(Participant), name);
        }

        /// <summary>
        /// Returns the person's name from the enum value
        /// </summary>
        /// <param name="value">The person's enum value</param>
        /// <returns>Person's name</returns>
        public static string GetNameFromEnumValue(int value)
        {
            string name = ((Participant)value).ToString();
            name = name.Replace('_', ' ');

            return name;
        }

        /// <summary>
        /// Sends emails on successful registration
        /// </summary>
        /// <param name="person">The person who registered</param>
        /// <param name="update">True if this is an updated registration</param>
        /// <param name="participating">Bool indicating if they're participating</param>
        /// <param name="addressLine1">First line of their address</param>
        /// <param name="addressLine2">Second line of their address</param>
        public static void SendEmails(string person, bool update, bool participating, string addressLine1, string addressLine2)
        {
            string mobileMessageFormat = "Person: {0}\n" + 
                                            "Update: {1}\n" +
                                            "Participating: {2}";
            string mobileMessage = string.Format(mobileMessageFormat, person, update ? "Yes" : "No", participating ? "Yes" : "No");
            SendEmail(string.Empty, mobileMessage, "myphonenumber@vtext.com");

            string messageFormat = "Person: {0}\n" +
                                    "Update: {1}\n" +
                                    "Participating: {2}";

            if (participating)
            {
                messageFormat += "\nAddress1: {3}\n" +
                                    "Address2: {4}\n";
            }

            string message = string.Format(messageFormat, person, update ? "Yes" : "No", participating ? "Yes" : "No", addressLine1, addressLine2);
            SendEmail("[CoE] Registration", message, "myemail@gmail.com");                                 
        }

        /// <summary>
        /// Send an email
        /// </summary>
        /// <param name="title">The title of the email</param>
        /// <param name="message">The message of the email</param>
        /// <param name="destination">The destination to send the message</param>
        public static void SendEmail(string title, string message, string destination)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("myemail@gmail.com", DecodeFrom64("mypassword")),
                EnableSsl = true
            };

            if (System.Configuration.ConfigurationManager.AppSettings["SendEmails"] == "true")
            {
                client.Send("myemail@gmail.com", destination, title, message);
            }
        }

        /// <summary>
        /// Base64 encode a string
        /// </summary>
        /// <param name="toEncode">String to encode</param>
        /// <returns>Encoded string</returns>
        public static string EncodeTo64(string toEncode)
        {
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
            return System.Convert.ToBase64String(toEncodeAsBytes);
        }

        /// <summary>
        /// Base64 decode a string
        /// </summary>
        /// <param name="encodedData">String to decode</param>
        /// <returns>Decoded string</returns>
        public static string DecodeFrom64(string encodedData)
        {
            byte[] encodedDataAsBytes = System.Convert.FromBase64String(encodedData);
            return System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
        }
    }
}