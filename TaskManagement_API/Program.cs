using StackExchange.Redis;
using TaskManagement_API.Data;
using TaskManagement_API.Interfaces;
using TaskManagement_API.Repositories;
using TaskManagement_API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
	var Host = builder.Configuration.GetConnectionString("RedisConnection:Host");
	var Password = builder.Configuration.GetConnectionString("RedisConnection:Password");
	return ConnectionMultiplexer.Connect($"{Host},password={Password}");
});

builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<TaskService>();
builder.Services.AddScoped<IListRepository, ListRepository>();
builder.Services.AddScoped<ListService>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<GroupService>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAll",
		builder =>
		{
			builder.AllowAnyOrigin()
					.AllowAnyHeader()
					.AllowAnyMethod();
		});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
