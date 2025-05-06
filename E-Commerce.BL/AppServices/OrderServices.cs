using E_Commece.DA.Context;
using E_Commerce.BL.Contracts;
using E_Commerce.BL.Features.Orders.DTOs;
using E_Commerce.BL.Features.Orders.Validators;
using E_Commerce.Core.Entitiies;
using E_Commerce.Core.Enums;
using E_Commerce.Core.Extensions;
using E_Commerce.Core.Interfaces.Repositories;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.BL.AppServices
{
    public class OrderServices : AppService, IOrderServices
    {
      
        private readonly IOrderRepository orderRepository;

        public OrderServices(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }
        public async Task<OrderDTO> GetOrderById(int id)
        {
            var order = await orderRepository.GetByIdAsync(id);

            if (order == null) return null;

            var orderMap = order.Adapt<OrderDTO>();
            orderMap.Total = order.CalculateTotalAmount();

            return orderMap;
        }
        public async Task<IEnumerable<OrderDTO>> GetAllOrdersAsync()
        {
            var orders = await orderRepository.GetAllAsync(o => o.Products);

            foreach (var ord in orders)
            {
                var original = orders.First(o => o.Id == ord.Id);
                 original.CalculateTotalAmount();
            }
            var mapped = orders.Adapt<List<OrderDTO>>();
            return mapped;
        }
        public async Task<(bool IsSuccess, string Message)> AddOrder(OrderDTO orderDto)
        {
            await DoValidationAsync<OrderDTOValidator, OrderDTO>(orderDto);

            var order = new Order
            {
                OrderDate = DateTime.Now,
                Status = orderDto.Status,
                Products = orderDto.Products.Select(p => new Product
                {
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = p.Quantity
                }).ToList()

            };
           await orderRepository.AddAsync(order);
           await  orderRepository.CommitAsync();
            // var totalAmount = order.CalculateTotalAmount();
            return (true, "Order Added successfully.");
        }
        public async Task<OrderDTO> UpdateOrder(int id, OrderDTO updateDto)
        {
            var orders = await orderRepository.GetAllAsync(o => o.Products);
            var order = orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
                throw new ArgumentNullException(nameof(order), "Order not found.");

           
            await orderRepository.CommitAsync();

            var dto = order.Adapt<OrderDTO>();
            dto.Total = order.CalculateTotalAmount();
            return dto;
        }
        public async Task<int> DeleteOrder(int id)
        {
            var orders = await orderRepository.GetAllAsync(o => o.Products);
            var order = orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
                throw new ArgumentNullException(nameof(order), "Order not found.");

            orderRepository.Delete(order);
            await orderRepository.CommitAsync();
            return order.Id;
        }
        public async Task<List<OrderDTO>> GetOrdersByStatusAsync(OrderStatus status)
        {
            var orders = await orderRepository.GetAllAsync(o => o.Products);

            var filteredOrders = orders.Where(o => o.Status == status);

            var orderDTOs = filteredOrders.Select(order =>
            {
                var dto = order.Adapt<OrderDTO>();
                dto.Total = order.CalculateTotalAmount();
                return dto;
            }).ToList();

            return orderDTOs;
        }
    }
}

        