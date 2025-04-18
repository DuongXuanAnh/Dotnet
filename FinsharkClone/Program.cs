using Microsoft.EntityFrameworkCore;
using FinsharkClone.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(10, 4, 11))));


var app = builder.Build();



app.MapGet("/", () => "Hello World!");


app.Run();
