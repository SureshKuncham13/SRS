using Microsoft.EntityFrameworkCore;
using SRS.Application.Interfaces.Repository;
using SRS.Application.Interfaces.Service;
using SRS.Application.Services;
using SRS.Infrastructure.Data;
using SRS.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<SrsDbContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("SrsConnection")));
builder.Services.AddDbContext<DevDbContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));

builder.Services.AddScoped<IBiometricUserLogRepository, BiometricUserLogRepository>();
builder.Services.AddScoped<IBiometricUserLogService, BiometricUserLogService>();

builder.Services.AddSingleton<IBiometricUserLogQueue, BiometricUserLogQueue>();
builder.Services.AddHostedService<BiometricUserLogBackgroundService>();


var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();