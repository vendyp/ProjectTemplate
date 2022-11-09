using System.Text.Json.Serialization;
using BoilerPlate.Infrastructure;
using BoilerPlate.Shared.Infrastructure;
using BoilerPlate.Shared.Infrastructure.Auth;
using BoilerPlate.Shared.Infrastructure.Contexts;
using BoilerPlate.Shared.Infrastructure.Logging;
using BoilerPlate.Shared.Infrastructure.Security;
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

// begin services to the container.
builder.Services.AddSharedInfrastructure();
builder.Services.AddInfrastructure();
builder.Services.AddDefaultJsonSerialization();
builder.Services.AddClock();
builder.Services.AddMemoryRequestStorage();
builder.Services.AddContext();
builder.Services.AddApplicationInitializer();
builder.Services.AddLogging();
builder.Services.AddSecurity();
// end services to the container.

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddAuth(_ => { });
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
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.MapGet("/", context => context.Response.WriteAsync("Hello World!"));
app.Run();