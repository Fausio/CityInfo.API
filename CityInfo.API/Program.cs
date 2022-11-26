using CityInfo.DATA.DbContext;
using CityInfo.SERVICE.Interfaces;
using CityInfo.SERVICE.Services;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Serilog;

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
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<FileExtensionContentTypeProvider>();


#if DEBUG
builder.Services.AddTransient<IMailServices, LocalMailServices>();
#else
builder.Services.AddTransient<IMailServices, CloudMailServices>();
#endif
 

// get value from appSetting
var SQLiteConnectionStrings = builder.Configuration["ConnectionStrings:SQLite"];
builder.Services.AddDbContext<AppDbContext>( dbContextOptions => dbContextOptions.UseSqlite(SQLiteConnectionStrings));
//using (var db = new AppDbContext(new DbContextOptions<AppDbContext>()))
//{
//    //db.Database.EnsureCreated(); Don't use
//    db.Database.Migrate();
//}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints => endpoints.MapControllers());

app.MapControllers();

app.Run();

