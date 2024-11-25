using dotenv.net;
using Microsoft.EntityFrameworkCore;
using PaymentsPlanner.Models;
using PaymentsPlanner.Services;

var builder = WebApplication.CreateBuilder(args);

DotEnv.Load();

// Add services to the container.

string databaseConnection = Environment.GetEnvironmentVariable("DATABASE_URL")!;

var MyAllowOrigins = "_myAllowOrigins";

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<PaymentService>();
builder.Services.AddScoped<PaymentTypeService>();
builder.Services.AddScoped<ResetPaymentService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowOrigins, policy =>
    {
        policy.WithOrigins("http://localhost:4200", "https://zealous-hill-0cd894a1e.5.azurestaticapps.net")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(databaseConnection);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseCors(MyAllowOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
