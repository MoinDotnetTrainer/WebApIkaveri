
using Asp.Versioning;
using Microsoft.EntityFrameworkCore;
using WebApIkaveri.Models;

using Microsoft.AspNetCore.Authentication.JwtBearer;


using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace WebApIkaveri
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AppDb>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });


            //api versioning dependency
            builder.Services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true; // if no version is specied the application will exe default method 
                options.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0); // if verison id specid this will execute
                options.ReportApiVersions = true;  // responce header inluces supported version
            }).AddMvc(o =>
            {
                // Change this line:
                // o.Conventions.Add(new VersionByNamespaceConvention()); //version controller based on their namespace

                // To this:
                o.Conventions.Add(new Asp.Versioning.Conventions.VersionByNamespaceConvention()); // version controller based on their namespace

            }).AddApiExplorer(x =>
            {
                x.GroupNameFormat = "'v'V";
                x.SubstituteApiVersionInUrl = true; // this helpful for swagger
            });


            //jwt
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });


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
        }
    }
}
