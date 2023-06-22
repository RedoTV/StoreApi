using Microsoft.AspNetCore.Authentication.JwtBearer;
using ProductsApi.Data;
using ProductsApi.Services;

var builder = WebApplication.CreateBuilder(args);

string dbConnection = builder.Configuration.GetConnectionString("DbConnection")!;
builder.Services.AddSqlite<ProductsDbContext>(dbConnection);

builder.Services.AddTransient<IRepository,RepositoryService>();
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
