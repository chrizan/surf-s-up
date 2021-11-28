using SurfsUp.DataProvider.Contracts;
using SurfsUp.DataProvider.Providers.Bafu;
using SurfsUp.DataProvider.Providers.Msw;
using SurfsUp.Persistence.Contracts;
using SurfsUp.Persistence.Service;
using SurfsUp.WebApp.Messengers;
using SurfsUp.WebApp.SwellAssessment.Bafu;
using SurfsUp.WebApp.SwellAssessment.Msw;
using Quartz;
using SurfsUp.WebApp.QuartzJobs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddSingleton<IMswDataProvider, MswDataProvider>();
builder.Services.AddSingleton<IMswEvaluator, MswEvaluator>();
builder.Services.AddSingleton<IBafuDataProvider, BafuDataProvider>();
builder.Services.AddSingleton<IBafuEvaluator, BafuEvaluator>();
builder.Services.AddSingleton<IMessenger, MailMessenger>();
builder.Services.AddSingleton<IHtmlMailBuilder, HtmlMailBuilder>();
builder.Services.AddSingleton<IDatabaseService, DatabaseService>();

builder.Services.AddQuartz(quartz =>
{
    quartz.UseMicrosoftDependencyInjectionJobFactory();

    var jobKey = new JobKey("daily0700");
    quartz.AddJob<QuartzJob>(jobKey, j => j.WithDescription("Check Magic Seaweed"));
    quartz.AddTrigger(trigger => trigger
        .WithIdentity("Cron Trigger")
        //.WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(7, 0))
        .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(17, 44))
        .ForJob(jobKey)
    );
});

builder.Services.AddQuartzHostedService(options =>
{
    options.WaitForJobsToComplete = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
