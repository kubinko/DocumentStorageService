using DocumentStorageService.CustomFormatters;
using DocumentStorageService.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Caching.Memory;
using System.Reflection;

string cacheOptionSetting = "CacheSettings";

var currentAssembly = Assembly.GetExecutingAssembly();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.RespectBrowserAcceptHeader = true;

    options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
    options.OutputFormatters.Add(new MessagePackFormatter());
});

builder.Services
    .AddOptions<MemoryCacheEntryOptions>(cacheOptionSetting);

builder.Services
    .AddMemoryCache()
    .AddSingleton<ICacheService, CacheService>()
    .AddSingleton<IStorageService, InMemoryStorageService>()
    .AddMediatR(options => options.RegisterServicesFromAssembly(currentAssembly))
    .AddFluentValidationAutoValidation(options => options.DisableDataAnnotationsValidation = true)
    .AddFluentValidationClientsideAdapters()
    .AddValidatorsFromAssembly(currentAssembly);

var app = builder.Build();

app.MapControllers();

app.Run();
