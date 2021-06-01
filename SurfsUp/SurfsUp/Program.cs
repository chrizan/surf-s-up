using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using SurfsUp.DataProvider.Contract;
using SurfsUp.DataProvider.Data;
using SurfsUp.SurfsUp.Jobs;
using System;

namespace SurfsUp.SurfsUp
{
    public class Program
    {
        private static IConfiguration Configuration { get; set; }

        public static void Main(string[] args)
        {
            Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureServices(services =>
                {
                    //services.Configure<QuartzOptions>(Configuration.GetSection("Quartz"));

                    services.AddQuartz(q =>
                    {
                        q.UseMicrosoftDependencyInjectionJobFactory();

                        services.AddSingleton<IDataProvider, MswDataProvider>();

                        var jobKey = new JobKey("jobKeyName", "jobKeyGroup");
                        q.AddJob<Job>(jobKey, j => j.WithDescription("my awesome job"));

                        q.AddTrigger(t => t
                            .WithIdentity("Cron Trigger")
                            .ForJob(jobKey)
                            .StartAt(DateBuilder.EvenSecondDate(DateTimeOffset.UtcNow.AddSeconds(3)))
                            .WithCronSchedule("0/3 * * * * ?")
                            .WithDescription("my awesome cron trigger")
                        );
                    });

                    services.AddQuartzHostedService(options =>
                    {
                        options.WaitForJobsToComplete = true;
                    });
                });
    }
}
