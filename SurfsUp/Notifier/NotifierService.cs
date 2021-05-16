using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SurfsUp.DataProvider.Contract;
using SurfsUp.DataProvider.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace SurfsUp.Notifier
{
    public class NotifierService : IHostedService, IDisposable
    {
        private readonly IDataProvider _dataProvider;

        private int executionCount = 0;
        private readonly ILogger<NotifierService> _logger;
        private Timer _timer;

        public NotifierService(ILogger<NotifierService> logger, IDataProvider dataProvider)
        {
            _logger = logger;
            _dataProvider = dataProvider;
        }

        ~NotifierService()
        {
            Dispose(false);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(NotifierService)} is running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            var count = Interlocked.Increment(ref executionCount);

            SwellData swellData = _dataProvider.GetSwellDataFromWeb("https://de.magicseaweed.com/Levanto-Surf-Report/3571/").Result;

            //SendMail();

            _logger.LogInformation($"{nameof(NotifierService)} is working. Count: {count}");
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

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(NotifierService)} is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _timer?.Dispose();
            }
        }
    }
}
