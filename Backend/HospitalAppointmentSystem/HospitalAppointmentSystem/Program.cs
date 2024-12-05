using Microsoft.EntityFrameworkCore;
using DataAccess.Context;
using DataAccess.UnitOfWork;
using DataAccess.Repositories.Interfaces;
using DataAccess.Repositories.Implementations;
using Business.Services.Implementations;
using Business.Helpers;
using Microsoft.OpenApi.Models;
using Business.Mapping;
using Business.Services.Interfaces;
using Business.Validation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Database connection setup
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dependency Injection for UnitOfWork and Repositories
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();
builder.Services.AddScoped<IMedicalReportRepository, MedicalReportRepository>();
builder.Services.AddScoped<IDoctorScheduleRepository, DoctorScheduleRepository>();
builder.Services.AddScoped<ILiveSupportRepository, LiveSupportRepository>();

// Dependency Injection for Business Layer Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<ILiveSupportService, LiveSupportService>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IFeedbackService, FeedbackService>();
builder.Services.AddScoped<IMedicalReportService, MedicalReportService>();

builder.Services.AddScoped<UserValidator>();
builder.Services.AddScoped<AppointmentValidator>();

// Automapper setup for mapping profiles
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddControllers();
// JWT Authentication configuration
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
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
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("DoctorOnly", policy => policy.RequireRole("Doctor"));
    options.AddPolicy("PatientOnly", policy => policy.RequireRole("Patient"));
});

// CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Swagger/OpenAPI setup
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Hospital Appointment System API",
        Version = "v1",
        Description = "Hospital appointment management system API documentation."
    });

    // JWT Ayarlarý
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\""
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1"));
}

// Middleware for HTTPS, CORS, and Exception handling
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthentication();  // Authentication middleware
app.UseAuthorization();   // Authorization middleware
app.MapControllers();

app.Run();
