
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using VolgaIt.Domain;
using VolgaIt.Domain.Entities;
using VolgaIt.Domain.Repositories.Abstract;
using VolgaIt.Domain.Repositories.EntityFramework;
using VolgaIt.Mapper;
using VolgaIt.Service;
using VolgaIt.Service.Interfaces;

namespace VolgaIt
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.Bind("Config", new Config());

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            });
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddTransient<ITransportRepository, EFTrasportsRepository>();
            builder.Services.AddTransient<ITransportTypeRepository, EFTransportTypeRepository>();
            builder.Services.AddTransient<IDatabaseService, DatabaseService>();
            builder.Services.AddTransient<IRentRepository, EFRentRepository>();
            builder.Services.AddTransient<DataManager>();

            builder.Services.AddScoped<IJwtGenerator, JwtGenerator>();

            builder.Services.AddIdentity<AppUser, IdentityRole>(setup => { })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();


            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(Config.ConnectionString));

            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        ValidateAudience = false,
                        ValidateIssuer = false,
                    };
                });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }

    public class AuthOptions
    {
        public const string ISSUER = "CSEON"; // издатель токена
        public const string AUDIENCE = "VolgaItClients"; // потребитель токена
        const string KEY = "mysupersecret_secretkey!123";   // ключ для шифрации
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new(Encoding.UTF8.GetBytes(KEY));
    }
}
//TODO: Протеститировать на наличие багов при нестандартных значениях аргуметов.