using Employee_Mgmt_Back.Data;
using Employee_Mgmt_Back.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;    
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Employee_Mgmt_Back.Context
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Log.Logger = new LoggerConfiguration()
            //    .ReadFrom.Configuration(builder.Configuration)
            //    .CreateLogger();

            //builder.Host.UseSerilog();

            builder.Services.AddDbContext<EmpDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ConnStr")));

            // Add book service
            builder.Services.AddScoped<AuthService>();
            builder.Services.AddScoped<JwtService>();



            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //add Cors 
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("Employee-Management-System", policy =>
                {
                    policy.WithOrigins("http://localhost:4200")
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials();
                });
            });

            // JWT configuration
            var jwtSettings = builder.Configuration.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    ClockSkew = TimeSpan.Zero
                };
            });


            var app = builder.Build();

            //app.UseSerilogRequestLogging();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("Employee-Management-System");

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<EmpDbContext>();
                //context.EnsureAdmin();
            }

            app.Run();
        }
    }
}

















    //    {
    //        var builder = WebApplication.CreateBuilder(args);

    //       // Log.Logger = new LoggerConfiguration()
    //       //.ReadFrom.Configuration(builder.Configuration)
    //       //.CreateLogger();

    //       // builder.Host.UseSerilog();

            

    //        // ✅ Register EF Core
    //        builder.Services.AddDbContext<EmpDbContext>(options =>
    //            options.UseSqlServer(builder.Configuration.GetConnectionString("ConnStr")));

    //        // ✅ Register JwtService (for generating tokens)
    //        builder.Services.AddScoped<JwtService>();
    //        builder.Services.AddScoped<AuthService>();


    //        // Add services to the container.
    //        builder.Services.AddControllers();
    //        // ✅ Swagger setup
    //        builder.Services.AddEndpointsApiExplorer();
    //        builder.Services.AddSwaggerGen();


    //        // ✅ Configure JWT Authentication
    //        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    //            .AddJwtBearer(options =>
    //            {
    //                options.TokenValidationParameters = new TokenValidationParameters
    //                {
    //                    ValidateIssuer = true,
    //                    ValidateAudience = true,
    //                    ValidateLifetime = true,
    //                    ValidateIssuerSigningKey = true,
    //                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
    //                    ValidAudience = builder.Configuration["Jwt:Audience"],
    //                    IssuerSigningKey = new SymmetricSecurityKey(
    //                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? "super_secret_key_123!"))
    //                };
    //            });


    //        // ✅ Allow Angular Frontend (CORS)
    //        builder.Services.AddCors(options =>
    //        {
    //            options.AddPolicy("AllowAngularApp", policy =>
    //                policy.WithOrigins("https://localhost:4200") // Angular default port
    //                      .AllowAnyHeader()
    //                      .AllowAnyMethod()
    //                      .AllowCredentials());
    //        });

    //        // ✅ Add Authorization
    //        builder.Services.AddAuthorization();
    //        var app = builder.Build();

    //        app.UseCors("AllowAngularApp");


    //        // ✅ Configure the HTTP request pipeline.
    //        if (app.Environment.IsDevelopment())
    //        {
    //            app.UseSwagger();
    //            app.UseSwaggerUI();
    //        }

    //        app.UseHttpsRedirection();

    //        // ✅ Authentication must come before Authorization
    //        app.UseAuthentication();
    //        app.UseAuthorization();

    //        app.MapControllers();

    //        app.Run();
    //    }
    //}
//}














/*

using Employee_Mgmt_Back.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// ✅ Configure EF Core
builder.Services.AddDbContext<EmpDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnStr")));

// ✅ Configure JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// ✅ Add Authorization
builder.Services.AddAuthorization();

// ✅ Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ✅ Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ✅ Important: Authentication must come BEFORE Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();












/*
using Employee_Mgmt_Back.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<EmpDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnStr")));
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
app.UseAuthentication();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
*/






/*
using Employee_Mgmt_Back.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ----------------------------------------------------
// 1️⃣ Configure Services
// ----------------------------------------------------
builder.Services.AddControllers();
    //.AddJsonOptions(options =>
    //    options.JsonSerializerOptions.ReferenceHandler =
    //        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ Database Connection
builder.Services.AddDbContext<EmpDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnStr")));

//// ✅ JWT Authentication
//var jwtKey = builder.Configuration["Jwt:Key"] ?? "super_secret_key_123!";
//var key = Encoding.ASCII.GetBytes(jwtKey);
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,

        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"]
    };
});

builder.Services.AddAuthorization();

// ✅ Allow Angular Frontend (CORS)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
        policy.WithOrigins("http:////localhost:4200") // Angular default port
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

app.UseCors("AllowAngularApp");
// ----------------------------------------------------
// 2️⃣ Configure Middleware Pipeline
// ----------------------------------------------------
if (app.Environment.IsDevelopment())
{
    //app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

*/





////using Employee_Mgmt_Back.Data;
////using Microsoft.EntityFrameworkCore;

////var builder = WebApplication.CreateBuilder(args);

////// Add services to the container.

////builder.Services.AddControllers();
////builder.Services.AddDbContext<EmpDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnStr")));

////// Learn more about configuring Swagger/OpenAPI at //https:////aka.ms/aspnetcore/swashbuckle
////builder.Services.AddEndpointsApiExplorer();
////builder.Services.AddSwaggerGen();

////var app = builder.Build();

////// Configure the HTTP request pipeline.
////if (app.Environment.IsDevelopment())
////{
////    app.UseSwagger();
////    app.UseSwaggerUI();
////}

////app.UseHttpsRedirection();

////app.UseAuthorization();

////app.MapControllers();

////app.Run();











//////using Microsoft.EntityFrameworkCore.InMemory;
//////using Employee_Mgmt_Back.Services;
//////using Employee_Mgmt_Back.Data;
//////using Employee_Mgmt_Back.Services;
//////using Microsoft.AspNetCore.Authentication.JwtBearer;
//////using Microsoft.EntityFrameworkCore;
//////using Microsoft.IdentityModel.Tokens;
//////using System.Text;

//////var builder = WebApplication.CreateBuilder(args);

//////builder.Services.AddControllers();
//////builder.Services.AddEndpointsApiExplorer();
//////builder.Services.AddSwaggerGen();

//////builder.Services.AddDbContext<EmpDbContext>(opt =>
//////    opt.UseInMemoryDatabase("EMployeeMgmt"));

//////builder.Services.AddScoped<AuthService>();
//////builder.Services.AddScoped<JwtService>();

//////builder.Services.AddCors(options =>
//////{
//////    options.AddPolicy("AllowAngularApp",
//////        b => b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
//////});

//////var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"] ?? "super_secret_key_123!");

//////builder.Services.AddAuthentication(opt =>
//////{
//////    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//////    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//////})
//////.AddJwtBearer(opt =>
//////{
//////    opt.RequireHttpsMetadata = false;
//////    opt.SaveToken = true;
//////    opt.TokenValidationParameters = new TokenValidationParameters
//////    {
//////        ValidateIssuerSigningKey = true,
//////        IssuerSigningKey = new SymmetricSecurityKey(key),
//////        ValidateIssuer = false,
//////        ValidateAudience = false
//////    };
//////});

//////var app = builder.Build();

//////app.UseSwagger();
//////app.UseSwaggerUI();

//////app.UseCors("AllowAngularApp");
//////app.UseAuthentication();
//////app.UseAuthorization();

//////app.MapControllers();
//////app.Run();



/////*
////using Employee_Mgmt_Back.Data;
////using Employee_Mgmt_Back.Models;
////using Employee_Mgmt_Back.Services;
////using Microsoft.AspNetCore.Authentication.JwtBearer;
////using Microsoft.EntityFrameworkCore;
////using Microsoft.IdentityModel.Tokens;
////using System;
////using System.Text;

////var builder = WebApplication.CreateBuilder(args);

////// Add services to the container.

////builder.Services.AddControllers();
////// Learn more about configuring Swagger/OpenAPI at https:////aka.ms/aspnetcore/swashbuckle
////builder.Services.AddEndpointsApiExplorer();
////builder.Services.AddSwaggerGen();

////builder.Services.AddControllers()
////    .AddJsonOptions(options =>
////        options.JsonSerializerOptions.ReferenceHandler = 
////            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);



////builder.Services.AddDbContext<EmpDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnStr")));


////builder.Services.AddScoped<AuthService>();
////builder.Services.AddScoped<JwtService>();

////var jwtKey = builder.Configuration["Jwt:Key"] ?? "super_secret_key_123!";
////var key = Encoding.ASCII.GetBytes(jwtKey);

////builder.Services.AddAuthentication(x =>
////{
////    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
////    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
////})
////.AddJwtBearer(x =>
////{
////    x.RequireHttpsMetadata = false;
////    x.SaveToken = true;
////    x.TokenValidationParameters = new TokenValidationParameters
////    {
////        ValidateIssuerSigningKey = true,
////        IssuerSigningKey = new SymmetricSecurityKey(key),
////        ValidateIssuer = false,
////        ValidateAudience = false
////    };
////});

////builder.Services.AddAuthorization();
////builder.Services.AddCors(options =>
////{
////    options.AddPolicy("AllowAngularApp",
////        policy => policy
////            .WithOrigins("http:////localhost:4200")
////            .AllowAnyHeader()
////            .AllowAnyMethod());
////});

////var app = builder.Build();


////// Configure the HTTP request pipeline.
////if (app.Environment.IsDevelopment())
////{
////    app.UseSwagger();
////    app.UseSwaggerUI();
////}

////app.UseHttpsRedirection();

////app.UseAuthentication();

////app.UseAuthorization();

////app.MapControllers();

////app.Run();
////*/