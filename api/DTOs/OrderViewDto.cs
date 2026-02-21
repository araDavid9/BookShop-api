using api.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api.DTOs;

public class OrderViewDto
{
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } 
    public decimal TotalPrice { get; set; } 
    public List<OrderDetail> OrderDetails { get; set; }
    public DateTime CreatedAt { get; set; } 
}