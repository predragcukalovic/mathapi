using Papi.GameServer.Math.ApiCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSystemWebAdapters();
builder.Services.AddHttpForwarder();

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.RegisterLogger();

// Add services to the container.
builder.Services.RegisterServices(builder);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseSystemWebAdapters();

app.MapControllers();

app.Run();

