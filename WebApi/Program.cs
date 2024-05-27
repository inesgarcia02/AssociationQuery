using Application.Services;
using DataModel.Mapper;
using DataModel.Repository;
using Domain.Factory;
using Domain.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using WebApi.Controllers;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
string replicaNameArg = Array.Find(args, arg => arg.Contains("--replicaName"));
string replicaName;
if (replicaNameArg != null)
    replicaName = replicaNameArg.Split('=')[1];
else
    replicaName = config.GetConnectionString("replicaName");

var queueName = config["Queues:" + replicaName];

var port = config["Ports:" + replicaName];

var rabbitMqHost = config["RabbitMq:Host"];
var rabbitMqPort = config["RabbitMq:Port"];
var rabbitMqUser = config["RabbitMq:UserName"];
var rabbitMqPass = config["RabbitMq:Password"];

string DBConnectionString = config.GetConnectionString("PostgresConnection");

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<AbsanteeContext>(option =>
{
    option.UseNpgsql(DBConnectionString);
}, optionsLifetime: ServiceLifetime.Scoped);
 
 
// builder.Services.AddDbContextFactory<AbsanteeContext>(options =>
// {
//     options.UseNpgsql(DBConnectionString);
// });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(opt =>
    opt.MapType<DateOnly>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "date",
        Example = new OpenApiString(DateTime.Today.ToString("yyyy-MM-dd"))
    })
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});


builder.Services.AddSingleton<IConnectionFactory>(sp =>
{
    return new ConnectionFactory()
    {
        HostName = rabbitMqHost,
        Port = int.Parse(rabbitMqPort),
        UserName = rabbitMqUser,
        Password = rabbitMqPass
    };
});

builder.Services.AddTransient<IAssociationRepository, AssociationRepository>();
builder.Services.AddTransient<IAssociationFactory, AssociationFactory>();
builder.Services.AddTransient<AssociationMapper>();
builder.Services.AddTransient<AssociationService>();

builder.Services.AddTransient<IColaboratorsIdRepository, ColaboratorsIdRepository>();
builder.Services.AddTransient<ColaboratorsIdMapper>();
builder.Services.AddTransient<ColaboratorIdService>();

builder.Services.AddTransient<IProjectRepository, ProjectRepository>();
builder.Services.AddTransient<ProjectMapper>();
builder.Services.AddTransient<ProjectService>();

builder.Services.AddSingleton<IRabbitMQConsumerController, RabbitMQAssociationConsumerController>();
builder.Services.AddSingleton<IRabbitMQConsumerController, RabbitMQProjectConsumerController>();
builder.Services.AddSingleton<IRabbitMQConsumerController, RabbitMQColaboratorConsumerController>();


var app = builder.Build();

var rabbitMQConsumerServices = app.Services.GetServices<IRabbitMQConsumerController>();
foreach (var service in rabbitMQConsumerServices)
{
    service.ConfigQueue(queueName);
    service.StartConsuming();
};

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run($"https://localhost:{port}");

public partial class Program{ }