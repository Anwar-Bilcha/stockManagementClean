using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using stockManagement.Application.Interfaces;
using stockManagement.Infrastructure;
using stockManagement.Infrastructure.ServiceImplementation;
using stockManagementClean.API.Mapper;
using stockManagementClean.API.utilities;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<StockDbContext>();
builder.Services.AddTransient<IProductService, ProductService>();
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
            ClockSkew = TimeSpan.Zero // Set ClockSkew to zero for better
        };
    });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ITokenGenerator, JwtTokenGenerator>();
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
