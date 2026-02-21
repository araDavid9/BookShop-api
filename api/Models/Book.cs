using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api.Models;

public class Book
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set;} = ObjectId.GenerateNewId().ToString();
    public required string Name { get; set; }
    public required decimal Price { get; set; }
    public required int Stock { get; set; }
    
    [BsonRepresentation(BsonType.ObjectId)]
    public string AuthorId { get; set; }
    
    [BsonRepresentation(BsonType.ObjectId)]
    public string PublisherId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; }
}