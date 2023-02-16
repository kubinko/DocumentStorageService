using DocumentStorageService.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;

var currentAssembly = Assembly.GetExecutingAssembly();

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services
    .AddSingleton<IStorageService, InMemoryStorageService>()
    .AddMediatR(options => options.RegisterServicesFromAssembly(currentAssembly))
    .AddFluentValidationAutoValidation(options => options.DisableDataAnnotationsValidation = true)
    .AddFluentValidationClientsideAdapters()
    .AddValidatorsFromAssembly(currentAssembly);

var app = builder.Build();

app.MapControllers();

app.Run();
