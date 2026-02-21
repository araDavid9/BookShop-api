using api.Data;
using api.Models;
using api.Repositories.Interfaces;

namespace api.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly BookStoreDbContext _dbContext;

    public OrderRepository(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Order> CreateOrder(Order order)
    {
        _dbContext.Orders.Add(order);
        await _dbContext.SaveChangesAsync();
        return order;
    }

    public async Task<List<Order>> GetOrdersByUserId(string userId)
    {
        return _dbContext.Orders
            .Where(o => o.UserId == userId)
            .ToList();
    }
}