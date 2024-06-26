using CS_Emerios_API_Tracker.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Serilog.Events;
using Serilog;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using MudBlazor.Services;
using CS_Emerios_API_Tracker.Helper;
using CS_Emerios_API_Tracker.Infrastructure.Mapping;
using CS_Emerios_API_Tracker.Infrastructure.API;
using CS_Emerios_API_Tracker.Infrastructure.Interfaces;

var builder = WebApplication.CreateBuilder(args);

//Serilog
Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.File("C:\\EmeriosAPITracker\\log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
//builder.Services.AddSingleton<WeatherForecastService>();

// Add Optimization of SignalR to precise on what kind of data we will be getting at
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});

// Adds the required services to the ASP.NET Core Dependency Injection (DI) layer to support SignalR.
builder.Services.AddSignalR(o =>
{
    o.EnableDetailedErrors = true;
    o.MaximumReceiveMessageSize = 102400000;
});

// Add Protected Session Storage
builder.Services.AddScoped<ProtectedLocalStorage>();

// Add Custom Authentication State Provider
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Add API Controller
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
    options.UseCamelCasing(false);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Mud Component To the library
builder.Services.AddMudServices();

// Add interface and service method
builder.Services.AddScoped<IADConnection, ADConnection>();

var app = builder.Build();

// Add Response Compression Middleware For SignalR
app.UseResponseCompression();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();
}

// For Swagger Documentation
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseStaticFiles();

//Add For API Controller
app.UseRouting();

// Add For API Controller
app.MapControllers();

app.MapBlazorHub();

// Add For SignalR
//app.MapHub<DashboardHub>("/dashboardhub");

app.MapFallbackToPage("/_Host");

app.Run();
