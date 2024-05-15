using Airlines.Persistence.Repository;
using Airlines.Persistence.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Airlines.Persistence.Entities;
using Airlines.Service.Services.AirlineService;
using Airlines.Service.Services.AirportService;
using Airlines.Service.Services.FlightService;
using Airlines.Service.Mappers;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Local");
builder.Services.AddDbContext<AirlinesDBContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(typeof(AirlineMapper).Assembly);
builder.Services.AddSingleton<AirlineMapper>();
builder.Services.AddSingleton<AirportMapper>();
builder.Services.AddSingleton<FlightMapper>();

builder.Services.AddScoped<IAirlineRepository, AirlineRepository>();
builder.Services.AddScoped<IAirportRepository, AirportRepository>();
builder.Services.AddScoped<IFlightRepository, FlightRepository>();

builder.Services.AddScoped<IAirlineService, AirlineService>();
builder.Services.AddScoped<IAirportService, AirportService>();
builder.Services.AddScoped<IFlightService, FlightService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
