using Application;
using Domain;
using eshop.api.Common.Errors;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Persistence;

var builder = WebApplication.CreateBuilder(args);
{

    builder.Services.AddSingleton<ProblemDetailsFactory, EShopProblemDetailsFactory>();

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddDomain();
    builder.Services.AddApplication();
    builder.Services.AddPersistence(builder.Configuration);
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
