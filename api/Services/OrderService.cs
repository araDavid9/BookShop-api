using api.DTOs;
using api.Models;
using api.Repositories.Interfaces;
using api.Services.Interfaces;
using MongoDB.Bson;

namespace api.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IBookRepository _bookRepository;
    private readonly ITokenService _tokenService;
    private readonly ILogger<OrderService> _logger;
    
    public OrderService(IOrderRepository orderRepository, IBookRepository bookRepository, ITokenService tokenService, ILogger<OrderService> logger)
    {
        _orderRepository = orderRepository;
        _bookRepository = bookRepository;
        _tokenService = tokenService;
        _logger = logger;
    }
    
    public async Task<Order> CreateOrderAsync(List<OrderRequestDto> order)
    {
        var userId = await _tokenService.GetUserIdFromToken();
        _logger.LogInformation($"[OrderService] Creating order for user:{userId}");
        var newOrder = new Order()
        {
            UserId = userId,
            OrderDetails = new List<OrderDetail>()
        };
        
        foreach (var item in order)
        {
            if (!ObjectId.TryParse(item.BookId, out var _))
            {
                _logger.LogWarning($"[OrderService] Wrong book id format!!: {item.BookId}");
                throw new Exception("Book wrong format!");
            }
            var currentBook = await _bookRepository.GetBookById(item.BookId);
            if (currentBook == null)
            {
                _logger.LogWarning($"[OrderService] Book wasnt found: {item.BookId}");
                throw new Exception("Book not found");
            }
               
            
            if (currentBook.Stock < item.Quantity)
            {
                _logger.LogWarning($"[OrderService] user tried to order:{item.BookId}- {item.Quantity} items when there is {currentBook.Stock}");
                throw new Exception($"you cant order more than existing stock of this book:{currentBook.Name}");
            }
            
            newOrder.OrderDetails.Add(new()
            {
                BookId = currentBook.Id,
                BookName = currentBook.Name,
                Quantity = item.Quantity,
                Price = currentBook.Price,
            });
            
            newOrder.TotalPrice += item.Quantity * currentBook.Price;
        }

        var result = await _orderRepository.CreateOrder(newOrder);
        _logger.LogInformation($"[OrderService] order has been placed!:{result.Id}");
        await updateStocks(order);
        return result;
    }

    public async Task<List<OrderViewDto>> GetOrdersByUserId()
    {
        var userId = await _tokenService.GetUserIdFromToken();
        var result = await _orderRepository.GetOrdersByUserId(userId);
        return result.Count > 0
            ? result.Select(x => new OrderViewDto()
            {
                Id = x.Id,
                TotalPrice = x.TotalPrice,
                OrderDetails = x.OrderDetails,
                CreatedAt = x.CreatedAt
            }).ToList()
            : [];

    }

    private async Task updateStocks(List<OrderRequestDto> order)
    {
        _logger.LogInformation($"[OrderService] Updating stocks");
        foreach (var item in order)
        {
            _logger.LogInformation($"[OrderService] updating stock for :{item.BookId}");
           await _bookRepository.UpdateBookStock(item.Quantity, item.BookId);
        }
    }
}