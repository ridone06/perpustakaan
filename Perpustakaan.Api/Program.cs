using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Perpustakaan.Api.Data;
using Serilog;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Exceptions.EntityFrameworkCore.Destructurers;
using SerilogTimings;
using System;
using System.IO;

namespace Perpustakaan
{
#pragma warning disable CS1591
    public class Program
    {
        public static int Main(string[] args)
        {
            var ENVIRONMENT = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("logsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"logsettings.{ENVIRONMENT}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.WithProperty("Application", typeof(Program).Assembly.GetName().Name)
                .Enrich.WithProperty("Environment", ENVIRONMENT)
                .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder()
                    .WithDefaultDestructurers()
                    .WithDestructurers(new[] { new DbUpdateExceptionDestructurer() })
                ).CreateLogger();

            try
            {
                var host = CreateHostBuilder(args).Build();
                var config = host.Services.GetRequiredService<IConfiguration>();

                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var hostingEnvironment = services.GetService<IWebHostEnvironment>();
                    var Configuration = services.GetService<IConfiguration>();

                    using (var op = Operation.Begin("Seeding data..."))
                    {
                        SeedData.EnsureSeedData(services);

                        op.Complete();
                    }
                }

                Log.Information("Starting host Perpustakaan API...");
                host.Run();
                Log.Information("Perpustakaan API Started");

                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Perpustakaan API terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseSerilog();
    }
#pragma warning restore CS1591
}
