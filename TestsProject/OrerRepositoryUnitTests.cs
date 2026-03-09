using AutoMapper;
using Dtos;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Repositories;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.TestProject

{
    public class OrderRepositoryUnitTests
    {
        private readonly Mock<ShopContext> _mockContext;
        private readonly OrderRepository _orderRepository;
        private readonly Mock<DbSet<Order>> _mockOrderSet;

        public OrderRepositoryUnitTests()
        {
            _mockContext = new Mock<ShopContext>();
            _mockOrderSet = new Mock<DbSet<Order>>();

            // Mock DbSet behavior
            _mockContext.Setup(m => m.Orders).Returns(_mockOrderSet.Object);
            _orderRepository = new OrderRepository(_mockContext.Object);
        }

        [Fact]
        public async Task GetOrderById_ReturnsOrder_WithOrderItems_WhenOrderExists()
        {
            // Arrange
            var orderId = 1;
            var order = new Order
            {
                OrderId = orderId,
                OrderItems = new List<OrderItem>
                {
                    new OrderItem { ProductId = 1, Quantity = 2 },
                    new OrderItem { ProductId = 2, Quantity = 1 }
                }
            };

            var ordersData = new List<Order> { order }.AsQueryable();

            // Setting up the mock DbSet to return the list of orders
            _mockOrderSet.As<IQueryable<Order>>().Setup(m => m.Provider).Returns(ordersData.Provider);
            _mockOrderSet.As<IQueryable<Order>>().Setup(m => m.Expression).Returns(ordersData.Expression);
            _mockOrderSet.As<IQueryable<Order>>().Setup(m => m.ElementType).Returns(ordersData.ElementType);
            _mockOrderSet.As<IQueryable<Order>>().Setup(m => m.GetEnumerator()).Returns(ordersData.GetEnumerator());

            // Act
            var result = await _orderRepository.GetOrderById(orderId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(orderId, result.OrderId);
            Assert.Equal(2, result.OrderItems.Count);
        }

        [Fact]
        public async Task GetOrderById_ReturnsNull_WhenOrderDoesNotExist()
        {
            // Arrange
            var orderId = 999; // Non-existing order ID

            // Setting up the mock to return an empty set
            _mockOrderSet.As<IQueryable<Order>>().Setup(m => m.Provider).Returns(Enumerable.Empty<Order>().AsQueryable().Provider);
            _mockOrderSet.As<IQueryable<Order>>().Setup(m => m.Expression).Returns(Enumerable.Empty<Order>().AsQueryable().Expression);
            _mockOrderSet.As<IQueryable<Order>>().Setup(m => m.ElementType).Returns(Enumerable.Empty<Order>().AsQueryable().ElementType);
            _mockOrderSet.As<IQueryable<Order>>().Setup(m => m.GetEnumerator()).Returns(Enumerable.Empty<Order>().AsQueryable().GetEnumerator());

            // Act
            var result = await _orderRepository.GetOrderById(orderId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddOrder_ReturnsOrder_WhenOrderIsAdded()
        {
            // Arrange
            var order = new Order { OrderId = 1, OrderItems = new List<OrderItem>() };

            _mockContext.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1); // Simulate save changes

            // Act
            var result = await _orderRepository.AddOrder(order);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(order.OrderId, result.OrderId);
            _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once); // Verify SaveChangesAsync was called
        }

        [Fact]
        public async Task AddOrder_ShouldCallAddAsync_WhenOrderIsAdded()
        {
            // Arrange
            var order = new Order { OrderId = 1, OrderItems = new List<OrderItem>() };

            // Act
            await _orderRepository.AddOrder(order);

            // Assert
            _mockOrderSet.Verify(m => m.AddAsync(It.IsAny<Order>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task AddOrder_ShouldThrowException_WhenOrderIsNull()
        {
            // Arrange
            Order order = null;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _orderRepository.AddOrder(order));
        }

        [Fact]
        public async Task AddOrder_ShouldReturnOrderDto_WhenSumIsCorrect()
        {
            // Arrange
            var orderDto = new OrderDto(
                OrderId: 1,
                OrderDate: DateOnly.FromDateTime(DateTime.Now),
                OrderSum: 35m,
                OrderItems: new List<OrderItemsDto>
                {
                    new OrderItemsDto(1, 2), // 2 * 10 = 20
                    new OrderItemsDto(2, 3)  // 3 * 5 = 15
                },
                UserId: 42
            );

            // הגדרת המוקים
            var mockProductRepo = new Mock<IProductRepository>();
            var mockOrderRepo = new Mock<IOrderRepository>();
            var mockMapper = new Mock<IMapper>();
            var mockLogger = new Mock<ILogger<OrderService>>();

            // הגדרת מחירים במוק
            mockProductRepo.Setup(r => r.GetProductById(1)).ReturnsAsync(new Product { ProductId = 1, Price = 10m });
            mockProductRepo.Setup(r => r.GetProductById(2)).ReturnsAsync(new Product { ProductId = 2, Price = 5m });


            var orderEntity = new Order { OrderId = 1 };
            mockMapper.Setup(m => m.Map<Order>(orderDto)).Returns(orderEntity);
            mockOrderRepo.Setup(r => r.AddOrder(orderEntity)).ReturnsAsync(orderEntity);
            mockMapper.Setup(m => m.Map<Order, OrderDto>(orderEntity)).Returns(orderDto);

            var orderService = new OrderService(mockOrderRepo.Object, mockProductRepo.Object, mockMapper.Object, mockLogger.Object);

            // Act
            // אנחנו קוראים ל-AddOrder
            //היא כבר תפעיל את 
            //CheckOrderSum
            //הפרטית
            var result = await orderService.AddOrder(orderDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(35m, result.OrderSum);
        }

        [Fact]
        public async Task AddOrder_ShouldReturnNull_WhenSumIsIncorrect()
        {
            // Arrange
            var orderDto = new OrderDto(
                OrderId: 2,
                OrderDate: DateOnly.FromDateTime(DateTime.Now),
                OrderSum: 40m, // סכום שגוי (אמור להיות 35)
                OrderItems: new List<OrderItemsDto>
                {
                    new OrderItemsDto(1, 2), // 20
                    new OrderItemsDto(2, 3)  // 15
                },
                UserId: 42
            );

            var mockProductRepo = new Mock<IProductRepository>();
            var mockOrderRepo = new Mock<IOrderRepository>();
            var mockMapper = new Mock<IMapper>();
            var mockLogger = new Mock<ILogger<OrderService>>();

            mockProductRepo.Setup(r => r.GetProductById(1)).ReturnsAsync(new Product { ProductId = 1, Price = 10m });
            mockProductRepo.Setup(r => r.GetProductById(2)).ReturnsAsync(new Product { ProductId = 2, Price = 5m });

            var orderService = new OrderService(mockOrderRepo.Object, mockProductRepo.Object, mockMapper.Object, mockLogger.Object);

            // Act
            var result = await orderService.AddOrder(orderDto);

            // Assert
            // בגלל שהסכום שגוי
            // CheckOrderSum תחזיר false
            //AddOrder תחזיר null
            Assert.Null(result);
        }
    }
}
