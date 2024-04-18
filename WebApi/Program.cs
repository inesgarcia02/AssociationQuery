using Application.Services;
using DataModel.Mapper;
using DataModel.Repository;
using Domain.Factory;
using Domain.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using WebApi.Controllers;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
var associationQueueName = config["AssociationQueues:" + args[0]];
var projectQueueName = config["ProjectQueues:" + args[0]];
var colaboratorQueueName = config["ColaboratorQueues:" + args[0]];


// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AbsanteeContext>(opt =>
    //opt.UseInMemoryDatabase("AbsanteeList")
    //opt.UseSqlite("Data Source=AbsanteeDatabase.sqlite")
    opt.UseSqlite(Host.CreateApplicationBuilder().Configuration.GetConnectionString("AbsanteeDatabase"))
    );

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(opt =>
    opt.MapType<DateOnly>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "date",
        Example = new OpenApiString(DateTime.Today.ToString("yyyy-MM-dd"))
    })
);

builder.Services.AddTransient<IAssociationRepository, AssociationRepository>();
builder.Services.AddTransient<IAssociationFactory, AssociationFactory>();
builder.Services.AddTransient<AssociationMapper>();
builder.Services.AddTransient<AssociationService>();

//builder.Services.AddSingleton<IRabbitMQAssociationConsumerController, RabbitMQAssociationConsumerController>();
builder.Services.AddTransient<IColaboratorsIdRepository, ColaboratorsIdRepository>();
builder.Services.AddTransient<ColaboratorsIdMapper>();
builder.Services.AddTransient<ColaboratorIdService>();
builder.Services.AddTransient<IProjectRepository, ProjectRepository>();
builder.Services.AddTransient<ProjectMapper>();
builder.Services.AddTransient<ProjectService>();

// builder.Services.AddSingleton<IRabbitMQConsumerController>(sp =>
// {
//     using (var scope = sp.CreateScope())
//     {
//         var scopedServices = scope.ServiceProvider;
//         var associationService = scopedServices.GetRequiredService<AssociationService>();
//         return new RabbitMQConsumerController(associationService);
//     }
// });

builder.Services.AddSingleton<IRabbitMQAssociationConsumerController, RabbitMQAssociationConsumerController>();
builder.Services.AddSingleton<IRabbitMQProjectConsumerController, RabbitMQProjectConsumerController>();
builder.Services.AddSingleton<IRabbitMQColaboratorConsumerController, RabbitMQColaboratorConsumerController>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

var rabbitMQAssociationCConsumerService = app.Services.GetRequiredService<IRabbitMQAssociationConsumerController>();
var rabbitMQProjectConsumerService = app.Services.GetRequiredService<IRabbitMQProjectConsumerController>();
var rabbitMQColaboratorService = app.Services.GetRequiredService<IRabbitMQColaboratorConsumerController>();

rabbitMQAssociationCConsumerService.ConfigQueue(associationQueueName);
rabbitMQProjectConsumerService.ConfigQueue(projectQueueName);
rabbitMQColaboratorService.ConfigQueue(colaboratorQueueName);

rabbitMQAssociationCConsumerService.StartConsuming();
rabbitMQProjectConsumerService.StartConsuming();
rabbitMQColaboratorService.StartConsuming();

app.MapControllers();

app.Run();
