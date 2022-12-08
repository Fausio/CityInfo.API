using CityInfo.DATA.DbContext;
using CityInfo.SERVICE.Interfaces;
using CityInfo.SERVICE.Repository.Interfaces;
using CityInfo.SERVICE.Repository.Services;
using CityInfo.SERVICE.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson()
    .AddXmlDataContractSerializerFormatters();

Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
                                      .WriteTo.Console()
                                      .WriteTo.File("Logs/ASpNetCore_6_Log.txt", rollingInterval: RollingInterval.Day)
                                      .CreateLogger();

builder.Host.UseSerilog();
//builder.Logging.ClearProviders();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    setup.AddSecurityDefinition("AppSecurityDefinition", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Input for a valid token to access the API"
    });

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                 Reference = new OpenApiReference
                 {
                     Type = ReferenceType.SecurityScheme,
                     Id = "AppSecurityDefinition"
                 }
            }, new List<string>()
        }
    });
});

builder.Services.AddSingleton<FileExtensionContentTypeProvider>();


#if DEBUG
builder.Services.AddTransient<IMailServices, LocalMailServices>();
#else
builder.Services.AddTransient<IMailServices, CloudMailServices>();
#endif

builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// get value from appSetting
var SQLiteConnectionStrings = builder.Configuration["ConnectionStrings:SQLite"];
builder.Services.AddDbContext<AppDbContext>(dbContextOptions => dbContextOptions.UseSqlite(SQLiteConnectionStrings));
//using (var db = new AppDbContext(new DbContextOptions<AppDbContext>()))
//{
//    //db.Database.EnsureCreated(); Don't use
//    db.Database.Migrate();
//}

builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = builder.Configuration["Authentication:Issuer"],
                        ValidAudience = builder.Configuration["Authentication:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForkey"]))

                    };
                });

builder.Services.AddApiVersioning(setup =>
{
    setup.AssumeDefaultVersionWhenUnspecified = true;
    setup.DefaultApiVersion = new ApiVersion(1, 0);
    setup.ReportApiVersions = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => endpoints.MapControllers());

app.MapControllers();

app.Run();

