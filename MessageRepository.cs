using MongoDB.Bson;
using MongoDB.Driver;

public class MessageRepository
{
    private readonly IMongoCollection<Message> _messages;

    public MessageRepository(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetConnectionString("MongoDb"));
        var database = client.GetDatabase("NotificationDB");
        _messages = database.GetCollection<Message>("Notifications");
    }

    public async Task<Message> AddMessageAsync(Message message)
    {
        await _messages.InsertOneAsync(message);
        return message;
    }

    public async Task<List<Message>> GetMessagesAsync(string senderId, string receiverId)
    {
        var filter = Builders<Message>.Filter.Where(m =>
            (m.SenderId == senderId && m.ReceiverId == receiverId) ||
            (m.SenderId == receiverId && m.ReceiverId == senderId));

        return await _messages.Find(filter).ToListAsync();
    }
}

public class Message
{
    public ObjectId Id { get; set; }
    public required string SenderId { get; set; }
    public required string ReceiverId { get; set; }
    public required string Content { get; set; }
    public DateTime Timestamp { get; set; }
}

