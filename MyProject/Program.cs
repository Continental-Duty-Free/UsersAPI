using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using UsersAPI.AppLogic.UseCasesImplementation.Users;
using UsersAPI.AppLogic.UseCasesInterfaces.Users;
using UsersAPI.Data.Repos.EF;
using System.Text;
using UsersAPI.AppLogic.UseCasesImplementation.Customers;
using UsersAPI.AppLogic.UseCasesInterfaces.Customers;
using UsersAPI.Domain.Entitys;
using UsersAPI.Domain.ReposInterfaces;
using UsersAPI.Endpoints;
using UsersAPI.Middlewares;
using MyProject.Data.Repos.EF;

public partial class Program
{
    private static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer(); 
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "UsersAPI", Version = "v1" });
        });

        builder.Services.AddDbContext<Context>(options => options.UseSqlServer
            (builder.Configuration.GetConnectionString("DefaultConnectionString")));

        builder.Services.AddScoped<IUserRepository, UserRepository>();

        builder.Services.AddScoped<ICreateCustomer, CreateCustomer>();
        builder.Services.AddScoped<ILogin, Login>();

        builder.Services.AddScoped<Token>();

        builder.Services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
            };
        });

        builder.Services.AddAuthorization();

        var app = builder.Build();

        app.UseGlobalExceptionHandler();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "UsersAPI V1");
                c.RoutePrefix = "swagger";
            });
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapUsersEndPoints();
        app.MapCustomersEndPoints();
        app.UseMiddleware<GlobalExceptionHandler>();

        app.Run();
    }
}
