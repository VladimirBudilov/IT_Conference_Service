using IT_Conference_Service.Data;
using IT_Conference_Service.Data.Repositories;
using IT_Conference_Service.Data.Repositories.Interfaces;
using IT_Conference_Service.Services;
using IT_Conference_Service.Services.Interfaces;
using IT_Conference_Service.Services.Mapper;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "My API", Version = "v1" });
});

builder.Services.AddDbContext<ConferenceDbContext>(opts =>
{
    opts.UseNpgsql(builder.Configuration.GetConnectionString("WebApiDbConnection"));
});

builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();
builder.Services.AddScoped<ISpeackerInfoRepository, SpeackerInfoRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IApplicationService, ApplicationService>();

builder.Services.AddAutoMapper(typeof(AutomapperProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//CheckDatabase.EnsureExist(app);
app.Run();
