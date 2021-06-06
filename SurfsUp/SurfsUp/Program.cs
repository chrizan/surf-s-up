using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using SurfsUp.DataProvider.Contract;
using SurfsUp.DataProvider.Data;
using SurfsUp.SurfsUp.Jobs;
using SurfsUp.SurfsUp.Messengers;
using SurfsUp.SurfsUp.SwellAssessment;
using SurfsUp.SurfsUp.SwellAssessment.Strategies;

namespace SurfsUp.SurfsUp
{
    public static class Program
    {
        public static void Main(string[] args)
        {
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
                    services.AddQuartz(quartz =>
                    {
                        quartz.UseMicrosoftDependencyInjectionJobFactory();
                        services.AddTransient<IDataProvider, MswDataProvider>();
                        services.AddTransient<IMessenger, MailMessenger>();
                        services.AddTransient<IEvaluator, Evaluator>();
                        services.AddTransient<IStrategy, ItalyStrategy>();
                        services.AddTransient<IStrategy, FranceStrategy>();
                        services.AddTransient<IStrategy, SpainStrategy>();

                        var jobKey = new JobKey("daily0700");
                        quartz.AddJob<Job>(jobKey, j => j.WithDescription("Check Magic Seaweed"));

                        quartz.AddTrigger(trigger => trigger
                            .WithIdentity("Cron Trigger")
                            .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(7, 0))
                            .ForJob(jobKey)
                        );
                    });

                    services.AddQuartzHostedService(options =>
                    {
                        options.WaitForJobsToComplete = true;
                    });
                });
    }
}
