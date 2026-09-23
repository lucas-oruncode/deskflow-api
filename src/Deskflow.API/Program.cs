using Deskflow.API.Data;
using Deskflow.API.Repositories;
using Deskflow.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
}


app.Run();