using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api.Models;

public class Order
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
    
    [BsonRepresentation(BsonType.ObjectId)]
    public string UserId { get; set; }

    public decimal TotalPrice { get; set; } = 0;
    public List<OrderDetail> OrderDetails { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    
}