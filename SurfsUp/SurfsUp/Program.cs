using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using SurfsUp.DataProvider.Contracts;
using SurfsUp.DataProvider.Data;
using SurfsUp.SurfsUp.Jobs;
using SurfsUp.SurfsUp.Messengers;
using SurfsUp.SurfsUp.SwellAssessment.Bafu;
using SurfsUp.SurfsUp.SwellAssessment.Msw;
using SurfsUp.SurfsUp.SwellAssessment.Strategies.Bafu;
using SurfsUp.SurfsUp.SwellAssessment.Strategies.Msw;

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
                        services.AddTransient<IMswDataProvider, MswDataProvider>();
                        services.AddTransient<IMswEvaluator, MswEvaluator>();
                        services.AddTransient<IMswStrategy, ItalyStrategy>();
                        services.AddTransient<IMswStrategy, FranceStrategy>();
                        services.AddTransient<IMswStrategy, SpainStrategy>();
                        services.AddTransient<IBafuDataProvider, BafuDataProvider>();
                        services.AddTransient<IBafuEvaluator, BafuEvaluator>();
                        services.AddTransient<IBafuStrategy, ReussStrategy>();
                        services.AddTransient<IBafuStrategy, BirsStrategy>();
                        services.AddTransient<IBafuStrategy, ThurStrategy>();
                        services.AddTransient<IMessenger, MailMessenger>();

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
