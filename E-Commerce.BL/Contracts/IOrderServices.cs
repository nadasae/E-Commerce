using E_Commerce.BL.Features.Orders.DTOs;
using E_Commerce.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.BL.Contracts
{
    public interface IOrderServices
    {
        Task<OrderDTO> GetOrderById(int id);
        Task<IEnumerable<OrderDTO>> GetAllOrdersAsync();
        Task<(bool IsSuccess, string Message)> AddOrder(OrderDTO orderDto);
        Task<OrderDTO> UpdateOrder(int id, OrderDTO updateDto);
        Task<int> DeleteOrder(int id);
        Task<List<OrderDTO>> GetOrdersByStatusAsync(OrderStatus status);
    }
}
