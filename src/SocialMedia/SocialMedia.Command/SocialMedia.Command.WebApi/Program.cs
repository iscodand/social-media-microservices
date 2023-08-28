using CQRS.Core.Domain;
using CQRS.Core.Handlers;
using CQRS.Core.Infrastructure;
using SocialMedia.Command.Domain.Aggregates;
using SocialMedia.Command.Infrastructure.Handlers;
using SocialMedia.Command.Infrastructure.Config;
using SocialMedia.Command.Infrastructure.Events;
using SocialMedia.Command.Infrastructure.Repositories;
using SocialMedia.Command.WebApi.Commands;
using Confluent.Kafka;
using SocialMedia.Command.Infrastructure.Dispatchers;
using CQRS.Core.Producers;
using SocialMedia.Command.Producers;

var builder = WebApplication.CreateBuilder(args);

// MongoDB Configuration
builder.Services.Configure<MongoDbConfig>(builder.Configuration.GetSection(nameof(MongoDbConfig)));

// Kafka Configuration
builder.Services.Configure<ProducerConfig>(builder.Configuration.GetSection(nameof(ProducerConfig)));

// Dependency Injection
builder.Services.AddScoped<IEventStoreRepository, EventStoreRepository>();
builder.Services.AddScoped<IEventProducer, EventProducer>();
builder.Services.AddScoped<IEventStore, EventStore>();
builder.Services.AddScoped<IEventSourcingHandler<PostAggregate>, EventSourcingHandler>();
builder.Services.AddScoped<ICommandHandler, CommandHandler>();

// Register command handlers
ICommandHandler commandHandler = builder.Services.BuildServiceProvider().GetRequiredService<ICommandHandler>();
CommandDispatcher dispatcher = new();
dispatcher.RegisterHandler<NewPostCommand>(commandHandler.HandleAsync);
dispatcher.RegisterHandler<UpdatePostCommand>(commandHandler.HandleAsync);
dispatcher.RegisterHandler<LikePostCommand>(commandHandler.HandleAsync);
dispatcher.RegisterHandler<DeletePostCommand>(commandHandler.HandleAsync);
dispatcher.RegisterHandler<AddCommentCommand>(commandHandler.HandleAsync);
dispatcher.RegisterHandler<UpdateCommentCommand>(commandHandler.HandleAsync);
dispatcher.RegisterHandler<RemoveCommentCommand>(commandHandler.HandleAsync);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();
