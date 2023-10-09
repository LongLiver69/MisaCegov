using Dapper;
using MISA.WEB05.CEGOV;
using MISA.WEB05.CEGOV.Application;
using MISA.WEB05.CEGOV.Domain;
using MISA.WEB05.CEGOV.Infrastructure;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
    //.AddJsonOptions(options =>
    //{
    //    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    //});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var connectionString = builder.Configuration["ConnectionString"];
builder.Services.AddScoped<IUnitOfWork>(provider => new UnitOfWork(connectionString));

builder.Services.AddScoped<IAwardRepository, AwardRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IAwardEmployeeDetailRepository, AwardEmployeeDetailRepository>();
builder.Services.AddScoped<IExcelWorker, ExcelWorker>();
builder.Services.AddScoped<IAwardService, AwardService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IAwardEmployeeDetailService, AwardEmployeeDetailService>();
builder.Services.AddScoped<IAwardManager, AwardManager>();

DefaultTypeMap.MatchNamesWithUnderscores = true;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseStaticFiles();

app.UseCors(option => option.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.MapControllers();

app.UseMiddleware<ExceptionMiddleware>();

app.Run();
