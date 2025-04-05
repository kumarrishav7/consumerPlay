using Consumer;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

builder.Services.AddSingleton<IConnection>(provider =>
{
    try
    {
        var factory = new ConnectionFactory()
        {
            HostName = builder.Configuration["RabbitMQ:HostName"],
            UserName = builder.Configuration["RabbitMQ:UserName"],
            Password = builder.Configuration["RabbitMQ:Password"],
        };
        return factory.CreateConnection();
    }
    catch (Exception ex)
    {
        var logger = provider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "RabbitMQ connection failed.");
        throw new InvalidOperationException("Failed to create RabbitMQ connection.", ex);
    }
});

builder.Services.AddSingleton<MessageRepository>();
builder.Services.AddHostedService<ReadFromQueue>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.UseCors(options =>
    options.WithOrigins("http://localhost:4200", "https://ambitious-beach-032f22500.6.azurestaticapps.net")
            .AllowAnyMethod()
            .AllowCredentials()
            .AllowAnyHeader());
//app.MapHub<MySignalRHub>("/listHub");
app.Run();
