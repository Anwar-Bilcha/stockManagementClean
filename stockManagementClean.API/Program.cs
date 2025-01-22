using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using stockManagement.Application.Interfaces;
using stockManagement.Infrastructure;
using stockManagement.Infrastructure.ServiceImplementation;
using stockManagementClean.API.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<StockDbContext>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddAutoMapper(typeof(StockMapper), typeof(Program));
builder.Services.AddCors(options =>
{
options.AddPolicy("StockCorsPolicy",
        builder => builder
            .WithOrigins("https://localhost:7287.com")
            .WithMethods("GET", "PUT") // Specify allowed methods
            .WithHeaders("Authorization", "Content-Type"));
    options.AddPolicy("AllowAllOrigin",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod() // Specify allowed methods
            .AllowAnyHeader());
});

var app = builder.Build();
app.UseCors("StockCorsPolicy");
app.UseCors("AllowAllOrigin");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
