using Serilog;
using stockManagement.Application.Interfaces;
using stockManagement.Infrastructure;
using stockManagement.Infrastructure.ServiceImplementation;
using stockManagementClean.API.Mappers;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using static Serilog.Sinks.MSSqlServer.ColumnOptions;

var builder = WebApplication.CreateBuilder(args);

// Connection string to StockCleanDB
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Define custom column mappings


// Configure Serilog
//Log.Logger = new LoggerConfiguration()
//    .MinimumLevel.Debug()
//    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
//    .Enrich.FromLogContext()
//    .WriteTo.Console()
//    .WriteTo.MSSqlServer(
//        connectionString: connectionString,
//        sinkOptions: new MSSqlServerSinkOptions
//        {
//            TableName = "StockManagementLogs",
//            AutoCreateSqlTable = false // Ensure the table exists
//        },
//        columnOptions: new ColumnOptions
//        {
//            Store = { StandardColumn.Message, StandardColumn.MessageTemplate, StandardColumn.Level, StandardColumn.TimeStamp, StandardColumn.Exception, StandardColumn.Properties },
//            AdditionalColumns = new[]
//    {
//        // Example: Adding custom columns without conflicting names
//        //new SqlColumn { ColumnName = "CustomLogLevel", PropertyName = "Level", DataType = System.Data.SqlDbType.NVarChar },
//        new SqlColumn { ColumnName = "CustomLogProperties", PropertyName = "Properties", DataType = System.Data.SqlDbType.NVarChar }
//    }
//        })
//    .CreateLogger();

//builder.Host.UseSerilog();
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration) // Reads configuration from appsettings.json
    .CreateLogger();

// Add Serilog as the logging provider
builder.Host.UseSerilog();


// Add services to the container.
builder.Services.AddDbContext<StockDbContext>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddControllers();
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