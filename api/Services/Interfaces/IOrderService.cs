using api.DTOs;
using api.Models;

namespace api.Services.Interfaces;

public interface IOrderService
{
    public Task<Order> CreateOrderAsync(List<OrderRequestDto> order);
    public Task<List<OrderViewDto>> GetOrdersByUserId();
}