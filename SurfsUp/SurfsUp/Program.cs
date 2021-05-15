using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SurfsUp.Notifier.Contract;
using SurfsUp.Notifier.Notifier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurfsUp.SurfsUp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            StartNotifier();
            CreateHostBuilder(args).Build().Run();
        }

        private static void StartNotifier()
        {
            INotifier notifier = new MswNotifier();
            notifier.Start();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
