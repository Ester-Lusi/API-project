using AutoMapper;
using Dtos;
using Entities;
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
        IOrderRepository _iOrderRepository;
        //AutoMapper _imapper;
        IMapper _imapper;
        public OrderService(IOrderRepository iOrderRepository, IMapper mapper)
        {
            _iOrderRepository = iOrderRepository;
            _imapper = mapper;
        }
        public async Task<OrderDto> GetOrderById(int id)
        {
            Order order = await _iOrderRepository.GetOrderById(id);
            OrderDto orderDto = _imapper.Map<Order,OrderDto>(order);
            return orderDto;
        }
        public async Task<OrderDto> AddOrder(OrderDto order)
        {
            Order ord = _imapper.Map<Order>(order);
            Order res = await _iOrderRepository.AddOrder(ord);
            OrderDto orderDto = _imapper.Map<Order,OrderDto>(res);
            return orderDto;
        }
    }
}
