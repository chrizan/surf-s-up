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
                    services.AddSingleton<IMswDataProvider, MswDataProvider>();
                    services.AddSingleton<IMswEvaluator, MswEvaluator>();
                    services.AddSingleton<IMswStrategy, ItalyStrategy>();
                    services.AddSingleton<IMswStrategy, FranceStrategy>();
                    services.AddSingleton<IMswStrategy, SpainStrategy>();
                    services.AddSingleton<IBafuDataProvider, BafuDataProvider>();
                    services.AddSingleton<IBafuEvaluator, BafuEvaluator>();
                    services.AddSingleton<IBafuStrategy, ReussStrategy>();
                    services.AddSingleton<IBafuStrategy, BirsStrategy>();
                    services.AddSingleton<IBafuStrategy, ThurStrategy>();
                    services.AddSingleton<IMessenger, MailMessenger>();
                    services.AddSingleton<IHtmlMailBuilder, HtmlMailBuilder>();

                    services.AddQuartz(quartz =>
                    {
                        quartz.UseMicrosoftDependencyInjectionJobFactory();

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
