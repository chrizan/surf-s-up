using SurfsUp.DataProvider.Contract;
using SurfsUp.DataProvider.Data;
using SurfsUp.DataProvider.Models;
using SurfsUp.Notifier.Contract;
using System;
using System.Net;
using System.Net.Mail;
using System.Timers;

namespace SurfsUp.Notifier.Notifier
{
    public class MswNotifier : INotifier
    {
        private readonly Timer timer = new Timer(5000);

        public void Start()
        {
            timer.Elapsed += OnTimerElapsed;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private void OnTimerElapsed(object source, ElapsedEventArgs e)
        {
            IDataProvider dataProvider = new MswDataProvider();
            SwellData swellData = dataProvider.GetSwellDataFromWeb("https://de.magicseaweed.com/Levanto-Surf-Report/3571/").Result;

            //Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",
            //                  e.SignalTime);
            SendMail();


        }

        private void SendMail()
        {
            string to = "christophzanner@gmx.ch";
            string from = "christophzanner@gmx.ch";
            string server = "mail.gmx.net";

            MailMessage message = new MailMessage(from, to);
            message.Subject = "Using the new SMTP client.";
            message.Body = @"Using this new feature, you can send an email message from an application very easily.";
            SmtpClient client = new SmtpClient(server)
            {
                Port = 587,
                Credentials = new NetworkCredential("christophzanner@gmx.ch", "u7i8bla!"),
                EnableSsl = true
            };
            // Credentials are necessary if the server requires the client
            // to authenticate before it will send email on the client's behalf.
            //client.UseDefaultCredentials = true;

            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in CreateTestMessage2(): {0}", ex.ToString());
            }
        }
    }
}
