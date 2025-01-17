using Serilog;
using stockManagement.Application.Interfaces;
using stockManagement.Infrastructure;
using stockManagement.Infrastructure.ServiceImplementation;
using stockManagementClean.API.Mappers;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using static Serilog.Sinks.MSSqlServer.ColumnOptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using stockManagementClean.API.Utilities.JwtUtility;
using Microsoft.Extensions.Logging;
using stockManagementClean.API.Utilities;

var builder = WebApplication.CreateBuilder(args);



Log.Logger = new LoggerConfiguration()
    .WriteTo.File(
        path: "Logs/log-.txt",
        rollingInterval: RollingInterval.Day,
        outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss}] [{Level:u3}] {Message:lj}{NewLine}{Exception}"
    )
    .CreateLogger();

// Replace the default logging provider with Serilog
builder.Logging.ClearProviders();
builder.Logging.AddSerilog();




builder.Services.AddCors(options =>
{
    options.AddPolicy("StockManagementCORSPolicy", policy =>
    {
        policy.WithOrigins("https://localhost:7287/") // Allow only this specific origin
              .WithHeaders("Accept", "Authorization", "x-api-version") // Allow the client to use the listed headers
              .WithMethods("Get","Post"); // Allow GET, POST, PUT, DELETE, etc.
    });

    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin() // Allow any origin
              .WithHeaders("Accept")
              .WithMethods("GET");
    });
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ClockSkew = TimeSpan.Zero // Set ClockSkew to zero for better security
        };
    });

//Log.Logger = new LoggerConfiguration()
//    .ReadFrom.Configuration(builder.Configuration.GetSection("Serilog")) // Reads configuration from appsettings.json
//    .CreateLogger();

// Add Serilog as the logging provider
builder.Host.UseSerilog();
builder.Services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();


// Add services to the container.
builder.Services.AddDbContext<StockDbContext>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddControllers(options => {
    options.Filters.Add<GlobalLoggingFilter>();
 }
);
builder.Services.AddSingleton<GlobalModelValidator>();
//Log.Logger = new LoggerConfiguration()
//    .WriteTo.File("Logs/StockLog-{Date}.txt", rollingInterval: RollingInterval.Day)
//    .CreateLogger();
//builder.Host.UseSerilog();


// Configure Serilog

// ... rest of your Program.cs


builder.Services.AddAutoMapper(typeof(StockManagementMapper),typeof(Program));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
//app.UseSerilogRequestLogging();
app.UseCors("StockManagementCORSPolicy");
app.UseCors("AllowAnyOrigins");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

try
{
    Log.Logger.Information("Starting the application...");
    app.Run();
}
catch (Exception ex)
{
    Log.Logger.Fatal(ex, "The application failed to start correctly.");
    throw;
}
finally
{
    Log.CloseAndFlush(); // Ensure all logs are flushed before exiting
}