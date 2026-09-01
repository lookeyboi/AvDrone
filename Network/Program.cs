using MongoDB.Driver;
using Network.Models;
using MongoDB.Bson.Serialization.Conventions;

var camelCaseConvention = new ConventionPack { new CamelCaseElementNameConvention() };
ConventionRegistry.Register("CamelCase", camelCaseConvention, type => true);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- MongoDB registration ---

var mongoSettings = builder.Configuration.GetSection("MongoSettings");

builder.Services.AddSingleton<IMongoClient>(sp =>
    new MongoClient("mongodb://localhost:27017"));

builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IMongoClient>().GetDatabase("avdrones"));

// --- Repository and service registration ---

builder.Services.AddScoped<IDroneRepository, DroneRepository>();
builder.Services.AddScoped<IDroneService, DroneService>();

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