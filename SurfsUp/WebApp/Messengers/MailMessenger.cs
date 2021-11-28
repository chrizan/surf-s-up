using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace SurfsUp.WebApp.Messengers
{
    public class MailMessenger : IMessenger
    {
        private readonly IHtmlMailBuilder _htmlMailBuilder;

        private string _from;
        private string _to;
        private string _server;
        private int _port;
        private string _password;

        public MailMessenger(IConfiguration configuration, IHtmlMailBuilder htmlMailBuilder)
        {
            ReadConfiguration(configuration);
            _htmlMailBuilder = htmlMailBuilder;
        }

        public Task SendMessage(List<Message> messages)
        {
            //Plain text message
            MailMessage mailMessage = new(_from, _to)
            {
                Subject = $"Surf's Up!",
                Body = $"{GetBody(messages)}"
            };

            //Html message
            ContentType mimeType = new("text/html");
            var alternateView = AlternateView.CreateAlternateViewFromString(_htmlMailBuilder.BuildHtmlMail(messages), mimeType);
            mailMessage.AlternateViews.Add(alternateView);

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
