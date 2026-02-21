using api.DTOs;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }
    
    [HttpPost("PlaceOrder")]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> PlaceOrder([FromBody]List<OrderRequestDto> orderDto)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var result = await _orderService.CreateOrderAsync(orderDto);
        return Ok(result);
    }

    [HttpGet("GetMyOrders")]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> GetMyOrders()
    {
        var result = await _orderService.GetOrdersByUserId();
        return Ok(result);
    }
}