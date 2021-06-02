using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace SurfsUp.SurfsUp.Messengers
{
    public class MailMessenger : IMessenger
    {
        private IConfiguration Configuration { get; }

        public MailMessenger(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Task SendMessage(string spotName, string spotUrl, ISet<string> dates)
        {
            string from = Configuration.GetSection("Mail").GetValue<string>("Sender");
            string to = Configuration.GetSection("Mail").GetValue<string>("Recipient");
            MailMessage message = new MailMessage(from, to)
            {
                Subject = $"{spotName} is firing!",
                Body = $"Checkout {spotUrl}"
            };

            // Construct the alternate body as HTML.
            string body = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\">";
            body += "<HTML><HEAD><META http-equiv=Content-Type content=\"text/html; charset=iso-8859-1\">";
            body += "</HEAD><BODY><DIV><FONT face=Arial color=#ff0000 size=15>Hello from SurfsUp";
            body += "</FONT></DIV></BODY></HTML>";

            // Add the alternate body to the message.
            ContentType mimeType = new ContentType("text/html");
            AlternateView alternate = AlternateView.CreateAlternateViewFromString(body, mimeType);
            message.AlternateViews.Add(alternate);

            SmtpClient client = new SmtpClient(Configuration.GetSection("Mail").GetValue<string>("Server"))
            {
                Port = Configuration.GetSection("Mail").GetValue<int>("Port"),
                Credentials = new NetworkCredential(Configuration.GetSection("Mail").GetValue<string>("Sender"), Configuration.GetSection("Mail").GetValue<string>("Password")),
                EnableSsl = true
            };

            try
            {
                //client.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in CreateTestMessage2(): {0}", ex.ToString());
            }
            return Task.CompletedTask;
        }
    }
}
