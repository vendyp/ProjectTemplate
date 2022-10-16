using System.Text.Json.Serialization;
using BoilerPlate.Infrastructure;
using BoilerPlate.Shared.Infrastructure;
using BoilerPlate.Shared.Infrastructure.Auth;
using BoilerPlate.Shared.Infrastructure.Contexts;
using BoilerPlate.Shared.Infrastructure.Logging;
using BoilerPlate.Shared.Infrastructure.Serialization;
using BoilerPlate.Shared.Infrastructure.Serialization.SystemTextJson;
using BoilerPlate.Shared.Infrastructure.Services;
using BoilerPlate.Shared.Infrastructure.Storage;
using BoilerPlate.Shared.Infrastructure.Swaggers;
using BoilerPlate.Shared.Infrastructure.Time;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseLogging();

// Add services to the container.
builder.Services.AddSharedInfrastructure();
builder.Services.AddInfrastructure();
builder.Services.AddDefaultJsonSerialization();
builder.Services.AddClock();
builder.Services.AddMemoryRequestStorage();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddAuth(_ => { });
builder.Services.AddContext();
builder.Services.AddApplicationInitializer();
builder.Services.AddLogging();
builder.Services.AddSwaggerGen2();
builder.Services.AddControllers(options =>
    {
        options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.Converters.Add(new DateOnlyConverter());
        options.JsonSerializerOptions.Converters.Add(new TimeOnlyConverter());
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwaggerGenAndReDoc();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});
app.UseCors("cors");
app.UseSharedInfrastructure();
app.UseCustomExceptionHandler();
app.UseAuth();
app.UseContext();
app.UseLogging();
//app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapGet("/", context => context.Response.WriteAsync("Hello World!"));
});

app.Run();