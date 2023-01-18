using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RB.Core.Application.Interface;
using RB.Infrastructure.RB.Infrastructure.Repository;
using RB.Infrastructure.RB.Infrastructure.Services.General.Implementation;
using RB.Infrastructure.RB.Infrastructure.Services.General.Interface;
using RB.Infrastructure.RB.Infrastructure.Services.User;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Environment.IsDevelopment();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactJsDomain",
        policy => policy.WithOrigins("http://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyMethod()
        );
});
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
builder.Services.AddMvc();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
});

builder.Services.AddDbContext<UserDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("Default"),
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure();
                });
    });

builder.Services.AddScoped<IRegisterValidations, RegisterValidations>();
builder.Services.AddScoped<ISaveImage, SaveImage>();
builder.Services.AddScoped<IUserRegistration, UserRegistration>();
builder.Services.AddScoped<IVehicleRegistration, VehicleRegistration>();
builder.Services.AddScoped<IHostRide, HostRideService>();
builder.Services.AddScoped<ICallOption, CallOption>();
builder.Services.AddScoped<IForgotPassword, ForgotPassword>();
builder.Services.AddScoped<IUserLogin, UserLoginService>();
builder.Services.AddScoped<IRequestHandling,RequestHandling>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.GetFullPath("Images")),
    RequestPath = "/Images"
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseCors("ReactJsDomain");
app.UseAuthorization();

app.UseSession();

app.MapControllers();

app.Run();
