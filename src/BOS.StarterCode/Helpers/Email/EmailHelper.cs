using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;

namespace BOS.StarterCode.Helpers
{
    public class EmailHelper
    {
        public void SendEmail(string toEmailAddress, Dictionary<string, string> parameters)
        {
            var apiKey = Environment.GetEnvironmentVariable("Oywh04EKTQaq5NYiIh0G_w");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("startercode@bosframework.com", "BOS Team");
            var subject = parameters["Subject"];
            var to = new EmailAddress("toEmailAddress", "Example User");
            // var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = FetchTemplate(parameters);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlContent);
            var response = client.SendEmailAsync(msg);
        }

        private string FetchTemplate(Dictionary<string, string> parameters)
        {
            string fileName = string.Empty;
            String template = string.Empty;
            switch (parameters["Action"])
            {
                case "Registration":
                    fileName = "register.html";
                    break;
                case "ForgotPassword":
                    fileName = "forgotpassword.html";
                    break;
                default:
                    fileName = "";
                    break;
            }
            string filePath = Directory.GetCurrentDirectory();

            using (StreamReader sr = new StreamReader(filePath + "/Helpers/Email/Templates/" + fileName))
            {
                // Read the stream to a string, and write the string to the console.
                template = sr.ReadToEnd();
                Console.WriteLine(template);
            }
            UpdateTemplateValues(ref parameters, ref template);
            return template;
        }

        private void UpdateTemplateValues(ref Dictionary<string, string> parameters, ref string template)
        {
            template = template.Replace("{ApplicationURL}", "http://bosframework.com");
            template = template.Replace("{FromAddress}", parameters["From"]);
            switch (parameters["Action"])
            {
                case "Registration":
                    template = template.Replace("{Name}", parameters["Name"]);
                    break;
                case "ForgotPassword":
                    template = template.Replace("{Password}", parameters["Password"]);
                    break;
                default:
                    break;
            }
        }
    }
}
