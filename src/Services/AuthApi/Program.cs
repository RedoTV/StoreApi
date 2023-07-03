using AuthApi.Data;
using AuthApi.Services.Implementations;
using AuthApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

string dbConnection = builder.Configuration.GetConnectionString("DbConnection")!;

builder.Services.AddTransient<IIdentityService,IdentityService>();
builder.Services.AddSqlite<IdentityDbContext>(builder.Configuration.GetConnectionString("DbConnection"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
