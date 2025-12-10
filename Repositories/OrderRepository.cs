using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class OrderRepository : IOrderRepository
    {
        shopContext _dbContext;
        public OrderRepository(shopContext context)
        {
            _dbContext = context;
        }

        public async Task<Order> GetOrderById(int id)
        {
            return await _dbContext.Orders.FindAsync(id);
        }
        public async Task<Order> AddOrder(Order order)
        {
            _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
            return order;
        }
    }
}
