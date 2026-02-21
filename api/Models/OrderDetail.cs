using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api.Models;

public class OrderDetail
{
    [BsonRepresentation(BsonType.ObjectId)]
    public required string BookId { get; set; }
    public required string BookName { get; set; }
    public required int Quantity { get; set; }
    public required decimal Price { get; set; }
}