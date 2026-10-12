using SRS.Application.Interfaces.Repository;
using SRS.Application.Services;
using SRS.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IBiometricUserLogSyncRepository, BiometricUserLogSyncRepository>();
builder.Services.AddHostedService<BiometricUserLogBackgroundService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();