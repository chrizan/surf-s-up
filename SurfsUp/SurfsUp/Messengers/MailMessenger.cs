using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SurfsUp.SurfsUp.Messengers
{
    public class MailMessenger : IMessenger
    {
        private string _from;
        private string _to;
        private string _server;
        private int _port;
        private string _password;

        public MailMessenger(IConfiguration configuration)
        {
            ReadConfiguration(configuration);
        }

        public Task SendMessage(List<Message> messages)
        {
            MailMessage mailMessage = new(_from, _to)
            {
                Subject = $"Surf's Up!",
                Body = $"{GetBody(messages)}"
            };

            /*
            // Construct the alternate body as HTML.
            string body = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\">";
            body += "<HTML><HEAD><META http-equiv=Content-Type content=\"text/html; charset=iso-8859-1\">";
            body += "</HEAD><BODY><DIV><FONT face=Arial color=#ff0000 size=15>Hello from SurfsUp";
            body += "</FONT></DIV></BODY></HTML>";

            // Add the alternate body to the message.
            ContentType mimeType = new ContentType("text/html");
            AlternateView alternate = AlternateView.CreateAlternateViewFromString(body, mimeType);
            mailMessage.AlternateViews.Add(alternate);
            */

            SmtpClient client = new(_server)
            {
                Port = _port,
                Credentials = new NetworkCredential(_from, _password),
                EnableSsl = true
            };

            try
            {
                client.Send(mailMessage);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception caught in MailMessenger: {e}");
            }
            return Task.CompletedTask;
        }

        private void ReadConfiguration(IConfiguration configuration)
        {
            _from = configuration.GetSection("Mail").GetValue<string>("Sender");
            _to = configuration.GetSection("Mail").GetValue<string>("Recipient");
            _server = configuration.GetSection("Mail").GetValue<string>("Server");
            _port = configuration.GetSection("Mail").GetValue<int>("Port");
            _password = configuration.GetSection("Mail").GetValue<string>("Password");
        }

        private static string GetBody(List<Message> messages)
        {
            StringBuilder stringBuilder = new();
            foreach (var message in messages)
            {
                stringBuilder.Append(message.GetRepresentation());
                stringBuilder.Append(Environment.NewLine);
                stringBuilder.Append(Environment.NewLine);
            }
            return stringBuilder.ToString();
        }
    }
}
