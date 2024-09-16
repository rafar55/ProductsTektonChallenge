using Serilog;
using TektonChallenge.Core;
using TektonChallenge.Infrastructure;
using TektonChallenge.Infrastructure.Persistence;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting application");

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
        .ReadFrom.Configuration(hostingContext.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console());

    builder.Services
        .AddCore()
        .AddInfrastructure(builder.Configuration.GetConnectionString("DefaultConnection")!);

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    if (builder.Environment.IsDevelopment())
    {
        await DbInitializer.SeedAndRunMigrationsAsync(app);
    }

    app.Run();
    
    Log.Information("Application started");
}
//WHEN is to prevent know issue EF core when running migs 
//https://github.com/dotnet/efcore/issues/29923#issuecomment-2092619682
catch (Exception ex) when (ex.GetType().Name is not "StopTheHostException" &&
                           ex.GetType().Name is not "HostAbortedException" &&
                           ex.Source != "Microsoft.EntityFrameworkCore.Design")
{
    Log.Information("Error");
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.Information("Application shutting down");
    Log.CloseAndFlush();
}