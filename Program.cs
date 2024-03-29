using IT_Conference_Service.Data;
using IT_Conference_Service.Data.Repositories;
using IT_Conference_Service.Data.Repositories.Interfaces;
using IT_Conference_Service.Services.Interfaces;
using IT_Conference_Service.Services.Mapper;
using IT_Conference_Service.Services.Models;
using IT_Conference_Service.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    c.EnableAnnotations();
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    
});

builder.Services.AddDbContext<ConferenceDbContext>(opts =>
{
    opts.UseNpgsql(builder.Configuration.GetConnectionString("WebApiDbConnection"));
});

builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();
builder.Services.AddScoped<IAuthorInfoRepository, AuthorInfoRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IApplicationService, ApplicationService>();
builder.Services.AddScoped<ServiceValidator>();

builder.Services.AddAutoMapper(typeof(AutomapperProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseAuthorization();

app.MapControllers();

CheckDatabase.EnsureExist(app);
app.Run();
