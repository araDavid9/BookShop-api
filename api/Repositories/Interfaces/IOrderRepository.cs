using api.Models;

namespace api.Repositories.Interfaces;

public interface IOrderRepository
{
   public Task<Order> CreateOrder(Order order);
   public Task<List<Order>> GetOrdersByUserId(string userId);

}