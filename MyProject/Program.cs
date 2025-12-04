using Microsoft.EntityFrameworkCore;
using MyProject.AppLogic.UseCasesImplementation.Users;
using MyProject.AppLogic.UseCasesInterfaces.Users;
using MyProject.Data.Repos.EF;
using UsersAPI.BusinessLogic.ReposInterfaces;
using UsersAPI.Data.Repos.EF;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Context>(options => options.UseSqlServer
    (builder.Configuration.GetConnectionString("DefaultConnectionString")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICreateUser, CreateUser>();

var app = builder.Build();

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
