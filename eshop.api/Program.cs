using Application;
using Domain;
using eshop.api.Common.Errors;
using Infrastructure.BackgroundJobs;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Persistence;
using Quartz;

var builder = WebApplication.CreateBuilder(args);
{

    builder.Services.AddSingleton<ProblemDetailsFactory, EShopProblemDetailsFactory>();

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddDomain();
    builder.Services.AddApplication();
    builder.Services.AddPersistence(builder.Configuration);

    builder.Services.AddQuartz(config =>
    {
        var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));

        config
            .AddJob<ProcessOutboxMessagesJob>(jobKey)
            .AddTrigger(trigger =>
                trigger.ForJob(jobKey)
                       .WithSimpleSchedule(schedule =>
                            schedule.WithIntervalInSeconds(10)
                            .RepeatForever()));

        config.UseMicrosoftDependencyInjectionJobFactory();

    });

    builder.Services.AddQuartzHostedService();
}


var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseExceptionHandler("/error");

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
