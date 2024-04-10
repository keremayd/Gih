using Microsoft.AspNetCore.Mvc;
using NLog;
using Repositories.Contracts;
using Services;
using Services.Contracts;
using Services.Helper;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);
LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container.
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigurePostgreSqlContext(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.ConfigureActionFilters(); //IoC kaydını gerçekleştirdik ValidationFilterAttribute ve LogFilterAttribute'un
builder.Services.ConfigureLoggerService();
builder.Services.AddScoped<IPersonValidateService,PersonValidateService>();
builder.Services.AddScoped<IRestaurantValidateService,RestaurantValidateService>();
builder.Services.AddScoped<IAuthenticationService,AuthenticationService>();

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILoggerService>();
app.ConfigureExceptionHandler(logger);

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