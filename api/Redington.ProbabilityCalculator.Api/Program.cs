using Redington.ProbabilityCalculator.Api.Extensions;
using Serilog;

namespace Redington.ProbabilityCalculator.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
            
        builder.Services.AddApplicationServices();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddCors(options =>
        {
            // Will work only in development
            // Still it is discouraged and would like to use the below
            // policy.WithOrigins("http://localhost:5173") // 5173: vite port
            // However allowed all origins for coding test purpose
            options.AddPolicy("AllowReact", policy =>
            {
                policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });

            options.AddPolicy("Production", policy =>
            {
                policy.WithOrigins("https://redington.co.uk/:3000", "https://redington.co.uk/:5173")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        Log.Logger = new LoggerConfiguration()
            .WriteTo.File("logs/probability-calculator.txt", rollingInterval: RollingInterval.Day)
            .WriteTo.Console()
            .CreateLogger();

        builder.Host.UseSerilog();

        var app = builder.Build();
            
        app.UseGlobalExceptionHandler();
            
        app.UseHttpsRedirection();

        if (app.Environment.IsDevelopment())
        {
            app.UseCors("AllowReact");
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else
        {
            app.UseCors("Production");
        }

        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}