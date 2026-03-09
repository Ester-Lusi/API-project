using AutoMapper;
using Dtos;
using Entities;
using Microsoft.Extensions.Logging;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _iOrderRepository;
        private readonly IProductRepository _iProductRepository;
        private readonly IMapper _imapper;
        private readonly ILogger<OrderService> _logger;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository, IMapper mapper, ILogger<OrderService> logger)
        {
            _iOrderRepository = orderRepository;
            _iProductRepository = productRepository;
            _imapper = mapper;
            _logger = logger;
        }
        public async Task<OrderDto> GetOrderById(int id)
        {
            Order order = await _iOrderRepository.GetOrderById(id);
            OrderDto orderDto = _imapper.Map<Order,OrderDto>(order);
            return orderDto;
        }
        public async Task<OrderDto> AddOrder(OrderDto order)
        {
            if (await CheckOrderSum(order))
            { 
                Order ord = _imapper.Map<Order>(order);
                Order res = await _iOrderRepository.AddOrder(ord);
                OrderDto orderDto = _imapper.Map<Order,OrderDto>(res);
                return orderDto;
            }
            _logger.LogWarning("user id:" + order.UserId + "tried to close order with unmatched sum");
            return null;
        }


        private async Task<bool> CheckOrderSum(OrderDto order)
        {
            decimal? sum = 0;
            foreach (var item in order.OrderItems)
            {
                Product product = await _iProductRepository.GetProductById(item.ProductId);
                if (product != null)
                    sum += product.Price * item.Quantity;
            }
            if (sum == order.OrderSum)
                return true;
            return false;
        }
    }
}
