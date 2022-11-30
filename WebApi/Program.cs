using System.Text;
using Core.Context;
using Core.Entities;
using Core.Repositories.AccountRepository;
using Core.Repositories.FarmRepository;
using Core.Repositories.PetRepository;
using Core.Repositories.PetStatsRepository;
using Core.Repositories.UserActionRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Service.AutoMappers;
using Service.Services.AccountService;
using Service.Services.FarmService;
using Service.Services.FarmsService;
using Service.Services.PetService;
using Service.Services.PetsService;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.TryAddScoped<UserManager<User>>();
// Add services to the container.
builder.Services
    .AddDbContext<PetContext>
        (options => options.UseNpgsql(builder.Configuration["ConnectionStrings:DefaultConnection"]));
builder.Services.AddIdentity<User, Role>().AddEntityFrameworkStores<PetContext>();


//DI
builder.Services.AddAutoMapper(typeof(AppMappingFarm), typeof(AppMappingPet), typeof(AppMappingUser));

//rep
builder.Services.AddTransient<IAccountRepository, AccountRepositroy>();
builder.Services.AddTransient<IFarmRepository, FarmRepository>();
builder.Services.AddTransient<IPetRepository, PetRepository>();
builder.Services.AddTransient<IPetStatsRepository, PetStatsRepository>();
builder.Services.AddTransient<IUserActionRepository, UserActionRepository>();
//service
builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<IFarmService, FarmService>();
builder.Services.AddTransient<IFarmsService, FarmsService>();
builder.Services.AddTransient<IPetsService, PetsService>();
builder.Services.AddTransient<IPetService, PetService>();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
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

