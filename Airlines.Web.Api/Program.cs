using Airlines.Persistence.Repository;
using Airlines.Persistence.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Airlines.Persistence.Entities;
using Airlines.Service.Services.AirlineService;
using Airlines.Service.Services.AirportService;
using Airlines.Service.Services.FlightService;
using Airlines.Service.Mappers;
using YourProject.Web.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Home");
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

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

const string AllowAnyOrigin = "_allowAnyOriginPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowAnyOrigin,
                      policy =>
                      {
                          policy.AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader();
                      });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(AllowAnyOrigin);

app.Run();