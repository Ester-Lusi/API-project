using Dtos;
using Entities;

namespace Services
{
    public interface IOrderService
    {
        Task<OrderDto> AddOrder(OrderDto order);
        Task<OrderDto> GetOrderById(int id);
    }
}